namespace FastWiki.Service.DataAccess.Repositories.Wikis;

public sealed class WikiRepository(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, Wiki, long>(context, unitOfWork), IWikiRepository
{
    
}