namespace FastWiki.Service.Domain.Users.Repositories;

/// <summary>
///     用户仓储
/// </summary>
public interface IUserRepository : IRepository<User, Guid>
{
    /// <summary>
    ///     获取用户列表
    /// </summary>
    Task<List<User>> GetListAsync(string? keyword, int page, int pageSize);

    /// <summary>
    ///     获取用户数量
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<long> GetCountAsync(string? keyword);

    /// <summary>
    ///     删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    ///     禁用用户/启用用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="disable">是否禁用</param>
    /// <returns></returns>
    Task<bool> DisableAsync(Guid id, bool disable);

    /// <summary>
    ///     修改角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task UpdateRoleAsync(Guid id, RoleType role);

    /// <summary>
    ///     验证账户是否存在
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<bool> IsExistAccountAsync(string account);
    
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task InsertAsync(User user);
}