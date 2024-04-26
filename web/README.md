# FastWiki前端项目

安装依赖

```sh
yarn
```

再当前目录下创建`.env`然后添加内容

```env
VITE_FAST_API_URL=http://localhost:5124
```

启动项目

```sh
yarn run dev
```

构建项目

如果您需要发布到生产环境的话使用docker部署的话只需要构建后端服务，镜像会自动构建前端项目

如果你需要自己发布到服务器可执行

```sh
yarn run build
```
