using FastWiki.Service.Application.Model.Commands;
using FastWiki.Service.Domain.Model.Aggregates;
using FastWiki.Service.Domain.Model.Repositories;

namespace FastWiki.Service.Application.Model;

public sealed class ModelCommandHandler(IFastModelRepository fastModelRepository, IMapper mapper)
{
    [EventHandler]
    public async Task CreateFastModelAsync(CreateFastModelCommand command)
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
    
    [EventHandler]
    public async Task UpdateFastModelAsync(UpdateFastModelCommand command)
    {
        var model = mapper.Map<FastModel>(command.Dto);
        await fastModelRepository.UpdateAsync(model);
    }
    
    [EventHandler]
    public async Task EnableFastModelAsync(EnableFastModelCommand fastModelCommand)
    {
        await fastModelRepository.EnableAsync(fastModelCommand.Id, fastModelCommand.Enable);
    }

    [EventHandler]
    public async Task FastModelComputeTokenAsync(FastModelComputeTokenCommand command)
    {
        await fastModelRepository.FastModelComputeTokenAsync(command.Id, command.RequestToken, command.CompleteToken);
    }
}