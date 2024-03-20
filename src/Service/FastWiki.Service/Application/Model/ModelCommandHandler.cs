using FastWiki.Service.Application.Model.Commands;
using FastWiki.Service.Domain.Model.Aggregates;
using FastWiki.Service.Domain.Model.Repositories;

namespace FastWiki.Service.Application.Model;

public sealed class ModelCommandHandler(IFastModelRepository fastModelRepository)
{
    [EventHandler]
    public async Task CreateFastModeAsync(CreateFastModeCommand command)
    {
        if (await fastModelRepository.ExistAsync(command.Input.Name))
        {
            throw new UserFriendlyException("模型名称已存在");
        }

        var model = new FastModel(command.Input.Name, command.Input.Type, command.Input.Url, command.Input.ApiKey,
            command.Input.Description, command.Input.Models, command.Input.Order);

        await fastModelRepository.AddAsync(model);
    }

    [EventHandler]
    public async Task RemoveFastModelAsync(RemoveFastModelCommand command)
    {
        await fastModelRepository.RemoveAsync(command.Id);
    }
}