using System.Text.RegularExpressions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

const string DefaultSpecUrl = "https://raw.githubusercontent.com/manybrain/mailinatordocs/main/openapi/mailinator-api.yaml";

var options = Options.Parse(args);
if (options.ShowHelp)
{
    Options.PrintHelp();
    return 0;
}

var spec = await LoadSpecAsync(options);
var specOperations = GetOpenApiOperations(spec.Document).ToList();
var sdkOperations = GetCSharpOperations(options.ClientRoot).ToList();

var specByKey = specOperations.GroupBy(operation => operation.Key).ToDictionary(group => group.Key, group => group.ToList());
var sdkByKey = sdkOperations.GroupBy(operation => operation.Key).ToDictionary(group => group.Key, group => group.ToList());
var specByStructuralKey = specOperations.GroupBy(operation => operation.StructuralKey).ToDictionary(group => group.Key, group => group.ToList());
var sdkByStructuralKey = sdkOperations.GroupBy(operation => operation.StructuralKey).ToDictionary(group => group.Key, group => group.ToList());

var pathParameterMismatches = specOperations
    .Where(specOperation => !sdkByKey.ContainsKey(specOperation.Key))
    .Select(specOperation =>
    {
        sdkByStructuralKey.TryGetValue(specOperation.StructuralKey, out var candidates);
        var sdkOperation = candidates?.FirstOrDefault(candidate => candidate.Method == specOperation.Method);
        return sdkOperation is null || sdkOperation.PathParams.SetEquals(specOperation.PathParams)
            ? null
            : new OperationPair(specOperation, sdkOperation);
    })
    .Where(pair => pair is not null)
    .Cast<OperationPair>()
    .ToList();

var exactMatchKeys = specByKey.Keys.Intersect(sdkByKey.Keys).ToHashSet(StringComparer.Ordinal);
var mismatchedSpecKeys = pathParameterMismatches.Select(pair => pair.SpecOperation.Key).ToHashSet(StringComparer.Ordinal);
var mismatchedSdkKeys = pathParameterMismatches.Select(pair => pair.SdkOperation.Key).ToHashSet(StringComparer.Ordinal);

var missingFromSdk = specOperations
    .Where(operation => !sdkByKey.ContainsKey(operation.Key) && !mismatchedSpecKeys.Contains(operation.Key))
    .ToList();

var sdkAliasOperations = sdkOperations
    .Where(operation => !specByKey.ContainsKey(operation.Key) && !mismatchedSdkKeys.Contains(operation.Key))
    .Where(operation =>
        specByStructuralKey.TryGetValue(operation.StructuralKey, out var specMatches) &&
        specMatches.Any(specOperation => exactMatchKeys.Contains(specOperation.Key)))
    .ToList();

var sdkAliasKeys = sdkAliasOperations.Select(operation => operation.Key).ToHashSet(StringComparer.Ordinal);
var sdkOnly = sdkOperations
    .Where(operation => !specByKey.ContainsKey(operation.Key) && !mismatchedSdkKeys.Contains(operation.Key))
    .Where(operation => !sdkAliasKeys.Contains(operation.Key))
    .ToList();

var missingQueryParams = exactMatchKeys
    .Select(key =>
    {
        var specOperation = specByKey[key][0];
        var sdkOperation = sdkByKey[key][0];
        var missing = specOperation.QueryParams.Except(sdkOperation.QueryParams).OrderBy(param => param).ToList();
        return missing.Count == 0 ? null : new MissingQueryParams(specOperation, sdkOperation, missing);
    })
    .Where(item => item is not null)
    .Cast<MissingQueryParams>()
    .OrderBy(item => item.SpecOperation.Key)
    .ToList();

