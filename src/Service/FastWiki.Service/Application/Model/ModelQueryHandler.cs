using FastWiki.Service.Application.Model.Queries;
using FastWiki.Service.Contracts.Model;
using FastWiki.Service.Domain.Model.Repositories;

namespace FastWiki.Service.Application.Model;

public sealed class ModelQueryHandler(IFastModelRepository fastModelRepository)
{
    [EventHandler]
    public async Task GetModelListAsync(GetModelListQuery query)
    {
        var models = await fastModelRepository.GetModelListAsync(query.Keyword, query.Page, query.PageSize);

        var count = await fastModelRepository.GetModelCountAsync(query.Keyword);

        query.Result = new PaginatedListBase<FastModelDto>
        {
            Result = models.Select(x => new FastModelDto
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Description = x.Description
            }).ToList(),
            Total = count
        };
    }
}