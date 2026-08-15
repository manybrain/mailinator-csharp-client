# Repository instructions

## Project layout

- `mailinator-csharp-client/` contains the SDK. It targets `net471` and `netstandard2.0`; do not change the supported frameworks without an explicit compatibility decision.
- API methods live in `Clients/ApiClients/<Area>/`, and their request, response, and entity types live in the matching `Models/<Area>/` tree.
- `mailinator-csharp-client-tests/` is a legacy .NET Framework 4.7.2 MSTest project whose tests call the live Mailinator API.
- `eng/OpenApiCoverageCheck/` is a .NET 8 tool for comparing the SDK request surface with the Mailinator OpenAPI specification. See `eng/README.md` for usage.

## Making SDK changes

- Treat the [Mailinator OpenAPI specification](https://github.com/manybrain/mailinatordocs/blob/main/openapi/mailinator-api.yaml) as the source of truth for documented endpoints.
- Preserve the `/api/v2` base path defined in `MailinatorClient.cs`. Endpoint clients receive a relative prefix and append their operation paths.
- Follow the existing RestSharp request pattern: create requests with `GetRequest`, use `AddUrlSegment` for path values, `AddSafeQueryParameter` for optional query values, and `AddJsonBody` for JSON bodies.
- Keep public operations asynchronous and named with the `Async` suffix. Put new models in the matching area and layer (`Requests`, `Responses`, or `Entities`) using the existing namespaces.
- Do not remove or silently change obsolete public members as part of unrelated work; that is a breaking API change.
- When the public API changes, update relevant examples and release documentation (`EXAMPLES.md`, `README.md`, and `CHANGELOG.md`) in the same change.

## Verification

- Build the solution with `dotnet build mailinator-csharp-client.sln` when the installed SDK supports all target frameworks.
- For endpoint or parameter changes, run the coverage tool described in `eng/README.md`. Prefer a checked-out spec with `--spec`; omitting it fetches the current spec from GitHub.
- Treat the MSTest suite as integration testing, not as an offline unit suite. It requires a deliberately configured Mailinator account and can create or delete domains, rules, and messages. Do not run it against live credentials—or run deletion tests—without explicit authorization.
- If the local environment cannot build the legacy targets or lacks the .NET SDK, report that limitation instead of claiming verification.
