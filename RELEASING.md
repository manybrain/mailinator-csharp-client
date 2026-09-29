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
3. If tag rules protect names matching `v*`, allow this release workflow to create those tags with its `contents: write` token.

The workflow needs to be present on the repository's default branch before its trusted publishing policy can be used for a release.

## Automated release

Prepare and merge the release changes before starting the workflow:

1. Set `Version`, `AssemblyVersion`, and `FileVersion` in `mailinator-csharp-client/mailinator-csharp-client.csproj`.
2. Add a dated `## [x.y.z] - YYYY-MM-DD` entry to `CHANGELOG.md`.
3. Confirm CI passes on `master`.
4. In GitHub Actions, run the **Release** workflow on `master`.

The workflow verifies the project version and changelog; restores locked dependencies; builds the solution; runs only the offline unit tests; and creates the package. It checks that `master` has not advanced and the version tag is unused, then creates and pushes an annotated `vX.Y.Z` tag before publishing to NuGet.org. Finally, it creates a GitHub Release with the package attached. A failed publish leaves the tag in place for diagnosis; do not rerun the workflow with the same version.

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
git tag -a vX.Y.Z -m "MailinatorApiClient X.Y.Z"
git push origin vX.Y.Z
$env:NUGET_API_KEY = Read-Host 'NuGet API key' -MaskInput
dotnet nuget push artifacts/MailinatorApiClient.x.y.z.nupkg --source https://api.nuget.org/v3/index.json
Remove-Item Env:NUGET_API_KEY
```

Create the tag on the exact commit used to build the package. After publishing, create the corresponding GitHub Release from that tag and attach the same `.nupkg` file.

The live integration-test project is deliberately excluded from both release paths. It requires a configured Mailinator account and includes tests that mutate remote resources; see `TESTING.md` before running it.
