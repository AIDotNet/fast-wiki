namespace FastWiki.Service.DataAccess.Repositories.Wikis;

/// <inheritdoc />
public sealed class WikiRepository(
    WikiDbContext context,
    IConfiguration configuration,
    IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, Wiki, long>(context, unitOfWork), IWikiRepository
{
    /// <inheritdoc />
    public Task<List<Wiki>> GetListAsync(Guid userId, string? keyword, int page, int pageSize)
    {
        var query = CreateQuery(keyword, userId);
        return query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task UpdateAsync(Wiki wiki)
    {
        await Context.Wikis.Where(x => x.Id == wiki.Id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(b => b.Name, b => wiki.Name)
                    .SetProperty(b => b.Model, b => wiki.Model)
                    .SetProperty(b => b.EmbeddingModel, b => wiki.EmbeddingModel));
    }

    /// <inheritdoc />
    public Task<long> GetCountAsync(Guid userId, string? keyword)
    {
        var query = CreateQuery(keyword, userId);
        return query.LongCountAsync();
    }

    public async Task<List<WikiDetail>> GetDetailsListAsync(long wikiId, WikiQuantizationState? queryState,
        string? keyword, int page, int pageSize)
    {
        var query = CreateDetailsQuery(wikiId, queryState, keyword);
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<long> GetDetailsCountAsync(long wikiId, WikiQuantizationState? queryState, string? keyword)
    {
        var query = CreateDetailsQuery(wikiId, queryState, keyword);
        return await query.LongCountAsync();
    }

    /// <inheritdoc />
    public async Task<WikiDetail> AddDetailsAsync(WikiDetail wikiDetail)
    {
        var result = await Context.WikiDetails.AddAsync(wikiDetail);

        await Context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task RemoveDetailsAsync(long wikiDetailId)
    {
        await Context.WikiDetails.Where(x => x.Id == wikiDetailId).ExecuteDeleteAsync();
    }

    public async Task<WikiDetail> GetDetailsAsync(long wikiDetailId)
    {
        return await Context.WikiDetails.FindAsync(wikiDetailId);
    }

    public async Task RemoveDetailsAsync(List<long> wikiDetailIds)
    {
        await Context.WikiDetails.Where(x => wikiDetailIds.Contains(x.Id)).ExecuteDeleteAsync();
    }

    public Task UpdateDetailsState(long wikiDetailId, WikiQuantizationState state)
    {
        return Context.WikiDetails.Where(x => x.Id == wikiDetailId)
            .ExecuteUpdateAsync(s => s.SetProperty(b => b.State, b => state));
    }

    public Task<List<WikiDetail>> GetFailedDetailsAsync()
    {
        var query = Context.QuantizedLists
            .Where(x => x.State == QuantizedListState.Fail || x.State == QuantizedListState.Pending)
            .Select(x => x.WikiDetailId);

        return Context.WikiDetails.Where(x => query.Contains(x.Id)).ToListAsync();
    }

    public async Task RemoveDetailsVectorAsync(string index, string id)
    {
        if (configuration.GetConnectionString("DefaultConnection").IsNullOrEmpty())
        {
            // TODO: 磁盘不支持删除单个向量
        }
        else
        {
            await Context.Database.ExecuteSqlRawAsync(
                $"delete from \"{ConnectionStringsOptions.TableNamePrefix + index}\" where id='{id}';");
        }
    }

    public Task DetailsRenameNameAsync(long id, string name)
    {
        return Context.WikiDetails.Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(b => b.FileName, b => name));
    }

    public async Task<Wiki> WikiDetailGetWikiAsync(long wikiDetailId)
    {
        var query = await Context.WikiDetails.Where(x => x.Id == wikiDetailId).Select(x => x.WikiId)
            .FirstOrDefaultAsync();

        return await Context.Wikis.FindAsync(query);
    }

    public async Task<long> CreateQuantizationListAsync(long wikiId, long wikiDetailId, string remark)
    {
        // 判断是否已经存在
        var exist = await Context.QuantizedLists.AnyAsync(x => x.WikiId == wikiId && x.WikiDetailId == wikiDetailId);

        if (exist)
        {
            var entity = await Context.QuantizedLists.Where(x => x.WikiId == wikiId && x.WikiDetailId == wikiDetailId)
                .FirstOrDefaultAsync();

            await Context.QuantizedLists
                .Where(x => x.Id == entity.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(b => b.State, QuantizedListState.Pending)
                    .SetProperty(b => b.Remark, b => remark)
                    .SetProperty(b => b.ProcessTime, b => null));

            return entity.Id;
        }
        else
        {
            var entity = await Context.QuantizedLists.AddAsync(new QuantizedList()
            {
                WikiId = wikiId,
                WikiDetailId = wikiDetailId,
                Remark = remark,
                CreationTime = DateTime.Now,
                State = QuantizedListState.Pending,
            });

            await Context.SaveChangesAsync();

            return entity.Entity.Id;
        }
    }

    public async Task CompleteQuantizationListAsync(long id, string remark, QuantizedListState state)
    {
        await Context.QuantizedLists.Where(x => x.Id == id)
            .ExecuteUpdateAsync(s => s.SetProperty(b => b.State, state)
                .SetProperty(b => b.Remark, b => remark)
                .SetProperty(b => b.CreationTime, b => DateTime.Now)
                .SetProperty(b => b.ProcessTime, b => DateTime.Now));
    }

    public Task<List<QuantizedList>> GetQuantizedListAsync(long wikiId, QuantizedListState? state, int page,
        int pageSize)
    {
        var query = Context.QuantizedLists
            .Where(x => x.WikiId == wikiId);

        if (state.HasValue)
        {
            query = query.Where(x => x.State == state.Value);
        }

        return query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public Task<long> GetQuantizedListCountAsync(long wikiId, QuantizedListState? state)
    {
        var query = Context.QuantizedLists
            .Where(x => x.WikiId == wikiId);

        if (state.HasValue)
        {
            query = query.Where(x => x.State == state.Value);
        }

        return query.LongCountAsync();
    }

    public Task<List<WikiDetail>> GetDetailsByIdsAsync(List<long> wikiDetailIds)
    {
        return Context.WikiDetails.Where(x => wikiDetailIds.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();
    }


    private IQueryable<Wiki> CreateQuery(string? keyword, Guid userId)
    {
        var query = Context.Wikis
            .AsNoTracking()
            .Where(x => x.Creator == userId);

        if (!string.IsNullOrWhiteSpace(keyword)) query = query.Where(x => x.Name.Contains(keyword));

        return query;
    }

    private IQueryable<WikiDetail> CreateDetailsQuery(long wikiId, WikiQuantizationState? queryState, string? keyword)
    {
        var query = Context.WikiDetails.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword)) query = query.Where(x => x.FileName.Contains(keyword));

        if (queryState.HasValue)
            query = query.Where(x => x.State == queryState.Value);

        query = query.Where(x => x.WikiId == wikiId);

        return query;
    }
}