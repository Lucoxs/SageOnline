@echo off

cd C:\Users\lstro\source\repos\SageOnline\SageOnline

docker build -f dockerfiles/dockerfile.MSSQL.Documents -t sol_mssql_documents:latest .
docker image tag sol_mssql_documents:latest lucoxs/sol_mssql_documents:latest
docker image push lucoxs/sol_mssql_documents:latest

docker build -f dockerfiles/dockerfile.API.Documents -t sol_api_documents:latest .
docker image tag sol_api_documents:latest lucoxs/sol_api_documents:latest
docker image push lucoxs/sol_api_documents:latest

docker build -f dockerfiles/dockerfile.API.Gateway -t sol_api_gateway:latest .
docker image tag sol_api_gateway:latest lucoxs/sol_api_gateway:latest
docker image push lucoxs/sol_api_gateway:latest

docker build -f dockerfiles/dockerfile.API.Identity -t sol_api_identity:latest .
docker image tag sol_api_identity:latest lucoxs/sol_api_identity:latest
docker image push lucoxs/sol_api_identity:latest

pause