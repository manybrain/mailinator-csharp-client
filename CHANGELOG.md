# Changelog

All notable changes to this project will be documented in this file.

The format is based on *Keep a Changelog* and this project aims to follow *Semantic Versioning*.


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
