FROM nginx:stable-alpine AS base
WORKDIR /app
EXPOSE 80

FROM node:16-alpine3.15 AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
RUN npm config set registry https://registry.npm.taobao.org
RUN npm i
RUN npm run build

FROM base AS docs-web
WORKDIR /wwwroot
COPY --from=build /src/build /wwwroot/
COPY docs.conf /etc/nginx/conf.d/default.conf