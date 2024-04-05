using FastWiki.Service.Application.Function.Commands;
using FastWiki.Service.Application.Function.Queries;
using FastWiki.Service.Contracts.Function;
using FastWiki.Service.Contracts.Function.Dto;

namespace FastWiki.Service.Service;

public class FunctionService : ApplicationService<FunctionService>
{
    [Authorize]
    public async Task CreateFunctionAsync(FastWikiFunctionCallInput input)
    {
        var command = new CreateFunctionCommand(input);
        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task UpdateFunctionAsync(FastWikiFunctionCallDto input)
    {
        var command = new UpdateFunctionCommand(input);
        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task RemoveFunctionAsync(long id)
    {
        var command = new RemoveFunctionCommand(id);
        await EventBus.PublishAsync(command);
    }

    [Authorize]
    public async Task<PaginatedListBase<FastWikiFunctionCallDto>> GetFunctionListAsync(int page, int pageSize)
    {
        var command = new FunctionListQuery(page, pageSize);
        await EventBus.PublishAsync(command);

        return command.Result;
    }
    
    [Authorize]
    public async Task EnableFunctionCallAsync(long id, bool enable)
    {
        var command = new EnableFunctionCallCommand(id, enable);
        await EventBus.PublishAsync(command);
    }
    
    [Authorize]
    public async Task<List<FastWikiFunctionCallSelectDto>> GetFunctionCallSelectAsync()
    {
        var command = new GetFunctionCallSelectQuery();
        await EventBus.PublishAsync(command);

        return command.Result;
    }
}