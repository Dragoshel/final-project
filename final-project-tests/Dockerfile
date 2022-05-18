FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

COPY final-project-tests.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build

ENTRYPOINT [ "dotnet", "test" ]