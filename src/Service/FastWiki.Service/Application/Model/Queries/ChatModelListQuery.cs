using FastWiki.Service.Contracts.Model;

namespace FastWiki.Service.Application.Model.Queries;

/// <summary>
/// 获取对话模型列表
/// </summary>
public record ChatModelListQuery():Query<List<GetFastModelDto>>
{
    public override List<GetFastModelDto> Result { get; set; }
}