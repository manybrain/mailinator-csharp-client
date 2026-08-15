# Mailinator C# SDK

The official C# SDK for the [Mailinator API](https://www.mailinator.com/documentation/docs/api/index.html). This package is a thin, asynchronous wrapper around the Mailinator REST API, and the [Mailinator OpenAPI specification](https://github.com/manybrain/mailinatordocs/blob/main/openapi/mailinator-api.yaml) is the source of truth for documented endpoints.

The SDK targets .NET Framework 4.7.1 and .NET Standard 2.0.

## Installation

Install the `MailinatorApiClient` package from [NuGet](https://www.nuget.org/packages/MailinatorApiClient):

```sh
dotnet add package MailinatorApiClient
```

Package Manager Console:

```powershell
Install-Package MailinatorApiClient
```

PackageReference:

```xml
<PackageReference Include="MailinatorApiClient" Version="YOUR_VERSION" />
```

## Quick start

Create a Mailinator account, then obtain an API token from **Team Settings > API Tokens**. Keep the token outside your source code—for example, in an environment variable.

```csharp
using System;
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;

var apiToken = Environment.GetEnvironmentVariable("MAILINATOR_API_TOKEN");
if (string.IsNullOrEmpty(apiToken))
{
    throw new InvalidOperationException("Set MAILINATOR_API_TOKEN before running this example.");
}

var client = new MailinatorClient(apiToken);
var response = await client.MessagesClient.FetchInboxAsync(
    new FetchInboxRequest
    {
        Domain = "your-private-domain.com",
        Inbox = "your-inbox",
        Skip = 0,
        Limit = 20,
        Sort = Sort.desc
    });
```

All API operations are asynchronous and end in `Async`. Operations are grouped under `MessagesClient`, `DomainsClient`, `AuthenticatorsClient`, `StatsClient`, `WebhooksClient`, and `RulesClient`.

## API reference

- [Mailinator API reference](https://www.mailinator.com/documentation/docs/api/index.html) describes the REST API.
- [REFERENCE.md](REFERENCE.md) lists the operations currently exposed by this SDK.
- [EXAMPLES.md](EXAMPLES.md) contains examples for common SDK workflows.

## Authentication

Construct `MailinatorClient` with an API token for messages, domains, authenticators, stats, and rules:

```csharp
var client = new MailinatorClient(apiToken);
```

Webhook injection uses a webhook token in the request URL and does not use an API token. The parameterless client constructor initializes `WebhooksClient` for these calls:

```csharp
var webhookClient = new MailinatorClient();
```

See the [webhook examples](EXAMPLES.md#webhooks) for complete requests.

## Deprecated APIs

Some older SDK operations do not appear in the current OpenAPI specification. They remain available for compatibility but are marked with `[Obsolete]` and may be removed in a future major release. See the deprecation notes in [REFERENCE.md](REFERENCE.md#deprecated-operations) and the alignment work in [ROADMAP.md](ROADMAP.md).

## Development

Build the solution when the installed .NET SDK supports all target frameworks:

```sh
dotnet build mailinator-csharp-client.sln
```

Run the fast, offline unit tests:

```sh
dotnet test mailinator-csharp-client-unit-tests/mailinator-csharp-client-unit-tests.csproj
```

The separate legacy integration suite calls the live Mailinator API and requires deliberate account configuration. Some tests create or delete remote resources. Read [TESTING.md](TESTING.md) before running it.

To compare the SDK request surface with the OpenAPI specification, use the [OpenAPI coverage check](eng/README.md#openapi-coverage-check).
