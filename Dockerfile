# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnetbuild
WORKDIR /app  
EXPOSE 8080

COPY ["TaskManager.WebApi/TaskManager.WebApi.csproj", "TaskManager.WebApi/"]
COPY ["TaskManager.Model/TaskManager.Model.csproj", "TaskManager.Model/"]
COPY ["TaskManager.EFRepository/TaskManager.EFRepository.csproj", "TaskManager.EFRepository/"]
COPY ["TaskManager.IRepository/TaskManager.IRepository.csproj", "TaskManager.IRepository/"]
COPY ["TaskManager.Service/TaskManager.Service.csproj", "TaskManager.Service/"]

RUN dotnet restore "TaskManager.WebApi/TaskManager.WebApi.csproj" 

COPY . .
WORKDIR "/app/TaskManager.WebApi"
RUN dotnet build "TaskManager.WebApi.csproj" -c Release -o /app/build

FROM dotnetbuild as publish
RUN dotnet publish "TaskManager.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.WebApi.dll"]