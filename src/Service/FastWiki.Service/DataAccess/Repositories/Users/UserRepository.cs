using FastWiki.Service.Domain.Users.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Users;

public sealed class UserRepository : Repository<WikiDbContext, User, Guid>, IUserRepository
{
    public UserRepository(WikiDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<List<User>> GetListAsync(string? keyword, int page, int pageSize)
    {
        var query = GetQuery(keyword);
        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<long> GetCountAsync(string? keyword)
    {
        var query = GetQuery(keyword);
        return await query.LongCountAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // 管理员不能删除
        return (await Context.Users.Where(x => x.Id == id && x.Role != RoleType.Admin).ExecuteDeleteAsync()) > 1;
    }

    public async Task<bool> DisableAsync(Guid id, bool disable)
    {
        // 管理员不能禁用
        return (await Context.Users.Where(x => x.Id == id && x.Role != RoleType.Admin)
            .ExecuteUpdateAsync(item => item.SetProperty(x => x.IsDisable, disable))) > 0;
    }

    public async Task UpdateRoleAsync(Guid id, RoleType role)
    {
        await Context.Users.Where(x => x.Id == id).ExecuteUpdateAsync(item =>
            item.SetProperty(x => x.Role, role));
    }

    private IQueryable<User> GetQuery(string? keyword)
    {
        var query = Context.Users.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Account.Contains(keyword));
        }

        return query;
    }
}