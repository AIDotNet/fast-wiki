using FastWiki.Service.DataAccess;
using FastWiki.Service.Dto;
using FastWiki.Service.Entities;
using FastWiki.Service.Input;
using Masa.Utils.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FastWiki.Service.Services;

public class UserService(MasterDbContext masterDbContext) : ApplicationService<UserService>
{
    [Authorize]
    public async Task CreateAsync(CreateUserInput input)
    {
        if (await masterDbContext.Users.AnyAsync(x => x.Account == input.Account))
        {
            throw new UserFriendlyException("Account already exists");
        }


        var user = new User(input.Account, input.Name, input.Password, input.Avatar, input.Email, null, false);

        user.SetUserRole();

        await masterDbContext.Users.AddAsync(user);

        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task RemoveAsync(Guid id)
    {
        var user = await masterDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            throw new UserFriendlyException("User not found");
        }

        masterDbContext.Users.Remove(user);

        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task<PaginatedListBase<UserDto>> GetListAsync(int page, int pageSize)
    {
        var query = masterDbContext.Users;

        var list = await query
            .OrderBy(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await query.CountAsync();

        return new PaginatedListBase<UserDto>
        {
            Total = total,
            Result = Mapper.Map<List<UserDto>>(list)
        };
    }
}