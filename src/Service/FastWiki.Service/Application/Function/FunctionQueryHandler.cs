using FastWiki.Service.Application.Function.Queries;
using FastWiki.Service.Contracts.Function;
using FastWiki.Service.Contracts.Function.Dto;
using FastWiki.Service.Domain.Function.Repositories;
using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Application.Function;

public class FunctionQueryHandler(
    IFastWikiFunctionCallRepository fastWikiFunctionCallRepository,
    IMapper mapper,
    IUserContext userContext)
{
    [EventHandler]
    public async Task HandleAsync(FunctionListQuery command)
    {
        var items = await fastWikiFunctionCallRepository.GetFunctionListAsync(userContext.GetUserId<Guid>(),
            command.page, command.pageSize);

        var total = await fastWikiFunctionCallRepository.GetFunctionCountAsync(userContext.GetUserId<Guid>());

        command.Result = new PaginatedListBase<FastWikiFunctionCallDto>()
        {
            Result = mapper.Map<List<FastWikiFunctionCallDto>>(items),
            Total = total
        };
    }

    [EventHandler]
    public async Task HandleAsync(GetFunctionCallSelectQuery command)
    {
        var items = await fastWikiFunctionCallRepository.GetFunctionListAsync(userContext.GetUserId<Guid>());

        command.Result = mapper.Map<List<FastWikiFunctionCallSelectDto>>(items);
    }

    [EventHandler]
    public async Task HandleAsync(ChatApplicationFunctionCallQuery command)
    {
        var item = await fastWikiFunctionCallRepository.GetListAsync(x => command.Ids.Contains(x.Id));

        command.Result = item;
    }
}