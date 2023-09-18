@echo off

cd C:\Users\lstro\source\repos\SageOnline\SageOnline

docker build -f Docker/dockerfile.API.Documents -t lucoxs/sol_api_documents:latest .
docker image push lucoxs/sol_api_documents:latest

docker build -f Docker/dockerfile.API.Identity -t lucoxs/sol_api_identity:latest .
docker image push lucoxs/sol_api_identity:latest

docker build -f Docker/dockerfile.API.Gateway -t lucoxs/sol_api_gateway:latest .
docker image push lucoxs/sol_api_gateway:latest

pause