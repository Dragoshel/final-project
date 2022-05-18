FROM mcr.microsoft.com/dotnet/sdk:6.0

EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

WORKDIR /app

COPY final-project.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build

RUN dotnet dev-certs https

ENTRYPOINT [ "dotnet", "watch" ]