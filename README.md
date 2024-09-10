<div align="center"><a name="readme-top"></a>

<img height="160" src="https://api.token-ai.cn/logo.png">

<h1>FastWiki</h1>

FastWiki creates an enterprise-level AI customer service management system!

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
Documentation Language: [English](README.md) | [简体中文](README-zh-cn.md)

## Introduction

This project is a high-performance knowledge base system designed for large-scale information retrieval and intelligent search, built on the latest tech stack. Utilizing Microsoft's Semantic Kernel for deep learning and natural language processing, combined with .NET 8 and the React framework, and powered by MasaFramework in the backend, it implements an efficient, user-friendly, and scalable intelligent vector search platform. Our goal is to provide an intelligent search solution capable of understanding and handling complex queries, helping users quickly and accurately obtain the information they need.

## Tech Stack

- Frontend Framework: React + LobeChat + TypeScript
- Backend Framework: MasaFramework based on .NET 8
- Dynamic Functions implemented using JS V8 engine
- Vector Search Engine: Uses PostgreSQL's vector plugin to optimize search performance and provides DISK storage
- Deep Learning & NLP: Microsoft Semantic Kernel enhances semantic understanding of searches
- License: Apache-2.0, encouraging community contributions and usage

## Features

- Intelligent Search: Leveraging deep learning and natural language processing technologies of Semantic Kernel, capable of understanding complex queries and providing precise search results.
- High Performance: Optimized vector search performance through PostgreSQL's vector plugin, ensuring quick responses even with large datasets.
- Modern Frontend: Utilizes React + LobeUI frontend framework, providing responsive design and user-friendly interfaces.
- Powerful Backend: Based on the latest .NET 8 and MasaFramework, ensuring efficient and maintainable code.
- Open Source and Community Driven: Uses Apache-2.0 license, encouraging developers and businesses to use and contribute.
- Robust Dynamic JS Functions, with convenient smart code suggestions provided by Monaco.
- Powerful QA question-answer splitting mode, making knowledge base responses more intelligent.
- Quick integration with FeiShu Bot
- Quick integration with WeChat Official Account
- Quick integration with corporate projects

## Quick Deployment

### Prerequisites
- Docker
- AI model interfaces / need support for AI dialogue models and embedding models

### Deployment

Run with the docker command:

```bash
docker run -d \
  --name fast-wiki-service \
  --user root \
  --restart always \
  -p 8080:8080 \
  -v $(pwd)/wwwroot/uploads:/app/wwwroot/uploads \
  -v $(pwd)/data:/app/data \
  -e OPENAI_CHAT_ENDPOINT=https://api.openai.com \
  -e OPENAI_CHAT_EMBEDDING_ENDPOINT=https://api.openai.com \
  -e DEFAULT_TYPE=sqlite \
  -e DEFAULT_CONNECTION=Data\ Source=/app/data/fast-wiki.db \
  -e WIKI_CONNECTION=/app/data/wiki.db \
  -e OPENAI_CHAT_TOKEN=YourTokenKey \
  -e ASPNETCORE_ENVIRONMENT=Development \
  registry.token-ai.cn/ai-dotnet/fast-wiki-service
```

## Quick Start

### Prerequisites

Ensure you have installed the .NET 8 SDK, PostgreSQL database, and the PostgreSQL vector plugin, and configured the respective environment.

## Frontend

### Installation

1. Clone the repository:

```
git clone --recursive https://github.com/AIDotNet/fast-wiki.git
```

2. Install Node.js, the latest version (https://nodejs.p2hp.com/).

3. Delete the `package-lock.json` file and `node_modules` directory in the web directory.

4. Run in the web directory:

```
npm i
npm run build
```

5. Copy the contents from the `dist` directory in the web directory to the `\fast-wiki\src\Service\FastWiki.Service\wwwroot` directory (create `wwwroot` if it doesn't exist).

## Backend

1. Install dependencies:

Execute in the project root directory:

```
cd src/Service/FastWiki.Service
dotnet restore
```

2. Database Configuration:

Ensure your PostgreSQL database is running correctly and that the necessary database has been created. Modify the database connection string in `appsettings.json` according to your configuration.

### Running

Execute in the project root directory:

```
dotnet run
```

This will start the backend service. Access http://localhost:5124/ to view the frontend page.

Default username and password: admin Aa123456

## Environment Variable Parameters

FastWikiService environment variable parameters:

- QUANTIZE_MAX_TASK: Maximum concurrent quantization tasks, default is 3
- OPENAI_CHAT_ENDPOINT: Address of the OpenAI API
- OPENAI_CHAT_EMBEDDING_ENDPOINT: Address of the Embedding API
- OPENAI_CHAT_TOKEN: Token for OpenAI API
- OPENAI_EMBEDDING_TOKEN: Token for Embedding, defaults to empty, if empty, uses the dialogue token
- DEFAULT_TYPE: Business database type, defaults to sqlite | [pgsql | postgres]
- DEFAULT_CONNECTION: Business database connection string
- WIKI_CONNECTION: Wiki database connection string (if disk, then it's the directory)

## Technical Communication

![Group Chat QR Code](img/wechat.png)

## Contribution Guidelines

We welcome all forms of contributions, whether feature requests, bug reports, code submissions, documentation, or other types of support. Please refer to `CONTRIBUTING.md` for how to get started.

## License

This project is licensed under the Apache-2.0 license. For details, please see the [LICENSE](LICENSE) file.
