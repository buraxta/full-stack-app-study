# 1. Node.js 20'yi kullan
FROM node:18-alpine AS build
WORKDIR /app

# 2. Bağımlılıkları yükle
COPY package.json package-lock.json ./
RUN npm install

# 3. Tüm projeyi kopyala ve Next.js build et
COPY . .
RUN npm run build

# 4. Production için bir runtime ortamı oluştur
FROM node:18-alpine AS runtime
WORKDIR /app
COPY --from=build /app . 

# 5. Next.js uygulamasını başlat
CMD ["npm", "start"]
