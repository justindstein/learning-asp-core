Per tutorial: https://www.youtube.com/watch?v=fFpDf5si_Hw
1- Install Docker Desktop at https://www.docker.com/ > AMD64
2- https://hub.docker.com/r/microsoft/mssql-server
```docker pull mcr.microsoft.com/mssql/server:2019-latest```
3- startup 
```docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Ahead123!" -p 1433:1433 -d --name sqlserver mcr.microsoft.com/mssql/server:2019-latest```
4- download https://azure.microsoft.com/en-us/products/data-studio
5- Add connection
Server: localhost
Authentication type: SQL Login
User name: sa
Password: Ahead123!
