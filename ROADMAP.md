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
- Known gaps / bugs: missing spec endpoints, SDK-only endpoints, path parameter-name mismatches, and one missing query parameter listed below.

## Dependency Maintenance

Audit refreshed: 2026-08-10.

Security status:

- No current direct dependency or package listed in `mailinator-csharp-client-tests/packages.config` falls within a known advisory range in NuGet's vulnerability feed.
- `RestSharp` `112.0.0` resolves `System.Text.Json` at `8.0.4` or newer for `net471` and `netstandard2.0`; the advisories currently listed for the 8.x line affect versions through `8.0.3`.
- The repository has no lock files, and the .NET SDK is not available in the current audit environment, so a restored full transitive graph could not be verified with `dotnet list package --vulnerable --include-transitive`.

Production and tooling dependencies:

- `Newtonsoft.Json`: current `13.0.3`; latest stable `13.0.4`.
- `RestSharp`: current `112.0.0`; latest stable `114.0.0`. Version `114.0.0` still supports `net471` and `netstandard2.0`, but raises its `System.Text.Json` dependency from `8.0.4` to `10.0.0` and requires API compatibility testing.
- `Microsoft.OpenApi.Readers`: current `1.6.29`; latest stable `1.6.29` (2.x remains preview-only).

Legacy test-project dependencies:

- `Microsoft.ApplicationInsights`: `2.22.0` → `3.1.2`.
- `Microsoft.Testing.Platform` and related extensions: `1.3.2` → `2.3.3`.
- `Microsoft.TestPlatform.ObjectModel`: `17.10.0` → `18.8.1`.
- `MSTest.TestAdapter` and `MSTest.TestFramework`: `3.5.2` → `4.3.3`.
- Explicitly pinned support packages are also behind: `System.Buffers` (`4.5.1` → `4.6.1`), `System.Collections.Immutable` (`1.5.0` → `10.0.10`), `System.Diagnostics.DiagnosticSource` (`5.0.0` → `10.0.10`), `System.Memory` (`4.5.4` → `4.6.3`), `System.Numerics.Vectors` (`4.5.0` → `4.6.1`), `System.Reflection.Metadata` (`1.6.0` → `10.0.10`), and `System.Runtime.CompilerServices.Unsafe` (`5.0.0` → `6.1.2`).

Work items:

- Update `Newtonsoft.Json` to `13.0.4` and run build/tests.
- Evaluate `RestSharp` `114.0.0` in a dedicated change; verify source compatibility, serialization behavior, all target frameworks, and the full SDK test suite.
- Convert the legacy `net472` test project from `packages.config` to SDK-style `PackageReference`, then upgrade the Microsoft testing packages as one coordinated stack and remove direct pins for transitive `System.*` dependencies where possible.
- Keep `Microsoft.OpenApi.Readers` on `1.6.29` until a stable 2.x release or a specific tooling requirement justifies a preview.
- Add lock files and a CI dependency check (`dotnet list package --vulnerable --include-transitive`) after restore tooling is available.

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
