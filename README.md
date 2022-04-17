# AirAlerts Chances

A weekend pet project. As of now, we have got a bunch of services that notify about ongoing Air Alerts. This console application periodically fetches that public data and calculates a probablility of Air Alert in particular region (Lviv region). Calculations are based on enabled alerts in neighbouring states and districts, depending on their remoteness from Lviv region.

The application reports values to a Telegram channel via Telegram Bot API.

## Settings

Set Telegram Bot API Key and Telegram Chat ID in `AppConfiguration.cs`.

## Prerequisites

- Windows / Linux
- Docker

## Stack

.NET 6

## Run

```
docker build . airalerts
docker run -it airalerts
```


