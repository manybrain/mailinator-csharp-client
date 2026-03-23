# AI Instructions

This document explains how the Mailinator C# client maps to the Mailinator OpenAPI specification, and how to audit/update the SDK when the spec changes.

**OpenAPI specification (source of truth):** [mailinator-api.yaml](https://github.com/manybrain/mailinatordocs/blob/main/openapi/mailinator-api.yaml)

## Codebase Structure

This repository is organized around the same logical groupings as the Mailinator API:

- **API clients:** `mailinator-csharp-client/Clients/ApiClients/*/*Client.cs`
  - Each folder under `ApiClients/` corresponds to an OpenAPI **tag** (e.g. `Messages`, `Domains`, `Rules`, `Stats`, `Authenticators`, `Webhooks`).
  - Public `*Async` methods on these clients are the SDK’s API surface.
- **HTTP plumbing:** `mailinator-csharp-client/Clients/HttpClient/HttpClient.cs`
  - Wraps RestSharp, sets the base URL, and (when constructed with an API token) sets the `Authorization` header.
  - Sets User-Agent based on the executing assembly version.
- **Models:** `mailinator-csharp-client/Models/<Tag>/...`
  - `Entities/` contain reusable schema types.
  - `Requests/` contain method input models (path/query/body values).
  - `Responses/` contain response body models and usually compose `Entities/`.

## Request / Execution Pattern

Requests are constructed inside the `*Client` methods:

1. Create a `RestRequest` via `IHttpClient.GetRequest(relativeUrl, Method.<Verb>)`.
2. Set path params with `AddUrlSegment`.
3. Set query params with `AddSafeQueryParameter` (only adds when the value is non-null).
4. Set JSON bodies with `AddJsonBody`.
5. Execute with `ExecuteAsync<T>` (JSON → model deserialization) or the overload that accepts a custom deserializer (for bytes/raw responses).

Helper extensions live in `mailinator-csharp-client/Helpers/RequestExtensions.cs`.

## Base URL and Path Composition

The SDK base URI is defined in `mailinator-csharp-client/MailinatorClient.cs`:

- `https://api.mailinator.com/api/v2`

Each API client is initialized with an `endpointUrl` prefix (for example `"domains"`). Methods then append the rest of the path. Example:

- `endpointUrl = "domains"`
- method relative URL: `"/{domain}/inboxes/{inbox}"`
- effective request path: `/api/v2/domains/{domain}/inboxes/{inbox}`

**Rule:** always target `/api/v2/...` paths; do not introduce `/v2/...`.

## Authentication vs Webhooks

- Most endpoints require an API token: construct `new MailinatorClient(apiToken)` so `HttpClient` sets the `Authorization` header.
- Webhook injection endpoints intentionally do **not** use the API token. `new MailinatorClient()` (parameterless) initializes `WebhooksClient` using an unauthenticated `HttpClient`, and webhook auth is passed via the `whtoken` query parameter.

---

## Gap Analysis Workflow (Spec vs SDK)

Use this workflow whenever you want to audit the SDK against the OpenAPI spec, identify missing/extra coverage, and bring them into alignment.

### Step 1 — Fetch the OpenAPI Specification

Retrieve the raw YAML from:

```
https://raw.githubusercontent.com/manybrain/mailinatordocs/main/openapi/mailinator-api.yaml
```

Extract every `paths` entry. For each operation, record:

- HTTP method (`get`, `post`, `put`, `delete`, ...)
- Path template (e.g. `/api/v2/domains/{domain}/inboxes/{inbox}`)
- `operationId`
- Tag (maps to an `ApiClients/<Tag>/` directory)
- Path/query parameters (and which are optional)
- Request body schema (if present)
- Response schema(s)

### Step 2 — Catalog the SDK

For each `*Client.cs` under `mailinator-csharp-client/Clients/ApiClients/`:

1. Enumerate public `*Async` methods.
2. Capture the RestSharp HTTP verb used (`Method.Get`, `Method.Post`, ...).
3. Capture the relative URL string passed to `GetRequest(...)`.
4. Capture all `AddUrlSegment(...)` and `AddSafeQueryParameter(...)` usage.
5. Note the request/response model types used.

Also capture the `endpointUrl` prefixes assigned in `mailinator-csharp-client/MailinatorClient.cs`, since those affect the effective path.

### Step 3 — Identify Gaps

Produce a gap report with four sections:

#### A. In the spec but missing from the SDK
List every spec operation that has no corresponding SDK method (match by HTTP verb + effective path template).

#### B. In the SDK but not in the spec
List every SDK method whose HTTP verb + effective path template does not exist in the spec. Flag for clarification (it may be legacy or undocumented).

#### C. URL/path mismatches
Ensure the SDK’s effective path templates exactly match the spec’s paths (including `/api/v2/` and segment names).

#### D. Query parameter gaps
For each SDK method, compare the query parameters it sends against the spec’s declared parameters. List missing parameters and any parameter name mismatches.

### Step 4 — Build a Plan

Before making changes, write a plan that includes:

1. **New client methods to add** (grouped by OpenAPI tag / `ApiClients/<Tag>/`).
2. **Path fixes** (file + method + exact change).
3. **Query parameter additions** (file + method + parameter names).
4. **Model updates** (new/changed request/response/entities).
5. **Behavior changes** (pagination, defaults, delete flags, cursor handling, etc.).

Present the plan for approval before implementing.

### Step 5 — Implement

Follow existing patterns in the target `*Client.cs`:

- Use `httpClient.GetRequest(endpointUrl + "<path>", Method.<Verb>)`.
- Set path params with `AddUrlSegment`.
- Add optional query params with `AddSafeQueryParameter("param_name", value?.ToString())`.
- Add bodies with `AddJsonBody(...)`.
- Return a `Models/**/Responses/*Response` type.

Place new/updated model types in the matching `Models/<Tag>/...` folder and ensure namespaces follow the folder structure.

### Step 6 — Verify

After implementing:

1. Build the solution: `dotnet build`
2. Run tests (if configured): `dotnet test`
3. Manually sanity-check at least one updated method by confirming:
   - the effective path template matches the spec,
   - required path/query params are present,
   - the response model matches the spec’s schema shape.

