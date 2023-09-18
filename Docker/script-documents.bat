@echo off

cd C:\Users\lstro\source\repos\SageOnline\SageOnline

docker build -f Docker/dockerfile.API.Documents -t lucoxs/sol_api_documents:latest .
docker image push lucoxs/sol_api_documents:latest

cd Docker

docker-compose -f docker-compose-documents.yml up -d

pause