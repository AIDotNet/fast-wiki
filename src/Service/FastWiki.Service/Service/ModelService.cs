using AIDotNet.Abstractions;
using AIDotNet.MetaGLM;
using AIDotNet.OpenAI;
using AIDotNet.Qiansail;
using AIDotNet.SparkDesk;
using FastWiki.Service.Application.Model.Commands;
using FastWiki.Service.Application.Model.Queries;
using FastWiki.Service.Contracts.Model;
using Constant = FastWiki.Service.Contracts.Constant;

namespace FastWiki.Service.Service;

/// <summary>
/// 模型服务
/// </summary>
public sealed class ModelService(IServiceProvider serviceProvider) : ApplicationService<ModelService>
{
    /// <summary>
    /// 获取所有支持的对话类型
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetChatTypes()
        => IADNChatCompletionService.ServiceNames;


    public async ValueTask<(IADNChatCompletionService, FastModelDto)> GetChatService(string serviceId)
    {
        var query = new ModelInfoQuery(serviceId);
        await EventBus.PublishAsync(query);

        var service = serviceProvider.GetKeyedService<IADNChatCompletionService>(query.Result.Type);

        if (service != null)
        {
            return (service, query.Result);
        }

        throw new NotSupportedException($"不支持的对话类型：{serviceId}");
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
        var command = new CreateFastModelCommand(input);

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

    /// <summary>
    /// 修改模型
    /// </summary>
    /// <param name="dto"></param>
    [Authorize(Roles = Constant.Role.Admin)]
    public async Task UpdateFastModelAsync(FastModelDto dto)
    {
        var command = new UpdateFastModelCommand(dto);

        await EventBus.PublishAsync(command);
    }

    /// <summary>
    /// 禁用或启用模型
    /// </summary>
    /// <param name="id"></param>
    /// <param name="enable"></param>
    [Authorize(Roles = Constant.Role.Admin)]
    public async Task EnableFastModelAsync(string id, bool enable)
    {
        var command = new EnableFastModelCommand(id, enable);

        await EventBus.PublishAsync(command);
    }

    /// <summary>
    /// 获取可绑定的模型列表
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public async Task<List<GetFastModelDto>> GetChatModelListAsync()
    {
        var query = new ChatModelListQuery();

        await EventBus.PublishAsync(query);

        return query.Result;
    }
}