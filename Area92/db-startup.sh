#!/bin/sh

`docker run \
    --name anime-container \
    --volume "$PWD/mssqlsystem:/var/opt/mssql" \
    --volume "$PWD/mssqluser:/var/opt/sqlserver" \
    -e "ACCEPT_EULA=Y" \
    -e "SA_PASSWORD=SIMinABurner@24" \
    -p 1433:1433 \
    -d mcr.microsoft.com/mssql/server:2019-latest`