var lines = new List<string>
{
    options.Format == "markdown" ? "## OpenAPI coverage check" : "OpenAPI coverage check",
    $"Spec source: {spec.Source}",
    $"SDK root: {options.ClientRoot}",
    string.Empty,
    $"Spec operations: {specOperations.Count}",
    $"SDK operations: {sdkOperations.Count}",
    $"Exact matches: {exactMatchKeys.Count}",
    $"Missing from SDK: {missingFromSdk.Count}",
    $"SDK-only: {sdkOnly.Count}",
    $"SDK aliases/convenience wrappers: {sdkAliasOperations.Count}",
    $"Path parameter-name mismatches: {pathParameterMismatches.Count}",
    $"Operations with missing query params: {missingQueryParams.Count}",
    string.Empty
};

RenderList(lines, "Missing from SDK:", missingFromSdk);
RenderList(lines, "SDK-only:", sdkOnly);
RenderList(lines, "SDK aliases/convenience wrappers:", sdkAliasOperations);

if (pathParameterMismatches.Count > 0)
{
    lines.Add("Path parameter-name mismatches:");
    foreach (var pair in pathParameterMismatches.OrderBy(pair => pair.SpecOperation.Key))
    {
        lines.Add($"  - {pair.SpecOperation.Key}");
        lines.Add($"    spec path: {pair.SpecOperation.Path}");
        lines.Add($"    sdk path:  {pair.SdkOperation.Path}");
    }

    lines.Add(string.Empty);
}

if (missingQueryParams.Count > 0)
{
    lines.Add("Missing query params:");
    foreach (var item in missingQueryParams)
    {
        lines.Add($"  - {item.SpecOperation.Key}: {string.Join(", ", item.Missing)}");
    }

    lines.Add(string.Empty);
}

Console.WriteLine(string.Join(Environment.NewLine, lines).TrimEnd());

var driftDetected =
    missingFromSdk.Count > 0 ||
    sdkOnly.Count > 0 ||
    pathParameterMismatches.Count > 0 ||
    missingQueryParams.Count > 0;

return driftDetected && options.FailOnDrift ? 1 : 0;

static async Task<LoadedSpec> LoadSpecAsync(Options options)
{
    if (!string.IsNullOrWhiteSpace(options.SpecPath) && !string.IsNullOrWhiteSpace(options.SpecUrl))
    {
        throw new InvalidOperationException("Use either --spec or --spec-url, not both.");
    }

    var source = options.SpecPath ?? options.SpecUrl ?? DefaultSpecUrl;
    using var httpClient = new HttpClient();
    await using var stream = !string.IsNullOrWhiteSpace(options.SpecPath)
        ? File.OpenRead(options.SpecPath)
        : await httpClient.GetStreamAsync(source);

    var document = new OpenApiStreamReader().Read(stream, out var diagnostic);
    if (diagnostic.Errors.Count > 0)
    {
        var errors = string.Join(Environment.NewLine, diagnostic.Errors.Select(error => $"  - {error.Message}"));
        throw new InvalidOperationException($"Unable to parse OpenAPI document:{Environment.NewLine}{errors}");
    }

    return new LoadedSpec(document, source);
}

static IEnumerable<ApiOperation> GetOpenApiOperations(OpenApiDocument document)
{
    foreach (var path in document.Paths)
    {
        foreach (var operation in path.Value.Operations)
        {
            var pathParameters = path.Value.Parameters ?? Enumerable.Empty<OpenApiParameter>();
            var operationParameters = operation.Value.Parameters ?? Enumerable.Empty<OpenApiParameter>();
            var parameters = pathParameters
                .Concat(operationParameters)
                .Select(parameter => ResolveParameter(document, parameter));

            yield return new ApiOperation(
                Method: operation.Key.ToString().ToUpperInvariant(),
                Path: NormalizePath(path.Key),
                OperationId: operation.Value.OperationId,
                QueryParams: parameters
                    .Where(parameter => parameter.In == ParameterLocation.Query)
                    .Select(parameter => parameter.Name)
                    .ToHashSet(StringComparer.Ordinal),
                PathParams: parameters
                    .Where(parameter => parameter.In == ParameterLocation.Path)
                    .Select(parameter => parameter.Name)
                    .ToHashSet(StringComparer.Ordinal),
                Source: "OpenAPI");
        }
    }
}

