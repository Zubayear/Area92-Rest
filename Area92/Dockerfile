FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Area92/Area92.csproj", "Area92/"]
RUN dotnet restore "Area92/Area92.csproj"
COPY . .
WORKDIR "/src/Area92"
RUN dotnet build "Area92.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Area92.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Area92.dll"]
