# Releasing MailinatorApiClient

NuGet.org is the canonical package registry. Each published package version should also have an annotated Git tag and a GitHub Release using the same version.

## One-time trusted publishing setup

The release workflow uses NuGet.org trusted publishing, so it does not store a long-lived API key in GitHub.

1. In the GitHub repository settings, create an environment named `nuget.org`. Add required reviewers if desired, and add an environment secret named `NUGET_USER` containing the NuGet.org profile name that owns `MailinatorApiClient` (not its email address).
2. On NuGet.org, open the account's **Trusted Publishing** settings and create a GitHub Actions policy with:
   - repository owner: `manybrain`
   - repository: `mailinator-csharp-client`
   - workflow file: `release.yml`
   - environment: `nuget.org`
   - scope: publishing new versions of existing packages
   - package glob: `MailinatorApiClient`
3. Protect tag names matching `v*` in GitHub so only release maintainers can create them.

The workflow needs to be present on the repository's default branch before its trusted publishing policy can be used for a release.

## Automated release

Prepare and merge the release changes before creating the tag:

1. Set `Version`, `AssemblyVersion`, and `FileVersion` in `mailinator-csharp-client/mailinator-csharp-client.csproj`.
2. Add a dated `## [x.y.z] - YYYY-MM-DD` entry to `CHANGELOG.md`.
3. Confirm CI passes on `master`.
4. From an up-to-date, clean `master`, create and push the annotated tag:

   ```sh
   git tag -a v2.0.0 -m "MailinatorApiClient 2.0.0"
   git push origin v2.0.0
   ```

The tag starts `.github/workflows/release.yml`. The workflow verifies that the tag, project version, and changelog agree; restores locked dependencies; builds the solution; runs only the offline unit tests; creates the package; publishes it to NuGet.org; and creates a GitHub Release with the package attached.

Do not move or reuse a published version tag. NuGet package versions are immutable.

## Manual Windows fallback

Use this only when trusted publishing is unavailable. Start from a clean checkout of the exact commit that will be tagged, with the .NET 8 SDK or later and PowerShell 7 installed.

```powershell
dotnet restore mailinator-csharp-client.sln --locked-mode
dotnet build mailinator-csharp-client.sln --configuration Release --no-restore
dotnet test mailinator-csharp-client-unit-tests/mailinator-csharp-client-unit-tests.csproj --configuration Release --no-build --no-restore
dotnet pack mailinator-csharp-client/mailinator-csharp-client.csproj --configuration Release --no-build --no-restore --output artifacts
```

Inspect `artifacts/MailinatorApiClient.x.y.z.nupkg` before publishing. Create a short-lived NuGet.org API key restricted to pushing new versions of `MailinatorApiClient`, then enter it without placing it in shell history:

```powershell
$env:NUGET_API_KEY = Read-Host 'NuGet API key' -MaskInput
dotnet nuget push artifacts/MailinatorApiClient.x.y.z.nupkg --source https://api.nuget.org/v3/index.json
Remove-Item Env:NUGET_API_KEY
```

After a successful manual push, create and push the annotated tag, then create the corresponding GitHub Release from that tag and attach the same `.nupkg` file.

The live integration-test project is deliberately excluded from both release paths. It requires a configured Mailinator account and includes tests that mutate remote resources; see `TESTING.md` before running it.
