using FastWiki.Service.Application.Users.Queries;
using FastWiki.Service.Contracts.Users.Dto;
using FastWiki.Service.Domain.Users.Repositories;

namespace FastWiki.Service.Application.Users;

public sealed class UserQueryHandler(IUserRepository userRepository, IMapper mapper)
{
    [EventHandler]
    public async Task UserListAsync(UserListQuery query)
    {
        var list = await userRepository.GetListAsync(query.Keyword, query.Page, query.PageSize);

        var total = await userRepository.GetCountAsync(query.Keyword);

        query.Result = new PaginatedListBase<UserDto>
        {
            Total = total,
            Result = mapper.Map<List<UserDto>>(list)
        };
    }

}