@echo off

cd C:\Users\lstro\source\repos\SageOnline\SageOnline

docker build -f Docker/dockerfile.API.Gateway -t lucoxs/sol_api_gateway:latest .
docker image push lucoxs/sol_api_gateway:latest

pause