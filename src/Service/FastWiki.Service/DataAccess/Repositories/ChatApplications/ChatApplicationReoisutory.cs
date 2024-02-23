namespace FastWiki.Service.DataAccess.Repositories.ChatApplications;

public class ChatApplicationReoisutory(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, ChatApplication, string>(context, unitOfWork), IChatApplicationRepository
{
    public Task<List<ChatApplication>> GetListAsync(int page, int pageSize)
    {
        var query = CreateQueryable();
        
        return query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<long> GetCountAsync()
    {
        var query = CreateQueryable();
        
        return query.LongCountAsync();
    }

    private IQueryable<ChatApplication> CreateQueryable()
    {
        return Context.ChatApplications;
    }
}