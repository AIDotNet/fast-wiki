﻿using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;

namespace FastWiki.Service.Entities;


public sealed class FileStorage : FullAggregateRoot<long, Guid?>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 文件存放路径
    /// </summary>
    public string Path { get; private set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// 是否压缩
    /// </summary>
    public bool IsCompression { get; private set; }

    /// <summary>
    /// 文件存放目录
    /// </summary>
    public string FullName { get; private set; }

    protected FileStorage()
    {
    }

    public FileStorage(string name, string path, long size, bool isCompression)
    {
        Name = name;
        Path = path;
        Size = size;
        IsCompression = isCompression;
    }

    public void SetFullName(string fullName)
    {
        FullName = fullName;
    }
}