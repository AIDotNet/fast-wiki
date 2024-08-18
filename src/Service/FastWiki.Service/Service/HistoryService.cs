using Masa.BuildingBlocks.Authentication.Identity;
using mem0.Core;
using mem0.Core.Model;
using mem0.Core.VectorStores;

namespace FastWiki.Service.Service;

public class HistoryService(WikiDbContext wikiDbContext, IUserContext userContext) : IHistoryService
{
    public async Task AddHistoryAsync(string memoryId, string prevValue, string newValue, string @event,
        bool isDeleted = false,
        string userId = null, string trackId = null)
    {
        await wikiDbContext.Histories.AddAsync(new History
        {
            MemoryId = memoryId,
            PrevValue = prevValue,
            NewValue = newValue,
            Event = @event,
            DateTime = DateTime.Now,
            IsDeleted = isDeleted,
            UserId = userId,
            TrackId = trackId
        });

        await wikiDbContext.SaveChangesAsync();
    }

    public async Task<PagingDto<History>> GetHistoriesAsync(string memoryId, int page = 1, int pageSize = 10)
    {
        var total = await wikiDbContext.Histories.Where(h => h.MemoryId == memoryId).CountAsync();

        var histories = await wikiDbContext.Histories
            .Where(h => h.MemoryId == memoryId && userContext.UserId == h.UserId)
            .OrderByDescending(h => h.DateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagingDto<History>
        {
            Total = total,
            Items = histories
        };
    }

    public async Task ResetHistoryAsync()
    {
        await wikiDbContext.Histories
            .Where(x => userContext.UserId == x.UserId)
            .ExecuteDeleteAsync();
    }
}