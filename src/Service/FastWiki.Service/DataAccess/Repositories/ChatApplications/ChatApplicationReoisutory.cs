namespace FastWiki.Service.DataAccess.Repositories.ChatApplications;

public sealed class ChatApplicationReoisutory(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, ChatApplication, string>(context, unitOfWork), IChatApplicationRepository
{
    public Task<List<ChatApplication>> GetListAsync(int page, int pageSize, Guid userId)
    {
        var query = CreateQueryable(userId);

        return query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<long> GetCountAsync(Guid userId)
    {
        var query = CreateQueryable(userId);

        return query.LongCountAsync();
    }

    public async Task CreateChatShareAsync(ChatShare share)
    {
        await Context.ChatShares.AddAsync(share);
        await Context.SaveChangesAsync();
    }

    public async Task<List<ChatShare>> GetChatShareListAsync(Guid userId, string chatApplicationId, int page,
        int pageSize)
    {
        var query = CreateChatShareQueryable(userId, chatApplicationId);

        return await query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<long> GetChatShareCountAsync(Guid userId, string chatApplicationId)
    {
        var query = CreateChatShareQueryable(userId, chatApplicationId);

        return await query.LongCountAsync();
    }

    public async Task<ChatShare> GetChatShareAsync(string id)
    {
        return await Context.ChatShares.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ChatApplication> ChatShareApplicationAsync(string chatShareId)
    {
        var query =
            from share in Context.ChatShares
            join application in Context.ChatApplications on share.ChatApplicationId equals application.Id
            where share.Id == chatShareId
            select application;

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }


    public async Task<ChatShare> GetAPIKeyChatShareAsync(string apiKey)
    {
        return await Context.ChatShares
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.APIKey == apiKey);
    }

    public Task DeductTokenAsync(string chatShareId, int token)
    {
        return Context.ChatShares.Where(x => x.Id == chatShareId)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(b => b.UsedToken, x => x.UsedToken + token));
    }

    public async Task RemoveChatShareAsync(string id)
    {
        await Context.ChatShares
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task CreateChatRecordAsync(ChatRecord chatRecord)
    {
        await Context.ChatRecords.AddAsync(chatRecord);
    }

    private IQueryable<ChatShare> CreateChatShareQueryable(Guid userId, string chatApplicationId)
    {
        return Context.ChatShares.AsNoTracking()
            .Where(x => x.ChatApplicationId == chatApplicationId && x.Creator == userId);
    }

    private IQueryable<ChatApplication> CreateQueryable(Guid userId)
    {
        return Context.ChatApplications
            .AsNoTracking().Where(x => x.Creator == userId);
    }
}