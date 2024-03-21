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

    public async Task UpdateAsync(FastModel model)
    {
        await Context.FastModels.Where(x => x.Id == model.Id)
            .ExecuteUpdateAsync(item =>
                item.SetProperty(item => item.Name, model.Name)
                    .SetProperty(item => item.Type, model.Type)
                    .SetProperty(item => item.Url, model.Url)
                    .SetProperty(item => item.ApiKey, model.ApiKey)
                    .SetProperty(item => item.Description, model.Description)
                    .SetProperty(item => item.Models, model.Models)
                    .SetProperty(item => item.Order, model.Order));
    }

    public async Task EnableAsync(string id, bool enable)
    {
        await Context.FastModels.Where(x => x.Id == id)
            .ExecuteUpdateAsync(item => item.SetProperty(x => x.Enable, enable));
    }

    public async Task FastModelComputeTokenAsync(string id, int requestToken, int completeToken)
    {
        var total = completeToken + requestToken;
        await Context.FastModels.Where(x => x.Id == id)
            .ExecuteUpdateAsync(item =>
                item.SetProperty(x => x.UsedQuota, x => x.UsedQuota + total));
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