# Engineering Tools

## OpenAPI Coverage Check

This development-only tool uses `Microsoft.OpenApi.YamlReader` and `Microsoft.OpenApi` 3.10.2 to parse the specification. These packages are not dependencies of the published SDK. The reader brings in SharpYaml 2.1.5; do not override it with SharpYaml 3.x without reader compatibility support. Testing with 3.14.0 fails with `MissingMethodException` for the `SharpYaml.Parser<T>` constructor.

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
