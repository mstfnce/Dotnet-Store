# 1. AŞAMA: Derleme (build stage) - .NET SDK içerir, kodu derlemek için gereken her şey burada
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Önce sadece .csproj'u kopyala ve paketleri indir (kod değişse de bu katman cache'ten gelir, hızlanır)
COPY dotnet-store.csproj .
RUN dotnet restore dotnet-store.csproj

# Şimdi tüm kaynak kodu kopyala ve Release modunda derleyip publish çıktısı üret
COPY . .
RUN dotnet publish dotnet-store.csproj -c Release -o /app/publish --no-restore

# 2. AŞAMA: Çalıştırma (final stage) - sadece runtime içerir, SDK yok, image küçük kalır
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Derlenmiş çıktıyı build aşamasından buraya kopyala (SDK'nın kendisi final image'a girmez)
COPY --from=build /app/publish .

# Uygulama 8080 portunda HTTP dinlesin
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Container ayağa kalkınca çalışacak komut
ENTRYPOINT ["dotnet", "dotnet-store.dll"]
