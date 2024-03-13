---
sidebar_position: 3
---

# 使用Docker部署

创建网络

```shell
docker network create fast-wiki-network
```

启动postgres，当前镜像已经安装了pgvector插件，可以直接使用。

```shell
docker run -d --name postgres --network=fast-wiki-network \
    -e POSTGRES_USER=token \
    -e POSTGRES_PASSWORD=dd666666 \
    -e POSTGRES_DB=wiki \
    -e TZ=Asia/Shanghai \
    -v $(pwd)/postgresql:/var/lib/postgresql/data \
    registry.cn-shenzhen.aliyuncs.com/fast-wiki/pgvector:v0.5.0
```

启动fast-wiki-service, 请将`{您的TokenKey}`替换为您的OpenAI Key，如果需要使用自己的代理则修改`OPENAI_CHAT_ENDPOINT`和`OPENAI_CHAT_EMBEDDING_ENDPOINT`为自己的代理地址。
默认使用`Development`会迁移数据库，如果不需要迁移数据库则修改`ASPNETCORE_ENVIRONMENT`为`Production`，首次启动会迁移数据库，迁移完成后再次启动时请修改`ASPNETCORE_ENVIRONMENT`为`Production`。

```shell
docker run -d --name fast-wiki-service \
    --network=fast-wiki-network \
    -p 8080:8080 \
    -v $(pwd)/wwwroot:/app/wwwroot/ \
    -e OPENAI_CHAT_ENDPOINT=https://api.openai.com \
    -e OPENAI_CHAT_EMBEDDING_ENDPOINT=https://api.openai.com \
    -e OPENAI_CHAT_TOKEN={您的TokenKey} \
    -e OPENAI_CHAT_MODEL=gpt-3.5-turbo \
    -e OPENAI_EMBEDDING_MODEL=text-embedding-3-small \
    -e ASPNETCORE_ENVIRONMENT=Development \
    registry.cn-shenzhen.aliyuncs.com/fast-wiki/fast-wiki-service
```