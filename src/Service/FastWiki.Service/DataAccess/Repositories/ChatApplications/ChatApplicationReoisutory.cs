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

    public async Task CreateChatDialogAsync(ChatDialog chatDialog)
    {
        await Context.ChatDialogs.AddAsync(chatDialog);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveChatDialogAsync(string id)
    {
        await Context.ChatDialogs.Where(x => x.Id == id).ExecuteDeleteAsync();
        await Context.ChatDialogHistorys.Where(x => x.ChatDialogId == id).ExecuteDeleteAsync();

        await Context.SaveChangesAsync();
    }

    public async Task<List<ChatDialog>> GetChatDialogListAsync(string applicationId, bool all, Guid userId)
    {
        var query = Context.ChatDialogs.Where(x => x.ApplicationId == applicationId && x.Creator == userId);

        if (!all)
        {
            query = query.Where(x => x.Type == ChatDialogType.ChatApplication);
        }

        return await query
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task<List<ChatDialog>> GetChatShareDialogListAsync(string chatId)
    {
        return await Context.ChatDialogs.Where(x => x.ChatId == chatId)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task CreateChatDialogHistoryAsync(ChatDialogHistory chatDialogHistory)
    {
        await Context.ChatDialogHistorys.AddAsync(chatDialogHistory);

        await Context.SaveChangesAsync();
    }

    public async Task<List<ChatDialogHistory>> GetChatDialogHistoryListAsync(string chatDialogId, int page,
        int pageSize)
    {
        var query = CreateChatDialogHistoriesQueryable(chatDialogId);

        return await query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<long> GetChatDialogHistoryCountAsync(string chatDialogId)
    {
        var query = CreateChatDialogHistoriesQueryable(chatDialogId);

        return await query.LongCountAsync();
    }

    public async Task RemoveChatDialogHistoryAsync(string chatDialogId)
    {
        await Context.ChatDialogHistorys.Where(x => x.ChatDialogId == chatDialogId).ExecuteDeleteAsync();
    }

    public async Task RemoveChatDialogHistoryByIdAsync(string id)
    {
        await Context.ChatDialogHistorys.Where(x => x.Id == id).ExecuteDeleteAsync();
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

    public async Task UpdateChatDialogAsync(ChatDialog chatDialog)
    {
        await Context.ChatDialogs.Where(x => x.Id == chatDialog.Id).ExecuteUpdateAsync(x =>
            x.SetProperty(b => b.Name, chatDialog.Name));
    }

    public async Task RemoveShareDialogAsync(string chatId, string id)
    {
        await Context.ChatDialogs.Where(x => x.ChatId == chatId && x.Id == id).ExecuteDeleteAsync();
        await Context.ChatDialogHistorys.Where(x => x.ChatDialogId == id).ExecuteDeleteAsync();
    }

    public async Task UpdateShareDialogAsync(ChatDialog chatDialog)
    {
        await Context.ChatDialogs.Where(x => x.ChatId == chatDialog.ChatId && x.Id == chatDialog.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(b => b.Name, chatDialog.Name));
    }

    public Task<List<ChatDialog>> GetSessionLogDialogListAsync(Guid userId, string chatApplicationId, int page,
        int pageSize)
    {
        var query = Context.ChatDialogs
            .AsNoTracking()
            .Where(x => x.ApplicationId == chatApplicationId && x.Creator == userId);

        return query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<long> GetSessionLogDialogCountAsync(Guid userId, string chatApplicationId)
    {
        var query = Context.ChatDialogs
            .AsNoTracking()
            .Where(x => x.ApplicationId == chatApplicationId && x.Creator == userId);

        return await query.LongCountAsync();
    }

    public async Task PutChatHistoryAsync(string id, string content, string? chatShareId)
    {
        if (chatShareId.IsNullOrEmpty())
        {
            await Context.ChatDialogHistorys.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.Content, content));
        }
        else
        {
            var result = await Context.ChatShares.FirstOrDefaultAsync(x => x.Id == chatShareId);

            if (result == null)
            {
                throw new UserFriendlyException("分享对话不存在");
            }

            await Context.ChatDialogHistorys.Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.Content, content));
        }
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

    public Task<ChatDialogHistory> GetChatDialogHistoryAsync(string id)
    {
        return Context.ChatDialogHistorys.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    private IQueryable<ChatShare> CreateChatShareQueryable(Guid userId, string chatApplicationId)
    {
        return Context.ChatShares.AsNoTracking()
            .Where(x => x.ChatApplicationId == chatApplicationId && x.Creator == userId);
    }

    private IQueryable<ChatDialogHistory> CreateChatDialogHistoriesQueryable(string chatDialogId)
    {
        return Context.ChatDialogHistorys.Where(x => x.ChatDialogId == chatDialogId);
    }


    private IQueryable<ChatApplication> CreateQueryable(Guid userId)
    {
        return Context.ChatApplications
            .AsNoTracking().Where(x => x.Creator == userId);
    }
}