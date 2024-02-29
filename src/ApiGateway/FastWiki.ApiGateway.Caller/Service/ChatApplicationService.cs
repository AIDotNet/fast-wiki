using FastWiki.ApiGateway.caller.Service;
using FastWiki.Service.Contracts.ChatApplication.Dto;
using Masa.Utils.Models;
using System.Net.Http.Json;

namespace FastWiki.ApiGateway.Caller.Service;

public sealed class ChatApplicationService(ICaller caller, IHttpClientFactory httpClientFactory)
    : ServiceBase(caller, httpClientFactory), IChatApplicationService
{
    protected override string BaseUrl { get; set; } = "ChatApplications";

    public async Task CreateAsync(CreateChatApplicationInput input)
    {
        await PostAsync(nameof(CreateAsync), input);
    }

    public async Task RemoveAsync(string id)
    {
        await DeleteAsync(nameof(RemoveAsync) + "/" + id);
    }

    public async Task UpdateAsync(UpdateChatApplicationInput input)
    {
        await PutAsync(nameof(UpdateAsync), input);
    }

    public async Task<PaginatedListBase<ChatApplicationDto>> GetListAsync(int page, int pageSize)
    {
        return await GetAsync<PaginatedListBase<ChatApplicationDto>>(nameof(GetListAsync),
            new Dictionary<string, string>()
            {
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            });
    }

    public Task<ChatApplicationDto> GetAsync(string id)
    {
        return GetAsync<ChatApplicationDto>(nameof(GetAsync) + "/" + id);
    }

    public Task<ChatApplicationDto> GetChatShareApplicationAsync(string chatShareId)
    {
        return GetAsync<ChatApplicationDto>(nameof(GetChatShareApplicationAsync), new Dictionary<string, string>()
        {
            {
                "chatShareId", chatShareId
            }
        });
    }

    public async Task CreateChatDialogAsync(CreateChatDialogInput input)
    {
        await PostAsync(nameof(CreateChatDialogAsync), input);
    }

    public async Task<List<ChatDialogDto>> GetChatDialogAsync(string chatId)
    {
        return await GetAsync<List<ChatDialogDto>>(nameof(GetChatDialogAsync),new Dictionary<string, string>()
        {
            {
                "chatId",chatId
            }
        });
    }

    public async IAsyncEnumerable<CompletionsDto> CompletionsAsync(CompletionsInput input)
    {
        var response = await SendAsync(HttpMethod.Post, nameof(CompletionsAsync), input,
            HttpCompletionOption.ResponseHeadersRead);

        if (response.IsSuccessStatusCode)
        {
            await foreach (var item in response.Content.ReadFromJsonAsAsyncEnumerable<CompletionsDto>())
            {
                yield return item;
            }

            yield break;
        }

        throw new UserFriendlyException("请求异常");
    }

    public async IAsyncEnumerable<CompletionsDto> ChatShareCompletionsAsync(ChatShareCompletionsInput input)
    {
        var response = await SendAsync(HttpMethod.Post, nameof(ChatShareCompletionsAsync), input,
            HttpCompletionOption.ResponseHeadersRead);

        if (!response.IsSuccessStatusCode)
        {
            throw new UserFriendlyException("请求异常");
        }

        await foreach (var item in response.Content.ReadFromJsonAsAsyncEnumerable<CompletionsDto>())
        {
            yield return item;
        }
    }

    public async Task CreateChatDialogHistoryAsync(CreateChatDialogHistoryInput input)
    {
        await PostAsync(nameof(CreateChatDialogHistoryAsync), input);
    }

    public async Task<PaginatedListBase<ChatDialogHistoryDto>> GetChatDialogHistoryAsync(string chatDialogId, int page,
        int pageSize)
    {
        return await GetAsync<PaginatedListBase<ChatDialogHistoryDto>>(nameof(GetChatDialogHistoryAsync),
            new Dictionary<string, string>()
            {
                {
                    "chatDialogId", chatDialogId
                },
                {
                    nameof(page), page.ToString()
                },
                {
                    nameof(pageSize),
                    pageSize.ToString()
                }
            });
    }

    public async Task RemoveDialogHistoryAsync(string id)
    {
        await DeleteAsync(nameof(RemoveDialogHistoryAsync) + "/" + id);
    }

    public async Task CreateShareAsync(CreateChatShareInput input)
    {
        await PostAsync(nameof(CreateShareAsync), input);
    }

    public async Task<PaginatedListBase<ChatShareDto>> GetChatShareListAsync(string chatApplicationId, int page,
        int pageSize)
    {
        return await GetAsync<PaginatedListBase<ChatShareDto>>(nameof(GetChatShareListAsync),
            new Dictionary<string, string>()
            {
                {
                    "chatApplicationId", chatApplicationId
                },
                {
                    "page", page.ToString()
                },
                {
                    "pageSize", pageSize.ToString()
                }
            });
    }
}