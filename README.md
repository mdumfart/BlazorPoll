# BlazorPoll

BlazorPoll is a prototype for creating and voting on single/multiple choice polls.
The goal is a proof of work for Microsoft Blazor WebAssembly.

## Prerequisites

-   .Net SDK 5.x

## Quickstart

1. Clone the repository
2. Navigate into BlazorPoll/Docker and run `docker compose up -d`
3. Open BlazorPoll solution in Visual Studio
4. In Visual Studio open Package Manager Console (Tools --> NuGet Package Manager --> Package Manager Console) and run `update-database`
5. In Visual Studio open Developer PowerShell (View --> Terminal), navigate into BlazorPoll/BlazorPoll/Server and run `dotnet run`
6. Open Application at `https://localhost:5001`