static OpenApiParameter ResolveParameter(OpenApiDocument document, OpenApiParameter parameter)
{
    if (parameter.Reference?.Id is { Length: > 0 } referenceId &&
        document.Components?.Parameters.TryGetValue(referenceId, out var referencedParameter) == true)
    {
        return referencedParameter;
    }

    return parameter;
}

static IEnumerable<ApiOperation> GetCSharpOperations(string clientRoot)
{
    var endpointMap = GetClassEndpointMap(clientRoot);
    var apiClientsRoot = Path.Combine(clientRoot, "Clients", "ApiClients");
    if (!Directory.Exists(apiClientsRoot))
    {
        throw new DirectoryNotFoundException($"Unable to find API clients directory: {apiClientsRoot}");
    }

    foreach (var file in Directory.GetFiles(apiClientsRoot, "*Client.cs", SearchOption.AllDirectories))
    {
        var source = File.ReadAllText(file);
        var className = Path.GetFileNameWithoutExtension(file);
        var baseEndpoint = endpointMap.TryGetValue(className, out var endpoint) ? endpoint : string.Empty;

        foreach (var method in GetMethodBlocks(source))
        {
            var requestMatch = Regex.Match(
                method.Block,
                @"httpClient\.GetRequest\s*\(\s*endpointUrl\s*\+\s*""([^""]*)""\s*,\s*Method\.(\w+)",
                RegexOptions.Singleline);

            if (!requestMatch.Success)
            {
                continue;
            }

            var path = CombineEndpointPath(baseEndpoint, requestMatch.Groups[1].Value);
            var queryParams = Regex.Matches(method.Block, @"AddSafeQueryParameter\s*\(\s*""([^""]+)""")
                .Select(match => match.Groups[1].Value)
                .ToHashSet(StringComparer.Ordinal);
            var pathParams = Regex.Matches(method.Block, @"AddUrlSegment\s*\(\s*""([^""]+)""")
                .Select(match => match.Groups[1].Value)
                .ToHashSet(StringComparer.Ordinal);

            yield return new ApiOperation(
                Method: requestMatch.Groups[2].Value.ToUpperInvariant(),
                Path: path,
                OperationId: method.Name,
                QueryParams: queryParams,
                PathParams: pathParams,
                Source: Path.GetRelativePath(Directory.GetCurrentDirectory(), file));
        }
    }
}

static Dictionary<string, string> GetClassEndpointMap(string clientRoot)
{
    var mailinatorClient = Path.Combine(clientRoot, "MailinatorClient.cs");
    if (!File.Exists(mailinatorClient))
    {
        return new Dictionary<string, string>(StringComparer.Ordinal);
    }

    var source = File.ReadAllText(mailinatorClient);
    return Regex.Matches(source, @"(\w+Client)\s*=\s*new\s+\w+Client\s*\([^,]+,\s*""([^""]*)""\s*\)")
        .ToDictionary(
            match => match.Groups[1].Value,
            match => match.Groups[2].Value,
            StringComparer.Ordinal);
}

static IEnumerable<MethodBlock> GetMethodBlocks(string source)
{
    var matches = Regex.Matches(
            source,
            @"public\s+async\s+Task<[^>]+>\s+(\w+)\s*\([^)]*\)\s*\{",
            RegexOptions.Singleline)
        .Cast<Match>()
        .ToList();

    for (var index = 0; index < matches.Count; index++)
    {
        var match = matches[index];
        var nextStart = index + 1 < matches.Count ? matches[index + 1].Index : source.Length;
        yield return new MethodBlock(match.Groups[1].Value, source[match.Index..nextStart]);
    }
}

