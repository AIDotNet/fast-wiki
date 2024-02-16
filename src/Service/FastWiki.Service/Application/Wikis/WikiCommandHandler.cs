namespace FastWiki.Service.Application.Wikis;

public sealed class WikiCommandHandler(IWikiRepository wikiRepository)
{
    [EventHandler]
    public async Task CreateWiki(CreateWikiCommand command)
    {
        var wiki = new Wiki(command.Input.Name, command.Input.Icon, command.Input.Model);
        await wikiRepository.AddAsync(wiki);
    }
}