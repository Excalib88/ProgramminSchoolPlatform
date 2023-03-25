FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProgrammingSchool.Web/ProgrammingSchool.Web.csproj", "ProgrammingSchool.Web/"]
RUN dotnet restore "ProgrammingSchool.Web/ProgrammingSchool.Web.csproj"
COPY . .
WORKDIR "/src/ProgrammingSchool.Web"
RUN dotnet build "ProgrammingSchool.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgrammingSchool.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProgrammingSchool.Web.dll"]
