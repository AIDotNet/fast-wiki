namespace FastWiki.Service.Application.Wikis;

public sealed class WikiCommandHandler(IWikiRepository wikiRepository)
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
}