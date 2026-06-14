# Roadmap

This document is a living roadmap for the Mailinator C# client. It’s intentionally high-level and should evolve as we audit the SDK against the Mailinator OpenAPI spec and customer needs.

## Goals

- Stay aligned with the Mailinator OpenAPI specification.
- Maintain backwards compatibility where practical (or document breaking changes clearly).
- Provide clear, copy/pasteable examples for common workflows.
- Make releases predictable and easy to consume.

## Current Status (to fill in)

- Latest published package version:
- Target frameworks:
- API coverage vs spec:
- Known gaps / bugs:

## Gap Analysis (2026-03-23)

This snapshot compares the SDK’s implemented operations to the Mailinator OpenAPI spec (`mailinator-api.yaml`).

- Spec operations: 35
- SDK operations: 43
- Exact matches: 21
- Missing from SDK: 10
- SDK-only (no spec match): 17
- SDK aliases / convenience wrappers: 1
- Path parameter-name mismatches: 4
- Operations with missing query params: 1

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

These SDK operations do not have a matching operation in the current OpenAPI spec. Decide for each group whether to (a) update the spec, (b) deprecate/remove the SDK surface, or (c) keep but document explicitly as “not in spec”.

- **Rules** (6 operations under `/api/v2/domains/{domain_id}/rules...`)
- **Domains** create/delete (`POST`/`DELETE /api/v2/domains/{domain_id}`)
- **Authenticators** list/get variants (`/api/v2/authenticator...` and `/api/v2/authenticators`)
- **Messages** “latest” wildcard endpoints (`GET .../messages/*`)
- **Webhooks** private/custom-service endpoints (`POST /api/v2/domains/private/...`)

### Work Items (spec alignment)

Path template parameter names differ from the spec (non-breaking, but worth aligning for clarity and consistency):

- Attachments: `{attachmentName}` (spec) vs `{attachmentId}` (SDK)
- Authenticators: `{authenticator_id}` (spec) vs `{auth_id}` (SDK)
- Domains: `{domain_name}` (spec) vs `{domain_id}` (SDK)

Query parameters differ from the spec:

- `GET /api/v2/domains/{domain}/inboxes/{inbox}/messages/{messageId}` is missing the optional `delete` query parameter in the SDK.

## Near-Term (next 1–3 updates)

- Keep gap analysis up to date (re-run after changes).
- Decide on versioning and release cadence.
- Implement missing spec endpoints (see “Work Items (spec → SDK)”).
- Resolve spec alignment issues (path template parameter names).
- Make an explicit decision on SDK-only endpoints (spec update vs deprecate vs document).
- Improve docs: examples, configuration, troubleshooting.

## Mid-Term

- Improve test coverage and add integration test guidance.
- Add more ergonomic APIs / helpers while keeping the low-level request mapping.

## Long-Term

- Automate spec drift detection and regeneration / validation workflows.
- Improve observability and diagnostics (logging hooks, request/response tracing).

## Out of Scope (for now)

- Anything that depends on undocumented endpoints without confirmation.
