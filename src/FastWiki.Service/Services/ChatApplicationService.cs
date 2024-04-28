using FastWiki.Service.DataAccess;
using FastWiki.Service.Dto;
using FastWiki.Service.Entities;
using FastWiki.Service.Input;
using Masa.Utils.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FastWiki.Service.Services;

public class ChatApplicationService(MasterDbContext masterDbContext) : ApplicationService<ChatApplicationService>
{
    [Authorize]
    public async Task CreateAsync(CreateChatApplicationInput input)
    {
        var chatApplication = new ChatApplication(input.Name, input.ChatModel, input.Temperature);

        await masterDbContext.ChatApplications.AddAsync(chatApplication);
        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task RemoveAsync(string id)
    {
        var chatApplication = await masterDbContext.ChatApplications.FirstOrDefaultAsync(x => x.Id == id);

        if (chatApplication == null)
        {
            throw new UserFriendlyException("Chat application not found");
        }

        masterDbContext.ChatApplications.Remove(chatApplication);
        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task UpdateAsync(string id, CreateChatApplicationInput input)
    {
        var chatApplication = await masterDbContext.ChatApplications.FirstOrDefaultAsync(x => x.Id == id);

        if (chatApplication == null)
        {
            throw new UserFriendlyException("Chat application not found");
        }

        Mapper.Map(input, chatApplication);

        masterDbContext.ChatApplications.Update(chatApplication);

        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task<PaginatedListBase<ChatApplicationDto>> GetListAsync(int page, int pageSize)
    {
        var query = masterDbContext.ChatApplications.AsQueryable();

        var list = await query
            .Where(x => x.Creator == UserContext.GetUserId<Guid>())
            .OrderBy(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await query.CountAsync();

        return new PaginatedListBase<ChatApplicationDto>
        {
            Total = total,
            Result = Mapper.Map<List<ChatApplicationDto>>(list)
        };
    }
    
    [Authorize]
    public async Task<ChatApplicationDto> GetAsync(string id)
    {
        var chatApplication = await masterDbContext.ChatApplications.FirstOrDefaultAsync(x => x.Id == id);

        if (chatApplication == null)
        {
            throw new UserFriendlyException("Chat application not found");
        }

        return Mapper.Map<ChatApplicationDto>(chatApplication);
    }

    [Authorize]
    public async Task CreateChatApplicationShareAsync(ChatApplicationShareInput input)
    {
        var chatApplication = await masterDbContext.ChatApplications.FirstOrDefaultAsync(x => x.Id == input.ChatApplicationId);

        if (chatApplication == null)
        {
            throw new UserFriendlyException("Chat application not found");
        }

        var chatApplicationShare = new ChatApplicationShare(input.Name, input.ChatApplicationId, input.Expires, input.AvailableToken, input.AvailableQuantity);

        await masterDbContext.ChatApplicationShares.AddAsync(chatApplicationShare);
        await masterDbContext.SaveChangesAsync();
    }
    
    [Authorize]
    public async Task RemoveChatApplicationShareAsync(string id)
    {
        var chatApplicationShare = await masterDbContext.ChatApplicationShares.FirstOrDefaultAsync(x => x.Id == id);

        if (chatApplicationShare == null)
        {
            throw new UserFriendlyException("Chat application share not found");
        }

        masterDbContext.ChatApplicationShares.Remove(chatApplicationShare);
        await masterDbContext.SaveChangesAsync();
    }
    
    [Authorize]
    public async Task UpdateChatApplicationShareAsync(string id, ChatApplicationShareInput input)
    {
        var chatApplicationShare = await masterDbContext.ChatApplicationShares.FirstOrDefaultAsync(x => x.Id == id);

        if (chatApplicationShare == null)
        {
            throw new UserFriendlyException("Chat application share not found");
        }

        Mapper.Map(input, chatApplicationShare);

        masterDbContext.ChatApplicationShares.Update(chatApplicationShare);

        await masterDbContext.SaveChangesAsync();
    }
    
    [Authorize]
    public async Task<PaginatedListBase<ChatApplicationShareDto>> GetChatApplicationShareListAsync(int page, int pageSize)
    {
        var query = masterDbContext.ChatApplicationShares.AsQueryable();

        var list = await query
            .Where(x => x.Creator == UserContext.GetUserId<Guid>())
            .OrderBy(x => x.CreationTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await query.CountAsync();

        return new PaginatedListBase<ChatApplicationShareDto>
        {
            Total = total,
            Result = Mapper.Map<List<ChatApplicationShareDto>>(list)
        };
    }
}