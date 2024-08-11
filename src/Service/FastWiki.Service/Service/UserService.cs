using System.Text.RegularExpressions;
using FastWiki.Service.Contracts.Users.Dto;
using FastWiki.Service.Domain.Users.Repositories;
using IMapper = MapsterMapper.IMapper;

namespace FastWiki.Service.Service;

public sealed class UserService(IUserRepository userRepository, IMapper mapper) : ApplicationService<UserService>
{
    [Authorize]
    public async Task<ResultDto> ChangePasswordAsync(string password, string newPassword)
    {
        var user = await userRepository.FindAsync(UserContext.GetUserId<Guid>());

        if (user == null)
        {
            throw new UserFriendlyException("用户不存在");
        }

        if (!user.CheckCipher(password))
        {
            throw new UserFriendlyException("密码错误");
        }

        user.SetPassword(newPassword);

        await userRepository.UpdateAsync(user);

        return new ResultDto();
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task<PaginatedListBase<UserDto>> GetUsersAsync(string? keyword, int page, int pageSize)
    {
        var list = await userRepository.GetListAsync(keyword, page, pageSize);

        var total = await userRepository.GetCountAsync(keyword);

        return new PaginatedListBase<UserDto>
        {
            Total = total,
            Result = mapper.Map<List<UserDto>>(list)
        };
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task DeleteUserAsync(Guid id)
    {
        if (id == UserContext.GetUserId<Guid>())
            throw new UserFriendlyException("不能删除自己");

        await userRepository.DeleteAsync(id);
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task DisableUserAsync(Guid id, bool disable)
    {
        if (id == UserContext.GetUserId<Guid>())
            throw new UserFriendlyException("不能禁用自己");

        await userRepository.DisableAsync(id, disable);
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task UpdateRoleAsync(Guid id, RoleType role)
    {
        await userRepository.UpdateRoleAsync(id, role);
    }

    public async Task CreateAsync(CreateUserInput input)
    {
        // 校验账号和密码长度
        if (input.Account.Length is < 6 or > 20)
            throw new UserFriendlyException("账号长度必须在6-20之间");

        if (input.Password.Length < 6 || input.Password.Length > 20)
            throw new UserFriendlyException("密码长度必须在6-20之间");

        // 校验邮箱格式
        if (!Regex.IsMatch(input.Email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            throw new UserFriendlyException("邮箱格式错误");

        // TODO: 验证账号是否存在
        if (await userRepository.IsExistAccountAsync(input.Account))
            throw new UserFriendlyException("账号已存在");

        var user = new User(input.Account, input.Name, input.Password,
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", input.Email, input.Phone,
            false);

        await userRepository.AddAsync(user);
    }

    [Authorize]
    public async ValueTask<UserDto> GetAsync()
    {
        var result = await userRepository.FindAsync(UserContext.GetUserId<Guid>());
        if (result == null)
        {
            throw new UnauthorizedAccessException("用户不存在");
        }

        return Mapper.Map<UserDto>(result);
    }
}