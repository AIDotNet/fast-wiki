using FastWiki.Service.Application.Model.Queries;
using FastWiki.Service.Contracts.Model;
using FastWiki.Service.Domain.Model.Repositories;

namespace FastWiki.Service.Application.Model;

public sealed class ModelQueryHandler(IFastModelRepository fastModelRepository, IMapper mapper)
{
    [EventHandler]
    public async Task GetModelListAsync(GetModelListQuery query)
    {
        var models = await fastModelRepository.GetModelListAsync(query.Keyword, query.Page, query.PageSize);

        var count = await fastModelRepository.GetModelCountAsync(query.Keyword);

        query.Result = new PaginatedListBase<FastModelDto>
        {
            Result = mapper.Map<List<FastModelDto>>(models),
            Total = count
        };
    }

    [EventHandler]
    public async Task GetModelAsync(ChatModelListQuery query)
    {
        query.Result =
            mapper.Map<List<GetFastModelDto>>(
                (await fastModelRepository.GetListAsync(x => x.Enable == true)).OrderBy(x => x.Order));
    }
    
    [EventHandler]
    public async Task GetModelInfoAsync(ModelInfoQuery query)
    {
        var model = await fastModelRepository.FindAsync(query.Id);
        query.Result = mapper.Map<FastModelDto>(model);
    }
}