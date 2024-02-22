namespace FastWiki.Service.Application.Wikis;

public sealed class WikiCommandHandler(IWikiRepository wikiRepository, MemoryServerless memoryServerless)
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
        await wikiRepository.RemoveAsync(command.Id);
    }

    [EventHandler]
    public async Task CreateWikiDetailsAsync(CreateWikiDetailsCommand command)
    {
        var wikiDetails = new WikiDetail(command.Input.WikiId, command.Input.Name, command.Input.FilePath,
            command.Input.FileId, command.Input.Lins.Count(), "file");

        wikiDetails = await wikiRepository.AddDetailsAsync(wikiDetails);

        try
        {
            var documents = new List<WikiDetailsDocument>();
            foreach (var item in command.Input.Lins)
            {
                var documentId = Guid.NewGuid().ToString();
                documents.Add(new WikiDetailsDocument()
                {
                    DocumentId = documentId,
                    WikiDetailsId = wikiDetails.WikiId
                });
                var memoryResult = await memoryServerless.ImportTextAsync(item, documentId,
                    new TagCollection()
                    {
                        {
                            "wikiId", command.Input.WikiId.ToString()
                        },
                        {
                            "documentId", documentId
                        },
                        {
                            "fileId", command.Input.FileId.ToString()
                        },
                        {
                            "wikiDetailId", wikiDetails.Id.ToString()
                        }
                    }, "wiki");
            }

            await wikiRepository.AddWikiDetailsDocumentAsync(documents);
        }
        catch (Exception e)
        {
            await wikiRepository.RemoveDetailsAsync(wikiDetails.Id);
        }
    }

    [EventHandler]
    public async Task RemoveWikiDetailsCommand(RemoveWikiDetailsCommand command)
    {
        await wikiRepository.RemoveDetailsAsync(command.Id);

        foreach (var wikiDetail in await wikiRepository.GetDetailsListAsync(command.Id, null, 1, int.MaxValue))
        {
            await wikiRepository.RemoveDetailsAsync(wikiDetail.Id);

            foreach (var detailsDocument in await wikiRepository.GetWikiDetailsDocumentListAsync(wikiDetail.Id, 1,
                         int.MaxValue))
            {
                await memoryServerless.DeleteDocumentAsync(detailsDocument.DocumentId, "wiki");
            }
        }
    }

    [EventHandler]
    public async Task RemoveWikiDetailVectorQuantityAsync(RemoveWikiDetailVectorQuantityCommand command)
    {
        await memoryServerless.DeleteDocumentAsync(command.DocumentId, "wiki");
    }
}