FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DebitManagement.API/DebitManagement.API.csproj", "DebitManagement.API/"]
COPY ["DebitManagement.Base/DebitManagement.Base.csproj", "DebitManagement.Base/"]
COPY ["DebitManagement.Core/DebitManagement.Core.csproj", "DebitManagement.Core/"]
COPY ["DebitManagement.Infrastructure/DebitManagement.Infrastructure.csproj", "DebitManagement.Infrastructure/"]
COPY ["DebitManagement.Service/DebitManagement.Service.csproj", "DebitManagement.Service/"]
RUN dotnet restore "DebitManagement.API/DebitManagement.API.csproj"
COPY . .
WORKDIR "/src/DebitManagement.API"
RUN dotnet build "DebitManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DebitManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DebitManagement.API.dll"]
