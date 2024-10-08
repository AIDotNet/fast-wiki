﻿using FastWiki.Service.Service;
using Microsoft.KernelMemory.AI.OpenAI;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory.DataFormats.Text;
using Microsoft.KernelMemory.Diagnostics;
using Microsoft.KernelMemory.Pipeline;

namespace FastWiki.Service.Infrastructure.KM;

/// <summary>
///     QA问答处理器
/// </summary>
public class QAHandler : IPipelineStepHandler
{
    public static readonly AsyncLocal<(Wiki, WikiDetail)> _wikiDetail = new();

    private readonly ILogger<QAHandler> _log;
    private readonly TextPartitioningOptions _options;
    private readonly IPipelineOrchestrator _orchestrator;
#pragma warning disable KMEXP00
    private readonly TextChunker.TokenCounter _tokenCounter;
#pragma warning restore KMEXP00
    private readonly WikiMemoryService _wikiMemoryService;

    /// </inheritdoc>
    public QAHandler(
        string stepName,
        IPipelineOrchestrator orchestrator, WikiMemoryService wikiMemoryService,
        TextPartitioningOptions? options = null,
        ILogger<QAHandler>? log = null
    )
    {
        StepName = stepName;
        _orchestrator = orchestrator;
        _wikiMemoryService = wikiMemoryService;
        _options = options ?? new TextPartitioningOptions();
        _options.Validate();

        _log = log ?? DefaultLogger<QAHandler>.Instance;
        _tokenCounter = DefaultGPTTokenizer.StaticCountTokens;
    }

    /// <inheritdoc />
    public string StepName { get; }

    /// <inheritdoc />
    public async Task<(bool success, DataPipeline updatedPipeline)> InvokeAsync(
        DataPipeline pipeline, CancellationToken cancellationToken = default)
    {
        _log.LogDebug("Partitioning text, pipeline '{0}/{1}'", pipeline.Index, pipeline.DocumentId);

        if (pipeline.Files.Count == 0)
        {
            _log.LogWarning("Pipeline '{0}/{1}': there are no files to process, moving to next pipeline step.",
                pipeline.Index, pipeline.DocumentId);
            return (true, pipeline);
        }

        foreach (var uploadedFile in pipeline.Files)
        {
            // Track new files being generated (cannot edit originalFile.GeneratedFiles while looping it)
            Dictionary<string, DataPipeline.GeneratedFileDetails> newFiles = new();

            foreach (var generatedFile in uploadedFile
                         .GeneratedFiles)
            {
                var file = generatedFile.Value;
                if (file.AlreadyProcessedBy(this))
                {
                    _log.LogTrace("File {0} already processed by this handler", file.Name);
                    continue;
                }

                // Partition only the original text
                if (file.ArtifactType != DataPipeline.ArtifactTypes.ExtractedText)
                {
                    _log.LogTrace("Skipping file {0} (not original text)", file.Name);
                    continue;
                }

                // Use a different partitioning strategy depending on the file type
                List<string> partitions = new();
                List<string> sentences = new();
                var partitionContent = await _orchestrator
                    .ReadFileAsync(pipeline, file.Name, cancellationToken).ConfigureAwait(false);

                // Skip empty partitions. Also: partitionContent.ToString() throws an exception if there are no bytes.
                if (partitionContent.ToArray().Length == 0) continue;

                switch (file.MimeType)
                {
                    case MimeTypes.PlainText:
                    case MimeTypes.MarkDown:
                    {
                        _log.LogDebug("Partitioning text file {0}", file.Name);
                        var content = partitionContent.ToString();

                        var (wiki, wikiDetail) = _wikiDetail.Value;

                        await foreach (var item in OpenAIService
                                           .QaAsync(wikiDetail.QAPromptTemplate, content,
                                               wiki.Model, OpenAIOption.ChatToken,
                                               OpenAIOption.ChatEndpoint, _wikiMemoryService)
                                           .WithCancellation(cancellationToken))
                        {
                            partitions.Add(item);
                            sentences.Add(item);
                        }

                        break;
                    }
                    default:
                        _log.LogWarning("File {0} cannot be partitioned, type '{1}' not supported", file.Name,
                            file.MimeType);
                        // Don't partition other files
                        continue;
                }

                if (partitions.Count == 0) continue;

                _log.LogDebug("Saving {0} file partitions", partitions.Count);
                for (var partitionNumber = 0; partitionNumber < partitions.Count; partitionNumber++)
                {
                    // TODO: turn partitions in objects with more details, e.g. page number
                    var text = partitions[partitionNumber];
                    var sectionNumber = 0; // TODO: use this to store the page number (if any)
                    BinaryData textData = new(text);

                    var tokenCount = _tokenCounter(text);
                    _log.LogDebug("Partition size: {0} tokens", tokenCount);

                    var destFile = uploadedFile.GetPartitionFileName(partitionNumber);
                    await _orchestrator.WriteFileAsync(pipeline, destFile, textData, cancellationToken)
                        .ConfigureAwait(false);

                    var destFileDetails = new DataPipeline.GeneratedFileDetails
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        ParentId = uploadedFile.Id,
                        Name = destFile,
                        Size = text.Length,
                        MimeType = MimeTypes.PlainText,
                        ArtifactType = DataPipeline.ArtifactTypes.TextPartition,
                        PartitionNumber = partitionNumber,
                        SectionNumber = sectionNumber,
                        Tags = pipeline.Tags,
                        ContentSHA256 = textData.CalculateSHA256()
                    };
                    newFiles.Add(destFile, destFileDetails);
                    destFileDetails.MarkProcessedBy(this);
                }

                file.MarkProcessedBy(this);
            }

            // Add new files to pipeline status
            foreach (var file in newFiles) uploadedFile.GeneratedFiles.Add(file.Key, file.Value);
        }

        return (true, pipeline);
    }
}