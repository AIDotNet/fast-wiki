using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Application.ChatApplications;

public class ChatApplicationQueryHandler(
    IChatApplicationRepository chatApplicationRepository,
    IMapper mapper,
    IUserContext userContext)
{
    [EventHandler]
    public async Task ChatApplicationAsync(ChatApplicationQuery query)
    {
        var result = await chatApplicationRepository.GetListAsync(query.Page, query.PageSize);

        var total = await chatApplicationRepository.GetCountAsync();

        query.Result = new PaginatedListBase<ChatApplicationDto>()
        {
            Result = mapper.Map<List<ChatApplicationDto>>(result),
            Total = total
        };
    }

    [EventHandler]
    public async Task ChatApplicationInfoAsync(ChatApplicationInfoQuery query)
    {
        var result = await chatApplicationRepository.FindAsync(query.Id);

        query.Result = mapper.Map<ChatApplicationDto>(result);
    }

    [EventHandler]
    public async Task ChatDialogAsync(ChatDialogQuery query)
    {
        var result = await chatApplicationRepository.GetChatDialogListAsync(query.chatId, query.all);

        query.Result = mapper.Map<List<ChatDialogDto>>(result);
    }

    [EventHandler]
    public async Task ChatShareDialogAsync(ChatShareDialogQuery query)
    {
        var result = await chatApplicationRepository.GetChatShareDialogListAsync(query.chatId);

        query.Result = mapper.Map<List<ChatDialogDto>>(result);
    }

    [EventHandler]
    public async Task ChatDialogHistoryAsync(ChatDialogHistoryQuery query)
    {
        var result =
            await chatApplicationRepository.GetChatDialogHistoryListAsync(query.ChatDialogId, query.Page,
                query.PageSize);

        var total = await chatApplicationRepository.GetChatDialogHistoryCountAsync(query.ChatDialogId);

        query.Result = new PaginatedListBase<ChatDialogHistoryDto>()
        {
            Result = mapper.Map<List<ChatDialogHistoryDto>>(result.OrderBy(x => x.CreationTime)),
            Total = total
        };
    }

    [EventHandler]
    public async Task ChatShareAsync(ChatShareQuery query)
    {
        var result = await chatApplicationRepository.GetChatShareListAsync(userContext.GetUserId<Guid>(),
            query.chatApplicationId, query.page, query.pageSize);

        var total = await chatApplicationRepository.GetChatShareCountAsync(userContext.GetUserId<Guid>(),
            query.chatApplicationId);


        query.Result = new PaginatedListBase<ChatShareDto>()
        {
            Result = mapper.Map<List<ChatShareDto>>(result),
            Total = total
        };
    }

    [EventHandler]
    public async Task ChatShareInfoAsync(ChatShareInfoQuery query)
    {
        var result = await chatApplicationRepository.GetChatShareAsync(query.Id);

        query.Result = mapper.Map<ChatShareDto>(result);
    }

    [EventHandler]
    public async Task ChatShareApplicationAsync(ChatShareApplicationQuery query)
    {
        var chatApplication = await chatApplicationRepository.ChatShareApplicationAsync(query.chatSharedId);

        query.Result = mapper.Map<ChatApplicationDto>(chatApplication);
    }

    [EventHandler]
    public async Task GetSessionLogDialogQueryAsync(GetSessionLogDialogQuery query)
    {
        var result =
            await chatApplicationRepository.GetSessionLogDialogListAsync(query.chatApplicationId, query.page, query.pageSize);

        var total = await chatApplicationRepository.GetSessionLogDialogCountAsync(query.chatApplicationId);

        query.Result = new PaginatedListBase<ChatDialogDto>()
        {
            Result = mapper.Map<List<ChatDialogDto>>(result),
            Total = total
        };
    }
}