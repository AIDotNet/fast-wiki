---
sidebar_position: 1
---

# 快速开始本地开发

前置依赖项

您需要在计算机上安装和配置以下依赖项才能构建FastWiki:

- [.NET 8 SDK](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Vector](https://pgxn.org/dist/vector/#query-options)（插件文档；建议使用项目提供的compose文件启动PostgreSql，默认提供了Vector插件）
- [Docker](https://www.docker.com/)（构建镜像）
- [Git](https://git-scm.com/)

## Fork 存储库

您需要 [Fork](https://github.com/239573049/fast-wiki.git) 存储库。

## 克隆存储库

```shell
git clone https://github.com/239573049/fast-wiki.git
```

目录摘要说明：

- src/Service/FastWiki.Service：FastWiki服务端,使用`MasaFramework`，提供基础的API服务，包含数据库迁移和向量搜索，并且实现`FastWiki.Service.Contracts`的WebApi接口。
- src/Infrastructure/FastWiki.Infrastructure.Common：提供常用的工具类和扩展方法。
- src/Infrastructure/FastWiki.Infrastructure.Rcl.Command: 提供常用的Razor封装方法。
- src/Contracts/FastWiki.Service.Contracts：提供基础的DTO和业务接口定义。
- src/ApiGateway/FastWiki.ApiGateway.Caller 基于`MasaFramework`的API调用封装,是`FastWiki.Service.Contracts`的`HttpClient`实现，调用了`FastWiki.Service`的`WebAPI`封装。

## 部署数据库

第一次开发，需要先部署数据库，建议本地开发可以随便找一台 2G 的轻量小数据库实践。最好的方式就是在服务根据[docker部署](../deploy/docker.md)的教程部署一个`PostgreSQL`然后提供外网端口供测试访问。

## 初始化配置文件

在开发环境下打开`src\Service\FastWiki.Service\appsettings.Development.json`文件，然后修改ChatToken为自己的OpenAI的Key，如果需要使用自己的代理则修改ChatEndpoint和EmbeddingEndpoint为自己的代理地址。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgres;Port=5432;Database=wiki;Username=token;Password=dd666666;"
  },
  "OpenAI": {
    "ChatEndpoint": "https://api.openai.com",
    "EmbeddingEndpoint": "https://api.openai.com",
    "ChatToken": "您的AI的Key",
    "ChatModel": "gpt-3.5-turbo",
    "EmbeddingModel": "text-embedding-3-small"
  },
  "Jwt": {
    "Secret": "asd?fgahjwter.123(%^klqwter.123(%^werqwter.123(%^$%#",
    "EffectiveHours": 120
  }
}

```

## 运行

请注意，由于项目是前后分离项目，在执行的时候需要先启动`FastWiki.Service`，然后再启动`FastWiki.Web.Server`；
否则会出现`FastWiki.Web.Server`启动后无法连接到`FastWiki.Service`的情况。

启动服务端

```shell
cd src/Service/FastWiki.Service
dotnet run
```

启动前端

```shell
cd src/Web/FastWiki.Web.Server
dotnet run
```

## 部署

在根目录中提供了`docker-compose.yml`文件，可以直接使用`docker-compose`进行部署。

构建镜像

```shell
docker-compose build
```

推送镜像

```shell
docker-compose push
```

## QA

本地数据库无法连接
如果你是连接远程的数据库，先检查对应的端口是否开放。
如果是本地运行的数据库，可尝试host改成localhost或127.0.0.1
