using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.Application.Storage.Queries;

/// <summary>
/// 获取存储详情
/// </summary>
/// <param name="FileId"></param>
public record StorageInfoQuery(long FileId) : Query<FileStorage>
{
    public override FileStorage Result { get; set; }
}


/// <summary>
/// 获取存储详情
/// </summary>
/// <param name="FileId"></param>
public record StorageInfosQuery(IEnumerable<long> FileId) : Query<IEnumerable<FileStorage>>
{
    public override IEnumerable<FileStorage> Result { get; set; }
}