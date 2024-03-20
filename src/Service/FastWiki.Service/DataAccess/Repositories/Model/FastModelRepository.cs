using FastWiki.Service.Domain.Model.Aggregates;
using FastWiki.Service.Domain.Model.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Model;

public sealed class FastModelRepository : Repository<WikiDbContext, FastModel, string>, IFastModelRepository
{
    public FastModelRepository(WikiDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }


    public async Task<List<FastModel>> GetModelListAsync(string keyword, int page, int pageSize)
    {
        var query = CreateModelQuery(keyword);
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<long> GetModelCountAsync(string keyword)
    {
        var query = CreateModelQuery(keyword);
        return await query.LongCountAsync();
    }

    public Task<bool> ExistAsync(string name)
    {
        return Context.FastModels.AsNoTracking().AnyAsync(x => x.Name == name);
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var result = await Context.FastModels.Where(x => x.Id == id).ExecuteDeleteAsync();

        return result > 0;
    }

    private IQueryable<FastModel> CreateModelQuery(string keyword)
    {
        var query = Context.FastModels.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x =>
                x.Name.Contains(keyword) || x.Type.Contains(keyword) || x.Description.Contains(keyword));
        }

        return query.AsNoTracking();
    }
}