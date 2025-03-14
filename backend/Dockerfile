# Base image olarak .NET Runtime kullanıyoruz (sadece çalıştırma için)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# Build için .NET SDK içeren image kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# EF CLI'yi yüklüyoruz (Bunu buraya eklemelisin!)
RUN dotnet tool install --global dotnet-ef \
    && export PATH="$PATH:/root/.dotnet/tools"

# Projeyi kopyala ve restore et
COPY ["backend.csproj", "./"]
RUN dotnet restore "backend.csproj"

# Geri kalan dosyaları kopyala ve build yap
COPY . .
WORKDIR "/src"
RUN dotnet build "backend.csproj" -c Release -o /app/build

# Publish aşaması
FROM build AS publish
RUN dotnet publish "backend.csproj" -c Release -o /app/publish

# Final image (Çalıştırılabilir versiyon)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]
