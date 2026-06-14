# Engineering Tools

## OpenAPI Coverage Check

Compare the C# client request surface against the Mailinator OpenAPI specification:

```sh
dotnet run --project eng/OpenApiCoverageCheck -- --spec path/to/mailinator-api.yaml
```

If `--spec` is omitted, the tool fetches the current Mailinator OpenAPI YAML from:

```text
https://raw.githubusercontent.com/manybrain/mailinatordocs/main/openapi/mailinator-api.yaml
```

Use `--fail-on-drift` in CI once the SDK is expected to be in sync with the spec:

```sh
dotnet run --project eng/OpenApiCoverageCheck -- --spec path/to/mailinator-api.yaml --fail-on-drift
```
