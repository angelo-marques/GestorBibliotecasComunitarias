FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GestorBibliotecaComunitaria/GestorBiblioteca.API/GestorBiblioteca.API.csproj", "GestorBibliotecaComunitaria/GestorBiblioteca.API/"]
COPY ["GestorBibliotecaComunitaria/GestorBiblioteca.Application/GestorBiblioteca.Application.csproj", "GestorBibliotecaComunitaria/GestorBiblioteca.Application/"]
COPY ["GestorBibliotecaComunitaria/GestorBiblioteca.Domain/GestorBiblioteca.Domain.csproj", "GestorBibliotecaComunitaria/GestorBiblioteca.Domain/"]
COPY ["GestorBibliotecaComunitaria/GestorBiblioteca.Infrastructure/GestorBiblioteca.Infrastructure.csproj", "GestorBibliotecaComunitaria/GestorBiblioteca.Infrastructure/"]
RUN dotnet restore "GestorBibliotecaComunitaria/GestorBiblioteca.API/GestorBiblioteca.API.csproj"
COPY . .
WORKDIR "/src/GestorBibliotecaComunitaria/GestorBiblioteca.API"
RUN dotnet build "GestorBiblioteca.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestorBiblioteca.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestorBiblioteca.API.dll"]