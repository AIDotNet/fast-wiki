using Microsoft.KernelMemory;

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

        foreach (var item in command.Input.Lins)
        {
            var memoryResult = await memoryServerless.ImportTextAsync(item, wikiDetails.FileId.ToString(),
                new TagCollection()
                {
                    {
                        "wikiId", command.Input.WikiId.ToString()
                    }
                }, "wiki");
        }
    }
}