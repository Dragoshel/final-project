FROM mcr.microsoft.com/dotnet/sdk:6.0 as base

ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

WORKDIR /app
COPY ./final-project/final-project.csproj ./final-project/
RUN dotnet restore final-project

COPY ./final-project/ ./final-project/
RUN dotnet build final-project
RUN dotnet dev-certs https

FROM base as test

COPY ./final-project-tests/final-project-tests.csproj ./final-project-tests/
RUN dotnet restore final-project-tests

COPY ./final-project-tests/ ./final-project-tests/
RUN dotnet build final-project-tests 

CMD [ "dotnet", "test", "final-project-tests"]

FROM base as app

EXPOSE 5001

ENTRYPOINT [ "dotnet", "watch", "--project", "final-project"]