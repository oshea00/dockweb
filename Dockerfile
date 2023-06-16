
FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80
COPY publish/ /app
WORKDIR /app
ENV ASPNETCORE_URLS="http://*:80"
ENTRYPOINT ["dotnet", "dockweb.dll"]
