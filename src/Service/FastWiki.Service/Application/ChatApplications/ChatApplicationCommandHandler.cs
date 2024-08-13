namespace FastWiki.Service.Application.ChatApplications;

public class ChatApplicationCommandHandler(
    IChatApplicationRepository chatApplicationRepository,
    IMapper mapper)
{
    [EventHandler]
    public async Task CreateChatApplicationAsync(CreateChatApplicationCommand command)
    {
        var chatApplication = new ChatApplication(Guid.NewGuid().ToString("N"))
        {
            Name = command.Input.Name
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
    public async Task CreateChatShareAsync(CreateChatShareCommand command)
    {
        var share = new ChatShare(command.Input.Name, command.Input.ChatApplicationId, command.Input.Expires,
            command.Input.AvailableToken, command.Input.AvailableQuantity);
        await chatApplicationRepository.CreateChatShareAsync(share);
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

    [EventHandler]
    public async Task CreateChatRecordAsync(CreateChatRecordCommand command)
    {
        if (command.Question.IsNullOrWhiteSpace()) return;

        await chatApplicationRepository.CreateChatRecordAsync(new ChatRecord(Guid.NewGuid().ToString("N"),
            command.ApplicationId, command.Question));
    }

    [EventHandler]
    public async Task CreateQuestionsAsync(CreateQuestionsCommand command)
    {
        await chatApplicationRepository.CreateQuestionsAsync(new Questions(Guid.NewGuid().ToString("N"),
            command.QuestionsDto.ApplicationId, command.QuestionsDto.Question, command.QuestionsDto.Order));
    }

    [EventHandler]
    public async Task RemoveQuestionsAsync(RemoveQuestionsCommand command)
    {
        await chatApplicationRepository.RemoveQuestionsAsync(command.Id);
    }
}