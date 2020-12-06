FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster
COPY bin/Release/netcoreapp3.1/publish app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll", "--urls", "http://0.0.0.0:5000"]