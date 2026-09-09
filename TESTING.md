# Testing

The repository has two distinct MSTest suites: fast offline unit tests and legacy tests that call the live Mailinator API.

## Offline unit tests

The .NET 8 unit-test project verifies request construction for every public async SDK operation without making network calls. It asserts each operation's HTTP method, route, path/query parameters, and JSON body where applicable:

```sh
dotnet test mailinator-csharp-client-unit-tests/mailinator-csharp-client-unit-tests.csproj
```

These tests do not require Mailinator credentials.

## Live integration tests

The `mailinator-csharp-client-tests` project is a legacy .NET Framework 4.7.2 MSTest project. It requires a compatible Windows/.NET Framework environment and a deliberately configured Mailinator account.

The suite can create or delete domains, rules, and messages. Do not run it with production credentials. Review each selected test before supplying values that authorize deletion.

Copy the example configuration at the repository root:

```sh
cp .env.example .env
```

The `.env` file is excluded from Git. Process environment variables take precedence over values in the file. If `MAILINATOR_TEST_API_TOKEN` is absent, the integration suite is skipped.

### Configuration

| Variable | Purpose |
| --- | --- |
| `MAILINATOR_TEST_API_TOKEN` | API token used by authenticated integration tests. |
| `MAILINATOR_TEST_DOMAIN_PRIVATE` | Private domain used by domain and message tests. |
| `MAILINATOR_TEST_INBOX` | Existing inbox in the configured private domain. |
| `MAILINATOR_TEST_MESSAGE_ID` | Existing email in the configured private domain, for `GetMessageHeadersAsync` and `GetMessageSummaryAsync`. Header retrieval requires SMTP headers. |
| `MAILINATOR_TEST_PHONE_NUMBER` | Team SMS number whose messages can be fetched. |
| `MAILINATOR_TEST_MESSAGE_WITH_ATTACHMENT_ID` | ID of an existing message that has an attachment. |
| `MAILINATOR_TEST_ATTACHMENT_ID` | Attachment ID belonging to the configured message. |
| `MAILINATOR_TEST_DELETE_DOMAIN` | Domain that destructive domain-deletion tests may delete. |
| `MAILINATOR_TEST_WEBHOOKTOKEN_PRIVATEDOMAIN` | Webhook token for private-domain injection tests. |
| `MAILINATOR_TEST_WEBHOOKTOKEN_CUSTOMSERVICE` | Webhook token for custom-service injection tests. |
| `MAILINATOR_TEST_AUTH_SECRET` | Authenticator secret used by TOTP tests. |
| `MAILINATOR_TEST_AUTH_ID` | Authenticator ID used by lookup tests. |
| `MAILINATOR_TEST_WEBHOOK_INBOX` | Inbox used by webhook tests. |
| `MAILINATOR_TEST_WEBHOOK_CUSTOMSERVICE` | Custom-service name used by webhook tests. |

Run the integration suite only after restoring its legacy NuGet dependencies:

```sh
dotnet test mailinator-csharp-client-tests/mailinator-csharp-client-tests.csproj
```

Prefer a test filter when validating a specific endpoint, especially for operations that mutate remote state.

To test header retrieval only, configure `MAILINATOR_TEST_API_TOKEN`, `MAILINATOR_TEST_DOMAIN_PRIVATE`, and `MAILINATOR_TEST_MESSAGE_ID`, then run:

```sh
dotnet test mailinator-csharp-client-tests/mailinator-csharp-client-tests.csproj --filter "FullyQualifiedName=mailinator_csharp_client_tests.MessagesEndpointTests.GetMessageHeadersAsync"
```

Use an existing email received through SMTP with headers; an attachment is not required. This test only reads the message headers and does not create or delete messages. Missing configuration marks the test inconclusive.

Summary retrieval uses the same three environment variables and reads the existing message without creating or deleting data:

```sh
dotnet test mailinator-csharp-client-tests/mailinator-csharp-client-tests.csproj --filter "FullyQualifiedName=mailinator_csharp_client_tests.MessagesEndpointTests.GetMessageSummaryAsync"
```
