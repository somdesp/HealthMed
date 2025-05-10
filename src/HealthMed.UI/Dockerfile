# Etapa 1: Build da aplicação Angular
FROM node:18 AS build

WORKDIR /app

# Copia os arquivos de dependência e instala
COPY package*.json ./
RUN npm install

# Copia o restante do projeto e compila
COPY . .
RUN npm run build -- --configuration docker

# Etapa 2: Servir arquivos com Nginx
FROM nginx:alpine

# Remove arquivos padrão do Nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia os arquivos da build Angular para o Nginx
COPY --from=build /app/dist/health-med.ui/browser /usr/share/nginx/html

# (Opcional) Redirecionar rotas Angular para index.html
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
