﻿using System.Security.Cryptography;
using System.Text;
using FastWiki.Service.Backgrounds;
using FastWiki.Service.Service;
using Microsoft.KernelMemory.AI.OpenAI;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory.DataFormats.Text;
using Microsoft.KernelMemory.Diagnostics;
using Microsoft.KernelMemory.Pipeline;

namespace FastWiki.Service.Infrastructure.KM;

/// <summary>
/// QA问答处理器
/// </summary>
public class QAHandler : IPipelineStepHandler
{
    private readonly TextPartitioningOptions _options;
    private readonly IPipelineOrchestrator _orchestrator;
    private readonly WikiMemoryService _wikiMemoryService;
    private readonly ILogger<QAHandler> _log;
#pragma warning disable KMEXP00 // by design
    private readonly TextChunker.TokenCounter _tokenCounter;

    /// </inheritdoc>
    public QAHandler(
        string stepName,
        IPipelineOrchestrator orchestrator, WikiMemoryService wikiMemoryService,
        TextPartitioningOptions? options = null,
        ILogger<QAHandler>? log = null
    )
    {
        this.StepName = stepName;
        this._orchestrator = orchestrator;
        _wikiMemoryService = wikiMemoryService;
        this._options = options ?? new TextPartitioningOptions();
        this._options.Validate();

        this._log = log ?? DefaultLogger<QAHandler>.Instance;
        this._tokenCounter = DefaultGPTTokenizer.StaticCountTokens;
    }

    /// <inheritdoc />
    public string StepName { get; }

    /// <inheritdoc />
    public async Task<(bool success, DataPipeline updatedPipeline)> InvokeAsync(
        DataPipeline pipeline, CancellationToken cancellationToken = default)
    {
        this._log.LogDebug("Partitioning text, pipeline '{0}/{1}'", pipeline.Index, pipeline.DocumentId);

        if (pipeline.Files.Count == 0)
        {
            this._log.LogWarning("Pipeline '{0}/{1}': there are no files to process, moving to next pipeline step.",
                pipeline.Index, pipeline.DocumentId);
            return (true, pipeline);
        }

        foreach (DataPipeline.FileDetails uploadedFile in pipeline.Files)
        {
            // Track new files being generated (cannot edit originalFile.GeneratedFiles while looping it)
            Dictionary<string, DataPipeline.GeneratedFileDetails> newFiles = new();

            foreach (KeyValuePair<string, DataPipeline.GeneratedFileDetails> generatedFile in uploadedFile
                         .GeneratedFiles)
            {
                var file = generatedFile.Value;
                if (file.AlreadyProcessedBy(this))
                {
                    this._log.LogTrace("File {0} already processed by this handler", file.Name);
                    continue;
                }

                // Partition only the original text
                if (file.ArtifactType != DataPipeline.ArtifactTypes.ExtractedText)
                {
                    this._log.LogTrace("Skipping file {0} (not original text)", file.Name);
                    continue;
                }

                // Use a different partitioning strategy depending on the file type
                List<string> partitions = new();
                List<string> sentences = new();
                BinaryData partitionContent = await this._orchestrator
                    .ReadFileAsync(pipeline, file.Name, cancellationToken).ConfigureAwait(false);

                // Skip empty partitions. Also: partitionContent.ToString() throws an exception if there are no bytes.
                if (partitionContent.ToArray().Length == 0)
                {
                    continue;
                }

                switch (file.MimeType)
                {
                    case MimeTypes.PlainText:
                    case MimeTypes.MarkDown:
                    {
                        _log.LogDebug("Partitioning text file {0}", file.Name);
                        string content = partitionContent.ToString();

                        if (QuantizeBackgroundService.CacheWikiDetails.TryGetValue(StepName, out var wikiDetail))
                        {
                            await foreach (var item in OpenAIService
                                               .QaAsync(wikiDetail.Item1.QAPromptTemplate, content,
                                                   wikiDetail.Item2.Model, OpenAIOption.ChatToken,
                                                   OpenAIOption.ChatEndpoint, _wikiMemoryService)
                                               .WithCancellation(cancellationToken))
                            {
                                partitions.Add(item);
                                sentences.Add(item);
                            }
                        }


                        break;
                    }
                    default:
                        this._log.LogWarning("File {0} cannot be partitioned, type '{1}' not supported", file.Name,
                            file.MimeType);
                        // Don't partition other files
                        continue;
                }

                if (partitions.Count == 0)
                {
                    continue;
                }

                this._log.LogDebug("Saving {0} file partitions", partitions.Count);
                for (int partitionNumber = 0; partitionNumber < partitions.Count; partitionNumber++)
                {
                    // TODO: turn partitions in objects with more details, e.g. page number
                    string text = partitions[partitionNumber];
                    int sectionNumber = 0; // TODO: use this to store the page number (if any)
                    BinaryData textData = new(text);

                    int tokenCount = this._tokenCounter(text);
                    this._log.LogDebug("Partition size: {0} tokens", tokenCount);

                    var destFile = uploadedFile.GetPartitionFileName(partitionNumber);
                    await this._orchestrator.WriteFileAsync(pipeline, destFile, textData, cancellationToken)
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
                        ContentSHA256 = ComputeSha256Hash(text),
                    };
                    newFiles.Add(destFile, destFileDetails);
                    destFileDetails.MarkProcessedBy(this);
                }

                file.MarkProcessedBy(this);
            }

            // Add new files to pipeline status
            foreach (var file in newFiles)
            {
                uploadedFile.GeneratedFiles.Add(file.Key, file.Value);
            }
        }

        return (true, pipeline);
    }
    
    
    static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}