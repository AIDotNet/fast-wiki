using FastWiki.Service.Infrastructure.Helper;
using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Application.ChatApplications;

public class ChatApplicationCommandHandler(
    IChatApplicationRepository chatApplicationRepository,
    IMapper mapper,
    IUserContext userContext)
{
    [EventHandler]
    public async Task CreateChatApplicationAsync(CreateChatApplicationCommand command)
    {
        var chatApplication = new ChatApplication(Guid.NewGuid().ToString("N"))
        {
            Name = command.Input.Name,
            ChatType = string.Empty
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

    [EventHandler]
    public async Task CreateChatDialogAsync(CreateChatDialogCommand command)
    {
        var entity = new ChatDialog(command.Input.Name, command.Input.ChatId, command.Input.Description,
            command.Input.ApplicationId);

        entity.SetType(command.Input.Type);

        await chatApplicationRepository.CreateChatDialogAsync(entity);
    }

    [EventHandler]
    public async Task RemoveChatDialogAsync(RemoveChatDialogCommand command)
    {
        await chatApplicationRepository.RemoveChatDialogAsync(command.Id);
    }

    [EventHandler]
    public async Task CreateChatDialogHistoryAsync(CreateChatDialogHistoryCommand command)
    {
        var chatDialogHistory = new ChatDialogHistory(command.Input.ChatDialogId,
            command.Input.Content, TokenHelper.ComputeToken(command.Input.Content), command.Input.Current,
            command.Input.Type);

        await chatApplicationRepository.CreateChatDialogHistoryAsync(chatDialogHistory);
    }

    [EventHandler]
    public async Task RemoveChatDialogHistoryAsync(RemoveChatDialogHistoryCommand command)
    {
        await chatApplicationRepository.RemoveChatDialogHistoryByIdAsync(command.Id);
    }

    [EventHandler]
    public async Task CreateChatShareAsync(CreateChatShareCommand command)
    {
        var share = new ChatShare(command.Input.Name, command.Input.ChatApplicationId, command.Input.Expires,
            command.Input.AvailableToken, command.Input.AvailableQuantity);
        await chatApplicationRepository.CreateChatShareAsync(share);
    }

    [EventHandler]
    public async Task UpdateChatDialogAsync(UpdateChatDialogCommand command)
    {
        var chatDialog = mapper.Map<ChatDialog>(command.Input);

        await chatApplicationRepository.UpdateChatDialogAsync(chatDialog);
    }

    [EventHandler]
    public async Task RemoveShareDialogAsync(RemoveShareDialogCommand command)
    {
        await chatApplicationRepository.RemoveShareDialogAsync(command.ChatId, command.Id);
    }

    [EventHandler]
    public async Task UpdateShareChatDialogAsync(UpdateShareChatDialogCommand command)
    {
        await chatApplicationRepository.UpdateShareDialogAsync(mapper.Map<ChatDialog>(command.Input));
    }

    [EventHandler]
    public async Task UpdateChatShareAsync(PutChatHistoryCommand command)
    {
        await chatApplicationRepository.PutChatHistoryAsync(command.Input.Id, command.Input.Content,
            command.Input.ChatShareId);
    }

    [EventHandler]
    public async Task DeductTokenAsync(DeductTokenCommand command)
    {
        await chatApplicationRepository.DeductTokenAsync(command.Id, command.Token);
    }
    
    [EventHandler]
    public async Task RemoveChatShareAsync(RemoveChatShareCommand command)
    {
        await chatApplicationRepository.RemoveChatShareAsync(command.Id);
    }
}