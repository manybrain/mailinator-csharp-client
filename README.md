# Mailinator C# SDK

The official Mailinator C# SDK. This package is a thin, asynchronous wrapper around the Mailinator REST API, and the [Mailinator OpenAPI specification](https://github.com/manybrain/mailinatordocs/blob/main/openapi/mailinator-api.yaml) is the source of truth for documented endpoints.

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

## Upgrading from 1.0.7 to 2.0.0

Version 2.0.0 upgrades RestSharp from 112.0.0 to 114.0.0. RestSharp types are part of this SDK's public API, including `IHttpClient` and `DynamicJsonSerializer`, so consumers must account for RestSharp's breaking changes:

- Align any direct RestSharp references with 114.0.0 and rebuild consuming applications and libraries.
- If you implement RestSharp's `IAuthenticator`, update `Authenticate` to accept the optional `CancellationToken` parameter.
- If you access `ReadOnlyRestClientOptions.FollowRedirects` or `MaxRedirects`, migrate to `RedirectOptions`.
- Recompile code using RestSharp's generic parameter extension methods, whose signatures now include an optional culture parameter.

See the [RestSharp 114 changelog](https://restsharp.dev/docs/changelog/) for details. The SDK continues to target .NET Framework 4.7.1 and .NET Standard 2.0.

Message listing now sends the documented `ascending` and `descending` sort query values. Continue using `Sort.asc` and `Sort.desc` in C#.

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

For complete workflows, including domain message listing, message content, headers, attachments, and webhooks, see [EXAMPLES.md](EXAMPLES.md).

## Documentation

- [Mailinator API reference](https://www.mailinator.com/documentation/docs/api/index.html) describes the REST API.
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

Some older SDK operations do not appear in the current OpenAPI specification. They remain available for compatibility but are marked with `[Obsolete]` and may be removed in a future major release. See the compatibility decisions in [ROADMAP.md](ROADMAP.md#resolved-compatibility-decisions).

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

Maintainers should follow [RELEASING.md](RELEASING.md) to publish NuGet packages and matching GitHub Releases.
