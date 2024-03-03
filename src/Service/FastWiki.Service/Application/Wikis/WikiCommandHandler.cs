using FastWiki.Service.Application.Storage.Queries;
using FastWiki.Service.Backgrounds;
using FastWiki.Service.Service;

namespace FastWiki.Service.Application.Wikis;

public sealed class WikiCommandHandler(
    IWikiRepository wikiRepository,
    WikiMemoryService wikiMemoryService,
    MemoryServerless memoryServerless,
    IMapper mapper,
    IEventBus eventBus)
{
    [EventHandler]
    public async Task CreateWiki(CreateWikiCommand command)
    {
        var wiki = new Wiki(command.Input.Icon, command.Input.Name, command.Input.Model);
        await wikiRepository.AddAsync(wiki);
    }

    [EventHandler]
    public async Task RemoveWiki(RemoveWikiCommand command)
    {
        var wikiDetailsQuery = new WikiDetailsQuery(command.Id, string.Empty, 1, int.MaxValue);

        await eventBus.PublishAsync(wikiDetailsQuery);

        await wikiRepository.RemoveAsync(command.Id);

        var ids = wikiDetailsQuery.Result.Result.Select(x => x.Id).ToList();

        await wikiRepository.RemoveDetailsAsync(ids);

        foreach (var id in ids)
        {
            try
            {
                await memoryServerless.DeleteDocumentAsync(id.ToString(), "wiki");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    [EventHandler]
    public async Task CreateWikiDetailsAsync(CreateWikiDetailsCommand command)
    {
        // var serverless = wikiMemoryService.CreateMemoryServerless(new SearchClientConfig()
        // {
        //     MaxAskPromptSize = 128000,
        //     MaxMatchesCount = 3,
        //     AnswerTokens = 2000,
        //     EmptyAnswer = "知识库未搜索到相关内容"
        // }, command.Input.Mode == ProcessMode.Auto ? 512 : command.Input.Subsection);

        var wikiDetail = new WikiDetail(command.Input.WikiId, command.Input.Name, command.Input.FilePath,
            command.Input.FileId, 0, "file");

        wikiDetail = await wikiRepository.AddDetailsAsync(wikiDetail);

        await QuantizeBackgroundService.AddWikiDetailAsync(wikiDetail);
        
        //
        // var fileInfoQuery = new StorageInfoQuery(command.Input.FileId);
        //
        // await eventBus.PublishAsync(fileInfoQuery);
        //
        // wikiDetails = await wikiRepository.AddDetailsAsync(wikiDetails);
        //
        // try
        // {
        //     var result = await serverless.ImportDocumentAsync(fileInfoQuery.Result.FullName,
        //         wikiDetails.Id.ToString(),
        //         tags: new TagCollection()
        //         {
        //             {
        //                 "wikiId", command.Input.WikiId.ToString()
        //             },
        //             {
        //                 "fileId", command.Input.FileId.ToString()
        //             },
        //             {
        //                 "wikiDetailId", wikiDetails.Id.ToString()
        //             }
        //         }, "wiki");
        // }
        // catch (Exception e)
        // {
        //     await wikiRepository.RemoveDetailsAsync(wikiDetails.Id);
        // }
    }

    [EventHandler]
    public async Task RemoveWikiDetailsCommand(RemoveWikiDetailsCommand command)
    {
        await wikiRepository.RemoveDetailsAsync(command.Id);

        try
        {
            await memoryServerless.DeleteDocumentAsync(command.Id.ToString(), "wiki");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [EventHandler]
    public async Task RemoveWikiDetailVectorQuantityAsync(RemoveWikiDetailVectorQuantityCommand command)
    {
        await memoryServerless.DeleteDocumentAsync(command.DocumentId, "wiki");
    }

    [EventHandler]
    public async Task UpdateWikiAsync(UpdateWikiCommand command)
    {
        var wiki = mapper.Map<Wiki>(command.Dto);
        await wikiRepository.UpdateAsync(wiki);
    }
}