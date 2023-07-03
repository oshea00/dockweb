# Dockerized ASP.Net w/ GraphQL

A small sample project showing how to do a basic, non-trivial, graphql api. As a starting point, it starts up a server with jwt authentication/authorization and a small book/author schema using a service to serve data.

## Running
* F5 to build/debug (or use ```dotnet watch```)

## Docker using the Docker extension
Publish to local "publish" directory
```
> dotnet publish -c Release -o publish
```
Use Dockerfile VSCode extension to build container or:
```
> docker build -t dockweb:latest . 
```
Run container using Docker extension or manually:
```
docker run --rm -it -p 5000:5000/tcp dockweb:latest
```
>



