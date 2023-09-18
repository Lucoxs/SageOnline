@echo off

cd C:\Users\lstro\source\repos\SageOnline\SageOnline

docker build -f Docker/dockerfile.API.Identity -t lucoxs/sol_api_identity:latest .
docker image push lucoxs/sol_api_identity:latest

cd Docker

docker-compose -f docker-compose-identity.yml up -d

pause