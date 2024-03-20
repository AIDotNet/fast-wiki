using AIDotNet.Abstractions;
using AIDotNet.OpenAI;
using AIDotNet.SparkDesk;
using FastWiki.Service.Application.Model.Commands;
using FastWiki.Service.Application.Model.Queries;
using FastWiki.Service.Contracts.Model;
using FastWiki.Service.Domain.Model.Aggregates;
using Constant = FastWiki.Service.Contracts.Constant;

namespace FastWiki.Service.Service;

public sealed class ModelService : ApplicationService<ModelService>
{
    static ModelService()
    {
        ChatServices.Add(OpenAIOptions.ServiceName, new OpenAiService(new OpenAIOptions()
        {
            Client = new HttpClient(),
        }));

        ChatServices.Add(SparkDeskOptions.ServiceName, new SparkDeskService(new SparkDeskOptions()));
    }

    /// <summary>
    /// 获取所有支持的对话类型
    /// </summary>
    /// <returns></returns>
    public List<string> GetChatTypes()
        => IADNChatCompletionService.ServiceNames;

    private static readonly Dictionary<string, IADNChatCompletionService> ChatServices = new();

    public static IADNChatCompletionService GetChatService(string serviceName)
    {
        if (ChatServices.TryGetValue(serviceName, out var service))
        {
            return service;
        }

        throw new NotSupportedException($"不支持的对话类型：{serviceName}");
    }

    /// <summary>
    /// 获取模型列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [Authorize(Roles = Constant.Role.Admin)]
    public async Task<PaginatedListBase<FastModelDto>> GetModelListAsync(string keyword, int page, int pageSize)
    {
        var query = new GetModelListQuery(keyword, page, pageSize);
        
        await EventBus.PublishAsync(query);
        
        return query.Result;
    }
    
    /// <summary>
    /// 创建模型
    /// </summary>
    /// <param name="input"></param>
    [Authorize(Roles = Constant.Role.Admin)]
    public async Task CreateFastModelAsync(CreateFastModeInput input)
    {
        var command = new CreateFastModeCommand(input);
        
        await EventBus.PublishAsync(command);
    }
    
    /// <summary>
    /// 删除指定模型
    /// </summary>
    /// <param name="id"></param>
    [Authorize(Roles = Constant.Role.Admin)]
    public async Task RemoveFastModelAsync(string id)
    {
        var command = new RemoveFastModelCommand(id);
        
        await EventBus.PublishAsync(command);
    }
}