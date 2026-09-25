# Changelog

All notable changes to this project will be documented in this file.

The format is based on *Keep a Changelog* and this project aims to follow *Semantic Versioning*.

## [2.0.0] - TBD

### Breaking changes

- Upgraded RestSharp from `112.0.0` to `114.0.0`. The SDK exposes RestSharp types through its public API, including `IHttpClient` and `DynamicJsonSerializer`, so this release requires a major version bump from `1.0.7`. Rebuild consuming applications and libraries, align direct RestSharp references with `114.0.0`, and review the [migration guidance](README.md#upgrading-from-107-to-200).

### Added

- Domain and inbox webhook injection via `PostWebhookMessageAsync` and `PostWebhookInboxMessageAsync`, supporting both webhook authentication forms, rich JSON payloads, offline unit tests, and opt-in integration tests.
- `MessagesClient.GetMessageTextAsync`, `GetMessageTextPlainAsync`, and `GetMessageTextHtmlAsync` with request/response models, offline route and JSON deserialization tests, and opt-in read-only integration tests.
- `MessagesClient.GetMessageSummaryAsync` with request/response models, offline request and deserialization tests, and an opt-in live test for an existing email.
- `MessagesClient.GetMessageHeadersAsync` with request/response models, offline request and deserialization tests, and an opt-in live test for an existing email.
- `MessagesClient.ListDomainMessagesAsync` and `ListDomainMessagesRequest` for domain-wide message listing, with optional inbox filtering and all documented listing parameters. Returns the existing `FetchInboxResponse` model.
- Offline tests for domain listing query parameters, default behavior, and wildcard filtering.
- Offline request-construction coverage for existing API clients, including routes, path parameters, query parameters, and request bodies.
- NuGet package lock files for the SDK, test projects, and OpenAPI coverage tool.

### Security

- Updated `System.Text.Json` to `10.0.12` via a direct package reference, retaining the remediation for CVE-2024-43485.

### Changed

- Updated `Newtonsoft.Json` to `13.0.4`.
- Migrated the development-only OpenAPI coverage tool to `Microsoft.OpenApi.YamlReader` / `Microsoft.OpenApi` `3.10.2`, replacing `Microsoft.OpenApi.Readers` and using asynchronous loading and the new parameter reference model. Coverage output is unchanged for the same Mailinator specification. These packages are not dependencies of the published SDK.
- Retained the coverage reader's transitive SharpYaml `2.1.5` dependency: testing `3.14.0` produces a `MissingMethodException` because its parser API is binary-incompatible with the current Microsoft reader.
- Updated both test projects to `Microsoft.NET.Test.Sdk` `18.10.1`, `MSTest.TestAdapter` `4.4.1`, and `MSTest.TestFramework` `4.4.1`.
- Migrated the live integration-test project to SDK-style `PackageReference` and the same current test stack.
- Updated integration-test exception assertions and binding redirects for MSTest 4 compatibility.
- Shared `.env` loading across integration tests while preserving existing environment-variable values.
- Refreshed NuGet lock files for the updated direct and transitive dependencies.
- Updated the README with an API reference link and migration guidance from `1.0.7` to `2.0.0`; clarified sort query values in the examples. SDK target frameworks remain .NET Framework 4.7.1 and .NET Standard 2.0.

### Deprecated

- `AuthenticatorsClient.GetAuthenticatorsAsync`, `GetAuthenticatorAsync`, and `GetAuthenticatorByIdAsync`; use `GetAuthenticatorsByIdAsync` for the documented stored-authenticator operation.

### Fixed

- Corrected sort query serialization in `ListDomainMessagesAsync` and `FetchInboxAsync`: `Sort.asc` sends `ascending` and `Sort.desc` sends `descending`, matching the OpenAPI contract. Public enum names remain unchanged. Added offline regression coverage for both sort directions on both methods.
- Forwarded the optional `delete` query parameter in `FetchInboxMessageAsync`.


## [1.0.7] - 2026-08-15

### Added

- `ROADMAP.md`
- `CHANGELOG.md`
- `AGENTS.md`
- `EXAMPLES.md`
- `REFERENCE.md`, documenting the SDK's current public operations and deprecations.
- `TESTING.md`, separating offline unit tests from the opt-in live integration suite.

### Changed

- Rewrote the package README with installation, quick-start, authentication, and development guidance.

### Deprecated

- All `RulesClient` endpoints (`CreateRuleAsync`, `DeleteRuleAsync`, `EnableRuleAsync`, `DisableRuleAsync`, `GetAllRulesAsync`, `GetRuleAsync`).
- `DomainsClient` create/delete endpoints (`CreateDomainAsync`, `DeleteDomainAsync`).
- Messages “Latest” wildcard endpoints (`FetchLatestMessagesAsync`, `FetchLatestInboxMessagesAsync`).
