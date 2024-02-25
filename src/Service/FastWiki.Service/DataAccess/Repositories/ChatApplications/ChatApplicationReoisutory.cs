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

    public async Task CreateChatDialogAsync(ChatDialog chatDialog)
    {
        await Context.ChatDialogs.AddAsync(chatDialog);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveChatDialogAsync(string id)
    {
        var entity =  await Context.ChatDialogs.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            Context.ChatDialogs.Remove(entity);
        }
    }

    public async Task<List<ChatDialog>> GetChatDialogListAsync()
    {
        return await Context.ChatDialogs.ToListAsync();
    }

    private IQueryable<ChatApplication> CreateQueryable()
    {
        return Context.ChatApplications;
    }
}