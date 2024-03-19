using AIDotNet.Abstractions;
using AIDotNet.OpenAI;
using AIDotNet.SparkDesk;

namespace FastWiki.Service.Service;

public sealed class ModuleService : ApplicationService<ModuleService>
{
    static ModuleService()
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
    public async Task<List<string>> GetChatTypes()
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
}