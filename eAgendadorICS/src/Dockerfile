# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos do projeto e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia tudo e publica em modo release
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia a build gerada na etapa anterior
COPY --from=build /app/publish .

# Expor portas 80 (HTTP) e 443 (HTTPS)
EXPOSE 80
EXPOSE 443

# Inicia a aplicação
ENTRYPOINT ["dotnet", "AgendaICS.dll"]
