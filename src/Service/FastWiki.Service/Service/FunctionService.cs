using FastWiki.Service.Contracts.Function.Dto;
using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Function.Repositories;
using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Service;

public class FunctionService(
    IFastWikiFunctionCallRepository fastWikiFunctionCallRepository,
    IMapper mapper,
    IUserContext userContext)
    : ApplicationService<FunctionService>
{
    [Authorize]
    public async Task CreateFunctionAsync(FastWikiFunctionCallInput input)
    {
        var functionCall = new FastWikiFunctionCall()
        {
            Content = input.Content,
            Description = input.Description,
            Imports = input.Imports,
            Enable = true,
            Main = input.Main,
            Items = input.Items.Select(x => new FunctionItem
            {
                Key = x.Key,
                Value = x.Value
            }).ToList(),
            Name = input.Name,
            Parameters = input.Parameters.Select(x => new FunctionItem
            {
                Key = x.Key,
                Value = x.Value
            }).ToList()
        };

        await fastWikiFunctionCallRepository.AddAsync(functionCall);
    }

    [Authorize]
    public async Task UpdateFunctionAsync(FastWikiFunctionCallDto input)
    {
        var functionCall = await fastWikiFunctionCallRepository.FindAsync(input.Id);
        if (functionCall == null)
        {
            throw new Exception("function call not found");
        }

        functionCall.Content = input.Content;
        functionCall.Description = input.Description;
        functionCall.Imports = input.Imports;
        functionCall.Items = input.Items.Select(x => new FunctionItem
        {
            Key = x.Key,
            Value = x.Value
        }).ToList();
        functionCall.Name = input.Name;
        functionCall.Parameters = input.Parameters.Select(x => new FunctionItem
        {
            Key = x.Key,
            Value = x.Value
        }).ToList();
        functionCall.Enable = input.Enable;

        await fastWikiFunctionCallRepository.UpdateAsync(functionCall);
    }

    [Authorize]
    public async Task RemoveFunctionAsync(long id)
    {
        await fastWikiFunctionCallRepository.RemoveAsync(id);
    }

    [Authorize]
    public async Task<PaginatedListBase<FastWikiFunctionCallDto>> GetFunctionListAsync(int page, int pageSize)
    {
        var items = await fastWikiFunctionCallRepository.GetFunctionListAsync(userContext.GetUserId<Guid>(),
            page, pageSize);

        var total = await fastWikiFunctionCallRepository.GetFunctionCountAsync(userContext.GetUserId<Guid>());

        return new PaginatedListBase<FastWikiFunctionCallDto>()
        {
            Result = mapper.Map<List<FastWikiFunctionCallDto>>(items),
            Total = total
        };
    }

    [Authorize]
    public async Task EnableFunctionCallAsync(long id, bool enable)
    {
        await fastWikiFunctionCallRepository.EnableFunctionCallAsync(id, enable);
    }

    [Authorize]
    public async Task<List<FastWikiFunctionCallSelectDto>> GetFunctionCallSelectAsync()
    {
        var items = await fastWikiFunctionCallRepository.GetFunctionListAsync(userContext.GetUserId<Guid>());

        return mapper.Map<List<FastWikiFunctionCallSelectDto>>(items);
    }
}