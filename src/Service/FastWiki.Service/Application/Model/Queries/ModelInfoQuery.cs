using FastWiki.Service.Contracts.Model;

namespace FastWiki.Service.Application.Model.Queries;

/// <summary>
/// 获取模型信息
/// </summary>
/// <param name="Id"></param>
public record ModelInfoQuery(string Id):Query<FastModelDto>
{
    public override FastModelDto Result { get; set; }
}
