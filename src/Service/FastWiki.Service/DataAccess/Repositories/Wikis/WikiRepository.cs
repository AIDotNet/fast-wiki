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

    public async Task RemoveDetailsAsync(long wikiDetailId)
    {
        var entity = await Context.WikiDetails.FindAsync(wikiDetailId);

        if (entity != null)
            Context.WikiDetails.Remove(entity);
    }

    public async Task<WikiDetail> GetDetailsAsync(long wikiDetailId)
    {
        return await Context.WikiDetails.FindAsync(wikiDetailId);
    }

    public async Task<IEnumerable<WikiDetailsDocument>> GetWikiDetailsDocumentListAsync(long wikiDetailsId, int page, int pageSize)
    {
        var query = CreateDetailsDocumentsQuery(wikiDetailsId);

        return  await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetWikiDetailsDocumentCountAsync(long wikiDetailsId)
    {
        var query = CreateDetailsDocumentsQuery(wikiDetailsId);

        return await query.CountAsync();
    }

    public async Task AddWikiDetailsDocumentAsync(IEnumerable<WikiDetailsDocument> detailsDocuments)
    {
        await Context.WikiDetailsDocuments.AddRangeAsync(detailsDocuments);
    }

    private IQueryable<WikiDetailsDocument> CreateDetailsDocumentsQuery(long wikiDetailsId)
    {
        var query = Context.WikiDetailsDocuments.AsNoTracking();

        return query.Where(x => x.WikiDetailsId == wikiDetailsId);
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