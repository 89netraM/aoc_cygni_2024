FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine as builder
WORKDIR /aoc
COPY ./Day02.csproj .
RUN dotnet restore -r linux-musl-x64
COPY ./input.txt .
COPY ./Program.cs .
RUN dotnet build --no-restore -c Release -r linux-musl-x64
CMD dotnet run --no-build -c Release -r linux-musl-x64
