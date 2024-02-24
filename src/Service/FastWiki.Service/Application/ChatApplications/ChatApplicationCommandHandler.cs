using Masa.BuildingBlocks.Data.Mapping;

namespace FastWiki.Service.Application.ChatApplications;

public class ChatApplicationCommandHandler(IChatApplicationRepository chatApplicationRepository, IMapper mapper)
{
    [EventHandler]
    public async Task CreateChatApplicationAsync(CreateChatApplicationCommand command)
    {
        var chatApplication = new ChatApplication(Guid.NewGuid().ToString("N"))
        {
            Name = command.Input.Name,
        };

        await chatApplicationRepository.AddAsync(chatApplication);
    }

    [EventHandler]
    public async Task RemoveChatApplicationAsync(RemoveChatApplicationCommand command)
    {
        await chatApplicationRepository.RemoveAsync(command.Id);
    }

    [EventHandler]
    public async Task UpdateChatApplicationAsync(UpdateChatApplicationCommand command)
    {
        var chatApplication = await chatApplicationRepository.FindAsync(command.Input.Id);

        command.Input.Name = chatApplication?.Name;
        mapper.Map(command.Input, chatApplication);

        await chatApplicationRepository.UpdateAsync(chatApplication);
    }
}