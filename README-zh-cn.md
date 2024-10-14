<div align="center"><a name="readme-top"></a>

<img height="160" src="./img/logo.png">

<h1>FastWiki</h1>

FastWiki打造企业级人工智能客服管理系统！

[![][github-contributors-shield]][github-contributors-link]
[![][github-forks-shield]][github-forks-link]
[![][github-stars-shield]][github-stars-link]
[![][github-issues-shield]][github-issues-link]
[![][github-license-shield]][github-license-link]

[Changelog](./CHANGELOG.md) · [Report Bug][github-issues-link] · [Request Feature][github-issues-link]

![](https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png)

</div>

[npm-release-shield]: https://img.shields.io/npm/v/@lobehub/chat?color=369eff&labelColor=ffcb47&logo=npm&logoColor=white&style=flat-square

[npm-release-link]: https://www.npmjs.com/package/@lobehub/chat

[github-releasedate-shield]: https://img.shields.io/github/release-date/AIDotNet/fast-wiki?color=8ae8ff&labelColor=ffcb47&style=flat-square

[github-releasedate-link]: https://github.com/AIDotNet/fast-wiki/releases

[github-action-test-shield]: https://img.shields.io/github/actions/workflow/status/AIDotNet/fast-wiki/test.yml?color=8ae8ff&label=test&labelColor=ffcb47&logo=githubactions&logoColor=white&style=flat-square

[github-action-test-link]: https://github.com/AIDotNet/fast-wiki/actions/workflows/test.yml

[github-action-release-shield]: https://img.shields.io/github/actions/workflow/status/AIDotNet/fast-wiki/release.yml?color=8ae8ff&label=release&labelColor=ffcb47&logo=githubactions&logoColor=white&style=flat-square

[github-action-release-link]: https://github.com/AIDotNet/fast-wiki/actions/workflows/release.yml

[github-contributors-shield]: https://img.shields.io/github/contributors/AIDotNet/fast-wiki?color=c4f042&labelColor=ffcb47&style=flat-square

[github-contributors-link]: https://github.com/AIDotNet/fast-wiki/graphs/contributors

[github-forks-shield]: https://img.shields.io/github/forks/AIDotNet/fast-wiki?color=8ae8ff&labelColor=ffcb47&style=flat-square

[github-forks-link]: https://github.com/AIDotNet/fast-wiki/network/members

[github-stars-shield]: https://img.shields.io/github/stars/AIDotNet/fast-wiki?color=ffcb47&labelColor=ffcb47&style=flat-square

[github-stars-link]: https://github.com/AIDotNet/fast-wiki/network/stargazers

[github-issues-shield]: https://img.shields.io/github/issues/AIDotNet/fast-wiki?color=ff80eb&labelColor=ffcb47&style=flat-square

[github-issues-link]: https://github.com/AIDotNet/fast-wiki/issues

[github-license-shield]: https://img.shields.io/github/license/AIDotNet/fast-wiki?color=white&labelColor=ffcb47&style=flat-square

[github-license-link]: https://github.com/AIDotNet/fast-wiki/blob/main/LICENSE

# FastWiki

-----
文档语言: [English](README.md) | [简体中文](README-zh-cn.md)

## 介绍

本项目是一个高性能、基于最新技术栈的知识库系统，专为大规模信息检索和智能搜索设计。利用微软Semantic
Kernel进行深度学习和自然语言处理，结合.NET
8与react框架，后台采用MasaFramework，实现了一个高效、易用、可扩展的智能向量搜索平台。我们的目标是提供一个能够理解和处理复杂查询的智能搜索解决方案，帮助用户快速准确地获取所需信息。

## 技术栈

- 前端框架：react+LobeChat+ts
- 后端框架：MasaFramework 基于 .NET 8
- 基于JS V8引擎实现动态Function
- 向量搜索引擎：使用 PostgreSQL 的向量插件，优化搜索性能也提供了DISK
- 深度学习与NLP：微软Semantic Kernel，提升搜索的语义理解能力
- 许可证：Apache-2.0，鼓励社区贡献和使用

## 特点

