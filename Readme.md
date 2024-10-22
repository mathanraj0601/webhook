# Webhooks

This repository demonstrates a one-way pub-sub based event-driven communication system using webhooks. The project leverages several technologies to implement and showcase this communication pattern.

## Block Digram

Below is the block diagram illustrating the architecture and flow of the webhook system:
![image](https://github.com/mathanraj0601/webhook/assets/98396468/993c6329-39af-4be0-93fa-3db72f11314d)

## Local Setup
- Clone the repo and move into the folder
- Run `dotnet restore` in all ASP.NET and Console App
- Run `dotnet run` in all ASP.NET and Console App
- Install rabbit mq or spin up a rabbit mq container using `docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management`

## Demo
https://github.com/mathanraj0601/webhook/assets/98396468/db2c0014-cadc-4b93-a567-7bf4e24068f6

## Technology used
- ASP.NET Web API
- .NET Console APP
- Rabbit Mq
- React

## Learning documentation

Curious to dive deeper? I've documented my learnings and insights throughout this project. Read the documentation [Here](https://deeply-sneeze-d1c.notion.site/Webhooks-180c7cadfa464cf1aa3c1188c4e4e718).

