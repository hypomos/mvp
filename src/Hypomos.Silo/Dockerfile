FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY /artifacts/Hypomos.Silo .
ENTRYPOINT ["dotnet", "Hypomos.Silo.dll"]