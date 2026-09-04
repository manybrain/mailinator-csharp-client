# Roadmap

This document is a living roadmap for the Mailinator C# client. It’s intentionally high-level and should evolve as we audit the SDK against the Mailinator OpenAPI spec and customer needs.

## Goals

- Stay aligned with the Mailinator OpenAPI specification.
- Maintain backwards compatibility where practical (or document breaking changes clearly).
- Provide clear, copy/pasteable examples for common workflows.
- Make releases predictable and easy to consume.

## Current Status:

- Target frameworks: `net471`; `netstandard2.0`
- API coverage vs spec: see “Gap Analysis”
- Known gaps / bugs: missing spec endpoints and documented SDK-only compatibility operations. Path parameter-name differences and the SMS convenience alias are intentional and do not change the resulting HTTP route.

## Dependency Maintenance

Dependencies were audited on 2026-09-04. All projects have a clean NuGet vulnerability audit, and committed package lock files make restores reproducible.

## Gap Analysis (2026-09-04)

This snapshot compares the SDK’s implemented operations to the Mailinator OpenAPI spec (`mailinator-api.yaml`).

- Spec operations: 35
- SDK operations: 43
- Exact matches: 21
- Missing from SDK: 10
- SDK-only (no spec match): 17
- SDK aliases / convenience wrappers: 1
- Path parameter-name mismatches: 4
- Operations with missing query params: 0

Re-run locally:

- Fetch the spec YAML and compare it to `mailinator-csharp-client/Clients/ApiClients/**` operations (method + effective path + query params).
- Or run `dotnet run --project eng/OpenApiCoverageCheck -- --spec path/to/mailinator-api.yaml`.

### Work Items (spec → SDK)

Add these operations that exist in the spec but are missing from the SDK:

- **Messages**
  - `listDomainMessages` — `GET /api/v2/domains/{domain}/inboxes`
  - `getMessageHeaders` — `GET /api/v2/domains/{domain}/messages/{messageId}/headers`
  - `getMessageSummary` — `GET /api/v2/domains/{domain}/messages/{messageId}/summary`
  - `getMessageText` — `GET /api/v2/domains/{domain}/messages/{messageId}/text`
  - `getMessageTextHtml` — `GET /api/v2/domains/{domain}/messages/{messageId}/texthtml`
  - `getMessageTextPlain` — `GET /api/v2/domains/{domain}/messages/{messageId}/textplain`
  - `streamDomainMessages` — `GET /api/v2/domains/{domain}/stream`
  - `streamInboxMessages` — `GET /api/v2/domains/{domain}/stream/{inbox}`
- **Webhook**
  - `postWebhookMessage` — `POST /api/v2/domains/{domain}/webhook`
  - `postWebhookInboxMessage` — `POST /api/v2/domains/{domain}/webhook/{inbox}`

### Work Items (SDK → spec)

These SDK operations do not have a matching operation in the current OpenAPI spec. All remaining cases now have a compatibility decision recorded below.

- **Webhooks** private/custom-service endpoints (`POST /api/v2/domains/private/...`) are intentional, supported compatibility APIs shared with the JavaScript client. Keep them documented and do not deprecate them solely because they are absent from the current spec.

### Resolved compatibility decisions

The following operations are already deprecated. Retain them for source compatibility and skip further spec-alignment work; remove them only in a future breaking major release.

- **Rules** — 6 operations under `/api/v2/domains/{domain_id}/rules...`
- **Domains** — create/delete (`POST`/`DELETE /api/v2/domains/{domain_id}`)
- **Authenticators** — the unsupported list/get variants under `/api/v2/authenticator...` and `/api/v2/authenticators` are deprecated; retain them only until a future breaking major release.
- **Messages** — “latest” wildcard endpoints (`GET .../messages/*`)

### Work Items (spec alignment)

Path template parameter names differ from the spec. These are intentional, non-functional differences; the resulting HTTP routes are the same, so no SDK change is planned:

- Attachments: `{attachmentName}` (spec) vs `{attachmentId}` (SDK)
- Authenticators: `{authenticator_id}` (spec) vs `{auth_id}` (SDK)
- Domains: `{domain_name}` (spec) vs `{domain_id}` (SDK)

`FetchSMSMessagesAsync` is also an intentional convenience alias for inbox retrieval using the team SMS number. `FetchInboxAsync` remains available when callers need the full inbox-listing parameter set.

## Near-Term (next 1–3 updates)

- Keep gap analysis up to date (re-run after changes).
- Decide on versioning and release cadence.
- Implement missing spec endpoints (see “Work Items (spec → SDK)”).
- Improve docs: examples, configuration, troubleshooting.

## Mid-Term

- Improve test coverage.
- Add more ergonomic APIs / helpers while keeping the low-level request mapping.

## Long-Term

- Automate spec drift detection and regeneration / validation workflows.
- Improve observability and diagnostics (logging hooks, request/response tracing).

## Out of Scope (for now)

- Anything that depends on undocumented endpoints without confirmation.
