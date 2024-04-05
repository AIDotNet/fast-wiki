using FastWiki.Service.Application.Function.Commands;
using FastWiki.Service.Domain.Function.Aggregates;
using FastWiki.Service.Domain.Function.Repositories;

namespace FastWiki.Service.Application.Function;

public class FunctionCommandHandler(IFastWikiFunctionCallRepository fastWikiFunctionCallRepository)
{
    [EventHandler]
    public void Handle(CreateFunctionCommand command)
    {
        var functionCall = new FastWikiFunctionCall()
        {
            Content = command.FunctionCallInput.Content,
            Description = command.FunctionCallInput.Description,
            Imports = command.FunctionCallInput.Imports,
            Enable = true,
            Main = command.FunctionCallInput.Main,
            Items = command.FunctionCallInput.Items.Select(x => new FunctionItem
            {
                Key = x.Key,
                Value = x.Value
            }).ToList(),
            Name = command.FunctionCallInput.Name,
            Parameters = command.FunctionCallInput.Parameters.Select(x => new FunctionItem
            {
                Key = x.Key,
                Value = x.Value
            }).ToList()
        };

        fastWikiFunctionCallRepository.AddAsync(functionCall);
    }

    [EventHandler]
    public void Handle(RemoveFunctionCommand command)
    {
        fastWikiFunctionCallRepository.RemoveAsync(command.Id);
    }

    [EventHandler]
    public async Task Handle(UpdateFunctionCommand command)
    {
        var functionCall = await fastWikiFunctionCallRepository.FindAsync(command.Dto.Id);
        if (functionCall == null)
        {
            throw new Exception("function call not found");
        }

        functionCall.Content = command.Dto.Content;
        functionCall.Description = command.Dto.Description;
        functionCall.Imports = command.Dto.Imports;
        functionCall.Items = command.Dto.Items.Select(x => new FunctionItem
        {
            Key = x.Key,
            Value = x.Value
        }).ToList();
        functionCall.Name = command.Dto.Name;
        functionCall.Parameters = command.Dto.Parameters.Select(x => new FunctionItem
        {
            Key = x.Key,
            Value = x.Value
        }).ToList();
        functionCall.Enable = command.Dto.Enable;

        await fastWikiFunctionCallRepository.UpdateAsync(functionCall);
    }

    [EventHandler]
    public async Task Handle(EnableFunctionCallCommand command)
    {
        await fastWikiFunctionCallRepository.EnableFunctionCallAsync(command.Id, command.Enable);
    }
}