static string CombineEndpointPath(string baseEndpoint, string relativePath)
{
    var path = string.IsNullOrWhiteSpace(baseEndpoint)
        ? relativePath
        : relativePath.StartsWith("/", StringComparison.Ordinal)
            ? $"{baseEndpoint}{relativePath}"
            : $"{baseEndpoint}/{relativePath}";

    return NormalizePath($"/api/v2/{path}");
}

static string NormalizePath(string path)
{
    var normalized = Regex.Replace(path, "/+", "/");
    if (!normalized.StartsWith("/", StringComparison.Ordinal))
    {
        normalized = $"/{normalized}";
    }

    return normalized.Length > 1 ? normalized.TrimEnd('/') : normalized;
}

static void RenderList(List<string> lines, string title, List<ApiOperation> operations)
{
    if (operations.Count == 0)
    {
        return;
    }

    lines.Add(title);
    foreach (var operation in operations.OrderBy(operation => operation.Key))
    {
        lines.Add($"  - {OperationLabel(operation)}");
    }

    lines.Add(string.Empty);
}

static string OperationLabel(ApiOperation operation)
{
    return string.IsNullOrWhiteSpace(operation.OperationId)
        ? operation.Key
        : $"{operation.Key} ({operation.OperationId})";
}

internal sealed record ApiOperation(
    string Method,
    string Path,
    string? OperationId,
    HashSet<string> QueryParams,
    HashSet<string> PathParams,
    string Source)
{
    public string Key => $"{Method} {Path}";
    public string StructuralKey => Regex.Replace(Key, @"\{[^}]+\}", "{}");
}

internal sealed record LoadedSpec(OpenApiDocument Document, string Source);

internal sealed record MethodBlock(string Name, string Block);

internal sealed record OperationPair(ApiOperation SpecOperation, ApiOperation SdkOperation);

internal sealed record MissingQueryParams(ApiOperation SpecOperation, ApiOperation SdkOperation, List<string> Missing);

internal sealed class Options
{
    public string? SpecPath { get; private set; }
    public string? SpecUrl { get; private set; }
    public string ClientRoot { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "mailinator-csharp-client");
    public bool FailOnDrift { get; private set; }
    public string Format { get; private set; } = "text";
    public bool ShowHelp { get; private set; }

    public static Options Parse(string[] args)
    {
        var options = new Options();

        for (var index = 0; index < args.Length; index++)
        {
            var arg = args[index];
            switch (arg)
            {
                case "--spec":
                    options.SpecPath = ReadValue(args, ref index, arg);
                    break;
                case "--spec-url":
                    options.SpecUrl = ReadValue(args, ref index, arg);
                    break;
                case "--client-root":
                    options.ClientRoot = ReadValue(args, ref index, arg);
                    break;
                case "--fail-on-drift":
                    options.FailOnDrift = true;
                    break;
                case "--format":
                    options.Format = ReadValue(args, ref index, arg);
                    if (options.Format is not ("text" or "markdown"))
                    {
                        throw new ArgumentException("--format must be text or markdown.");
                    }
                    break;
                case "-h":
                case "--help":
                    options.ShowHelp = true;
                    break;
                default:
                    throw new ArgumentException($"Unknown argument: {arg}");
            }
        }

        return options;
    }

    public static void PrintHelp()
    {
        Console.WriteLine(
            """
            Usage: dotnet run --project eng/OpenApiCoverageCheck -- [options]

            Options:
              --spec PATH          OpenAPI YAML file to compare against.
              --spec-url URL       OpenAPI YAML URL to compare against.
              --client-root PATH   C# client project root. Defaults to ./mailinator-csharp-client.
              --fail-on-drift      Exit non-zero when spec and SDK differ.
              --format FORMAT      Output format: text or markdown.
              -h, --help           Show help.
            """);
    }

    private static string ReadValue(string[] args, ref int index, string option)
    {
        if (index + 1 >= args.Length)
        {
            throw new ArgumentException($"{option} requires a value.");
        }

        index++;
        return args[index];
    }
}
