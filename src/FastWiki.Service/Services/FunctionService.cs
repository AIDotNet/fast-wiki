using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FastWiki.Service.DataAccess;
using FastWiki.Service.Entities;
using FastWiki.Service.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FastWiki.Service.Services;

public class FunctionService(MasterDbContext masterDbContext) : ApplicationService<FunctionService>
{
    [Authorize]
    public async Task CreateAsync(CreateFunctionInput input)
    {
        var function = new AIFunction(input.Name, input.Description, input.Content,
            Mapper.Map<List<AIFunctionItem>>(input.Parameters), Mapper.Map<List<AIFunctionItem>>(input.Items),
            input.Imports, input.Main);

        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task RemoveAsync(long id)
    {
        await masterDbContext.AIFunctions
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    [Authorize]
    public async Task UpdateAsync(long id, CreateFunctionInput input)
    {
        var function = await masterDbContext.AIFunctions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (function == null)
        {
            throw new UserFriendlyException("Function not found");
        }

        Mapper.Map(input, function);

        await masterDbContext.SaveChangesAsync();
    }

    [Authorize]
    public async Task<List<AIFunctionDto>> GetListAsync()
    {
        var query = masterDbContext.AIFunctions.AsQueryable();

        var list = await query
            .OrderBy(x => x.CreationTime)
            .ToListAsync();

        return Mapper.Map<List<AIFunctionDto>>(list);
    }
    
}