- 智能搜索：借助Semantic Kernel的深度学习和自然语言处理技术，能够理解复杂查询，提供精准的搜索结果。
- 高性能：通过pgsql的向量插件优化向量搜索性能，确保即使在大数据量下也能快速响应。
- 现代化前端：使用react+lobeUI前端框架，提供响应式设计和用户友好的界面。
- 强大的后端：基于最新的.NET 8和MasaFramework，确保了代码的高效性和可维护性。
- 开源和社区驱动：采用Apache-2.0许可证，鼓励开发者和企业使用和贡献。
- 强大的动态JS Function，并且提供Monaco更方便的智能代码提示。
- 强大的QA问答拆分模式，让知识库回复更智能。
- 快速接入飞书机器人
- 快速接入微信公众号
- 快速接入企业项目

## 快速部署

### 先决条件
- Docker
- AI模型接口/需要支持AI对话模型和Embedding模型

### 部署

使用docker命令运行：

```bash
docker run -d \
  --name fast-wiki-service \
  --user root \
  --restart always \
  -p 8080:8080 \
  -v $(pwd)/wwwroot/uploads:/app/wwwroot/uploads \
  -v $(pwd)/data:/app/data \
  -e OPENAI_CHAT_ENDPOINT=https://api.openai.com/v1 \
  -e OPENAI_CHAT_EMBEDDING_ENDPOINT=https://api.openai.com/v1 \
  -e DEFAULT_TYPE=sqlite \
  -e DEFAULT_CONNECTION=Data\ Source=/app/data/fast-wiki.db \
  -e WIKI_CONNECTION=/app/data/wiki.db \
  -e OPENAI_CHAT_TOKEN=您的TokenKey \
  -e ASPNETCORE_ENVIRONMENT=Development \
  registry.token-ai.cn/ai-dotnet/fast-wiki-service
```

## 快速开始

### 先决条件

确保你已经安装了.NET 8 SDK和PostgreSQL数据库和PostgreSQL的vector插件，并且配置了相应的环境。

## 前端

### 安装

1. 克隆仓库：

```
git clone  --recursive https://gitee.com/aidotnet/fast-wiki
```

2. 安装好node.js，最新版本(https://nodejs.p2hp.com/)。

3. 将web目录的package-lock.json文件和node_modules目录删除,

4. 在web目录运行

```
npm i
npm run build
```

5. 将web目录下的dist下的内容copy到"\fast-wiki\src\Service\FastWiki.Service\wwwroot" 目录下(如果wwwroot没有就创建个)

## 后端

1. 安装依赖项：

在项目根目录下执行：

```
cd src/Service/FastWiki.Service
dotnet restore
```

2. 数据库配置：

确保你的PostgreSQL数据库运行正常，并且创建了必要的数据库。根据你的配置修改`appsettings.json`中的数据库连接字符串。

### 运行

在项目根目录下执行：

```
dotnet run
```

这将启动后端服务。访问http://localhost:5124/就可以看到前端的页面了

默认账号密码：admin Aa123456

## 环境变量参数

FastWikiService环境变量参数：

- QUANTIZE_MAX_TASK：量化任务的最大并发数，默认为3
- OPENAI_CHAT_ENDPOINT：OpenAI API的地址
- OPENAI_CHAT_EMBEDDING_ENDPOINT： Embedding API的地址
- OPENAI_CHAT_TOKEN： OpenAI API的Token
- OPENAI_EMBEDDING_TOKEN: Embedding的Token, 默认为空，为空则使用对话的Token
- DEFAULT_TYPE：业务数据库类型 默认sqlite|[pgsql|postgres]
- DEFAULT_CONNECTION：业务数据库连接字符串
- WIKI_CONNECTION: wiki数据库连接字符串(如果是disk则是目录)

## 技术交流

![群聊二维码](img/wechat.png)

## 贡献指南

我们欢迎所有形式的贡献，无论是功能请求、bug报告、代码提交、文档或是其他类型的支持。请参阅`CONTRIBUTING.md`了解如何开始。

## 许可证

本项目采用Apache-2.0许可证。详情请见[LICENSE](LICENSE)文件。
