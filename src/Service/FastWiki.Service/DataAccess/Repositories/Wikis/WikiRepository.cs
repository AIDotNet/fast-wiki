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

    /// <inheritdoc />
    public Task<long> GetCountAsync(string? keyword)
    {
        var query = CreateQuery(keyword);
        return query.LongCountAsync();
    }

    public async Task<List<WikiDetail>> GetDetailsListAsync(long wikiId, string? keyword, int page, int pageSize)
    {
        var query = CreateDetailsQuery(wikiId, keyword);
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<long> GetDetailsCountAsync(long wikiId, string? keyword)
    {
        var query = CreateDetailsQuery(wikiId, keyword);
        return await query.LongCountAsync();
    }

    /// <inheritdoc />
    public async Task<WikiDetail> AddDetailsAsync(WikiDetail wikiDetail)
    {
        var result = await Context.WikiDetails.AddAsync(wikiDetail);

        await Context.SaveChangesAsync();

        return result.Entity;
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

    private IQueryable<WikiDetail> CreateDetailsQuery(long wikiId, string? keyword)
    {
        var query = Context.WikiDetails.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => x.FileName.Contains(keyword));
        }

        query = query.Where(x => x.WikiId == wikiId);

        return query;
    }
}