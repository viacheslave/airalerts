FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

COPY ./src/AirAlerts.Application/AirAlerts.Application.csproj \
      /usr/src/app/src/AirAlerts.Application/AirAlerts.Application.csproj

COPY ./src/AirAlerts.Console/AirAlerts.Console.csproj \
      /usr/src/app/src/AirAlerts.Console/AirAlerts.Console.csproj

COPY ./src/AirAlerts.Jobs/AirAlerts.Jobs.csproj \
      /usr/src/app/src/AirAlerts.Jobs/AirAlerts.Jobs.csproj

COPY ./src/AirAlerts.TelegramReporter/AirAlerts.TelegramReporter.csproj \
      /usr/src/app/src/AirAlerts.TelegramReporter/AirAlerts.TelegramReporter.csproj

COPY ./tests/AirAlerts.Tests/AirAlerts.Tests.csproj \
      /usr/src/app/tests/AirAlerts.Tests/AirAlerts.Tests.csproj

COPY ./AirAlerts.sln /usr/src/app/AirAlerts.sln

WORKDIR /usr/src/app
RUN dotnet restore

COPY . /usr/src/app
RUN dotnet build -c Release
RUN dotnet publish -o "artifact" -c Release -r ubuntu.20.04-x64 --self-contained false /p:PublishSingleFile=true "src/AirAlerts.Console/AirAlerts.Console.csproj"

FROM mcr.microsoft.com/dotnet/runtime:6.0 as release
WORKDIR /usr/src/app
COPY --from=build /usr/src/app/artifact .

CMD [ "./AirAlerts.Console" ]