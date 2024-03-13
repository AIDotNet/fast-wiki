namespace FastWiki.Service.DataAccess.Repositories.Wikis;

/// <inheritdoc />
public sealed class WikiRepository(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, Wiki, long>(context, unitOfWork), IWikiRepository
{
    /// <inheritdoc />
    public Task<List<Wiki>> GetListAsync(string? keyword, int page, int pageSize)
    {
        var query = CreateQuery(keyword);
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
    public Task<long> GetCountAsync(string? keyword)
    {
        var query = CreateQuery(keyword);
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
        return Context.WikiDetails
            .Where(x => x.State == WikiQuantizationState.Fail || x.State == WikiQuantizationState.None).ToListAsync();
    }

    public async Task RemoveDetailsVectorAsync(string index, string id)
    {
        await Context.Database.ExecuteSqlRawAsync(
            $"delete from \"{ConnectionStringsOptions.TableNamePrefix + index}\" where id='{id}';");
    }


    private IQueryable<Wiki> CreateQuery(string? keyword)
    {
        var query = Context.Wikis.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.Name.Contains(keyword));
        }

        return query;
    }

    private IQueryable<WikiDetail> CreateDetailsQuery(long wikiId, WikiQuantizationState? queryState, string? keyword)
    {
        var query = Context.WikiDetails.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.FileName.Contains(keyword));
        }

        if (queryState.HasValue)
            query = query.Where(x => x.State == queryState.Value);

        query = query.Where(x => x.WikiId == wikiId);

        return query;
    }
}