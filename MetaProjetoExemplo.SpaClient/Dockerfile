FROM node:carbon-jessie AS build
COPY ./react-client/ /app/
WORKDIR /app/
RUN npm install
RUN npm run build


FROM nginx AS web
WORKDIR /
EXPOSE 80
COPY ./nginx.conf/ /etc/nginx/conf.d/default.conf
COPY --from=build /app/build/ /www/data/
ENTRYPOINT [ "nginx", "-g", "daemon off;" ]
