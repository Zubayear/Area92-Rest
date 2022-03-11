FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Area92.Client/Area92.Client.csproj", "Area92.Client/"]
RUN dotnet restore "Area92.Client/Area92.Client.csproj"
COPY . .
WORKDIR "/src/Area92.Client"
RUN dotnet build "Area92.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Area92.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Area92.Client.dll"]
