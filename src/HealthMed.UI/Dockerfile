FROM node:17.9-alpine as node
WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install -g @angular/cli@14.2.9 && npm install --legacy-peer-deps

COPY . .
RUN export NODE_OPTIONS=--openssl-legacy-provider && npm run build:docker

# CMD ["export", "NODE_OPTIONS=--openssl-legacy-provider", "&&", "ng", "serve", "--host", "0.0.0.0"]

FROM nginx:alpine
VOLUME /var/cache/nginx

COPY --from=node app/dist/app usr/share/nginx/html
COPY nginx/nginx.dev.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
