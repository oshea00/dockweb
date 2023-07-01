FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 5000
COPY publish/ /app
WORKDIR /app

ENV ASPNETCORE_URLS "http://0.0.0.0:5000"
ENTRYPOINT ["dotnet", "dockweb.dll"]
