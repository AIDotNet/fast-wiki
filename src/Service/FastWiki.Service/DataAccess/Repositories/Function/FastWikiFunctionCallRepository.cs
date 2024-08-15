using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Function.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Function;

public sealed class FastWikiFunctionCallRepository : Repository<WikiDbContext, FastWikiFunctionCall, long>,
    IFastWikiFunctionCallRepository
{
    public FastWikiFunctionCallRepository(WikiDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<List<FastWikiFunctionCall>> GetFunctionListAsync(Guid? userId, int page, int pageSize)
    {
        var query = CreateQuery(userId);
        return await query
            .OrderByDescending(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<long> GetFunctionCountAsync(Guid? userId)
    {
        var query = CreateQuery(userId);
        return await query.LongCountAsync();
    }

    public async Task EnableFunctionCallAsync(long id, bool enable)
    {
        await Context.FunctionCalls.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(i => i.Enable, enable));
    }

    public async Task<List<FastWikiFunctionCall>> GetFunctionListAsync(Guid? userId)
    {
        var query = CreateQuery(userId);
        return await query.Where(x => x.Enable)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
    }

    public async Task RemoveAsync(long id)
    {
        await Context.FunctionCalls.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task InsertAsync(FastWikiFunctionCall functionCall)
    {
        await Context.FunctionCalls.AddAsync(functionCall);

        await Context.SaveChangesAsync();
    }

    private IQueryable<FastWikiFunctionCall> CreateQuery(Guid? userId)
    {
        var query = Context.FunctionCalls.AsQueryable();
        if (userId.HasValue) query = query.Where(x => x.Creator == userId);

        return query;
    }
}