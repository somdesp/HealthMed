# Etapa 1: Construir a aplicação Angular
FROM node:18 AS build

# Definir o diretório de trabalho
WORKDIR /app

# Copiar o package.json e instalar dependências
COPY package*.json ./
RUN npm install

# Copiar o restante dos arquivos da aplicação
COPY . .

# Construir a aplicação Angular para produção
RUN npm run build --docker

# Etapa 2: Servir a aplicação com Nginx
FROM nginx:alpine

# Copiar os arquivos de build do Angular para o diretório do Nginx
COPY --from=build /app/dist/meu-app-angular /usr/share/nginx/html

# Expor a porta 80
EXPOSE 80

# Iniciar o servidor Nginx
CMD ["nginx", "-g", "daemon off;"]
