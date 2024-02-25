using Masa.BuildingBlocks.Data.Mapping;

namespace FastWiki.Service.Application.ChatApplications;

public class ChatApplicationQueryHandler(IChatApplicationRepository chatApplicationRepository, IMapper mapper)
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
        var result = await chatApplicationRepository.GetChatDialogListAsync();

        query.Result = mapper.Map<List<ChatDialogDto>>(result);
    }
}