# Changelog

All notable changes to this project will be documented in this file.

The format is based on *Keep a Changelog* and this project aims to follow *Semantic Versioning*.

## [1.0.8] - TBD

### Security

- Updated the transitive `System.Text.Json` dependency to `10.0.11` to remediate CVE-2024-43485.

### Changed

- Updated `Newtonsoft.Json` to `13.0.4`.
- Updated `RestSharp` to `114.0.0`.
- Updated the OpenAPI coverage tool's `Microsoft.OpenApi.Readers` dependency to `1.6.31`.
- Updated the offline unit-test stack to `Microsoft.NET.Test.Sdk` `18.9.0` and MSTest `4.4.0`.
- Migrated the live integration-test project to SDK-style `PackageReference` and the same current test stack.
- Updated integration-test exception assertions and binding redirects for MSTest 4 compatibility.
- Added NuGet package lock files.


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
