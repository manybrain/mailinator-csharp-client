# Examples

This file is a living collection of copy/pasteable examples for the Mailinator C# client.

## Setup

Install via NuGet:

```
PM> Install-Package MailinatorApiClient
```

Create a client using an API token:

```csharp
using mailinator_csharp_client;

var client = new MailinatorClient("yourApiTokenHere");
```

## Quickstart

Fetch message summaries for an inbox:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Messages.Entities;

var client = new MailinatorClient("yourApiTokenHere");

var request = new FetchInboxRequest
{
    Domain = "your_private_domain.com",
    Inbox = "your_inbox",
    Skip = 0,
    Limit = 20,
    Sort = Sort.desc
};

var response = await client.MessagesClient.FetchInboxAsync(request);
```

Use `Sort.asc` for ascending order or `Sort.desc` for descending order (the default). In version 2.0.0, both `FetchInboxAsync` and `ListDomainMessagesAsync` serialize these as the API query values `ascending` and `descending`.

For dependency migration steps, see [upgrading from 1.0.7 to 2.0.0](README.md#upgrading-from-107-to-200).

## Authenticators

Instant TOTP code + get a stored authenticator:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Authenticators.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var totp = await client.AuthenticatorsClient.InstantTOTP2FACodeAsync(
    new InstantTOTP2FACodeRequest { TotpSecretKey = "yourAuthSecret" });

var byId = await client.AuthenticatorsClient.GetAuthenticatorsByIdAsync(
    new GetAuthenticatorsByIdRequest { Id = "yourAuthId" });
```

## Domains

List domains + fetch a domain:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Domains.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var all = await client.DomainsClient.GetAllDomainsAsync();

var domain = await client.DomainsClient.GetDomainAsync(
    new GetDomainRequest { DomainId = "yourDomainIdHere" });
```

## Messages

### List domain messages

List messages across all inboxes in a domain:

```csharp
using mailinator_csharp_client.Models.Messages.Requests;

// Uses the authenticated client created in Setup.
var request = new ListDomainMessagesRequest
{
    Domain = "your_private_domain.com",
    Limit = 20
};

var response = await client.MessagesClient.ListDomainMessagesAsync(request);
var messages = response.Messages;

// Fetch the next page only when the API supplies a cursor.
if (!string.IsNullOrEmpty(response.Cursor))
{
    request.Cursor = response.Cursor;
    var nextPage = await client.MessagesClient.ListDomainMessagesAsync(request);
}
```

`Inbox` is an optional query filter: omit it or set it to `"*"` for all inboxes, or supply an inbox name/prefix such as `"orders*"`. The request also supports `Skip`, `Limit`, `Sort`, `DecodeSubject`, `Cursor`, `Full`, `Wait`, and `Delete`. Set `Full = true` to request full message content. `Delete` schedules deletion after retrieval (for example, `"30s"`); it is omitted by default.

The result is a `FetchInboxResponse`, sharing the inbox-listing response model and pagination cursor.

### Get message summary

```csharp
using mailinator_csharp_client.Models.Messages.Requests;

// Uses the authenticated client created in Setup.
var response = await client.MessagesClient.GetMessageSummaryAsync(
    new GetMessageSummaryRequest
    {
        Domain = "your_private_domain.com",
        MessageId = "your-message-id"
    });

var summary = response.Summary;
```

`Summary` reuses the `Message` model and contains the subject, domain, sender (`From`), message ID, recipient (`To`), and timestamp (`Time`). This endpoint does not return body or attachment content.

### Get message headers

```csharp
using mailinator_csharp_client.Models.Messages.Requests;

// Uses the authenticated client created in Setup.
var response = await client.MessagesClient.GetMessageHeadersAsync(
    new GetMessageHeadersRequest
    {
        Domain = "your_private_domain.com",
        MessageId = "your-message-id"
    });

var headers = response.Headers;
```

Use a message ID returned by inbox or domain listing. `Headers` is a `Dictionary<string, object>` that preserves custom header names. Values can be strings or JSON arrays (for example, `received`), matching the existing full-message header model.

### Post a message

Post (inject) a message:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var message = new MessageToPost
{
    Subject = "Testing message",
    From = "test_email@test.com",
    Text = "Hello World!"
};

var response = await client.MessagesClient.PostMessageAsync(
    new PostMessageRequest { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", Message = message });
```

Fetch inbox summaries + fetch message by id:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Messages.Entities;

var client = new MailinatorClient("yourApiTokenHere");

var inbox = await client.MessagesClient.FetchInboxAsync(
    new FetchInboxRequest { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", Skip = 0, Limit = 20, Sort = Sort.desc });

var message = await client.MessagesClient.FetchMessageAsync(
    new FetchMessageRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });
```

Fetch attachments + download a single attachment:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var attachments = await client.MessagesClient.FetchMessageAttachmentsAsync(
    new FetchMessageAttachmentsRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });

var attachment = await client.MessagesClient.FetchMessageAttachmentAsync(
    new FetchMessageAttachmentRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere", AttachmentId = "yourAttachmentIdHere" });
```

Links, SMTP log, and raw content:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var links = await client.MessagesClient.FetchMessageLinksAsync(
    new FetchMessageLinksRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });

var linksFull = await client.MessagesClient.FetchMessageLinksFullAsync(
    new FetchMessageLinksFullRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });

var smtp = await client.MessagesClient.FetchMessageSmtpLogAsync(
    new FetchMessageSmtpLogRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });

var raw = await client.MessagesClient.FetchMessageRawAsync(
    new FetchMessageRawRequest { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" });
```

Deletes:

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Messages.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var deleted = await client.MessagesClient.DeleteMessageAsync(
    new DeleteMessageRequest { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdHere" });

var deletedInbox = await client.MessagesClient.DeleteAllInboxMessagesAsync(
    new DeleteAllInboxMessagesRequest { Domain = "yourDomainNameHere", Inbox = "yourInboxHere" });

var deletedDomain = await client.MessagesClient.DeleteAllDomainMessagesAsync(
    new DeleteAllDomainMessagesRequest { Domain = "yourDomainNameHere" });
```

## Stats

Team summary:

```csharp
using mailinator_csharp_client;

var client = new MailinatorClient("yourApiTokenHere");

var team = await client.StatsClient.GetTeamAsync();
var stats = await client.StatsClient.GetTeamStatsAsync();
var info = await client.StatsClient.GetTeamInfoAsync();
```

## Webhooks

Inject via webhook endpoints (uses `whtoken`):

```csharp
using mailinator_csharp_client;
using mailinator_csharp_client.Models.Webhooks.Entities;
using mailinator_csharp_client.Models.Webhooks.Requests;

var client = new MailinatorClient("yourApiTokenHere");

var webhook = new Webhook
{
    From = "MyMailinatorCSharpTest",
    Subject = "testing message",
    Text = "hello world",
    To = "jack"
};

var privateWebhook = await client.WebhooksClient.PrivateWebhookAsync(
    new PrivateWebhookRequest { WebhookToken = "yourWebhookTokenPrivateDomain", Webhook = webhook });

var privateInboxWebhook = await client.WebhooksClient.PrivateInboxWebhookAsync(
    new PrivateInboxWebhookRequest { WebhookToken = "yourWebhookTokenPrivateDomain", Inbox = "yourWebhookInbox", Webhook = webhook });

var customServiceWebhook = await client.WebhooksClient.PrivateCustomServiceWebhookAsync(
    new PrivateCustomServiceWebhookRequest { WebhookToken = "yourWebhookTokenCustomService", CustomService = "yourWebhookCustomService", Webhook = webhook });

var customServiceInboxWebhook = await client.WebhooksClient.PrivateCustomServiceInboxWebhookAsync(
    new PrivateCustomServiceInboxWebhookRequest { WebhookToken = "yourWebhookTokenCustomService", CustomService = "yourWebhookCustomService", Inbox = "yourWebhookInbox", Webhook = webhook });
```

## Troubleshooting

- Ensure you’re using an API token from your Mailinator team settings.
- For webhook injection, use webhook tokens (`whtoken`) instead of your API token.

## Get message content

Use a message ID returned by inbox or domain listing:

```csharp
var extracted = await client.MessagesClient.GetMessageTextAsync(
    new GetMessageTextRequest { Domain = "your-private-domain.com", MessageId = "your-message-id" });
var plain = await client.MessagesClient.GetMessageTextPlainAsync(
    new GetMessageTextPlainRequest { Domain = "your-private-domain.com", MessageId = "your-message-id" });
var html = await client.MessagesClient.GetMessageTextHtmlAsync(
    new GetMessageTextHtmlRequest { Domain = "your-private-domain.com", MessageId = "your-message-id" });

string extractedText = extracted.Text;
string plainText = plain.TextPlain;
string htmlBody = html.TextHtml;
```

These endpoints return JSON wrappers with `text`, `text/plain`, and `text/html` fields respectively. The SDK preserves their content, including HTML markup and any quoted-printable artifacts such as `=C2=A0` in extracted text. Empty strings are preserved. These operations do not delete the message.

## Domain and inbox webhooks

These endpoints authenticate with webhook tokens; no API token is needed. Request types are in `mailinator_csharp_client.Models.Webhooks.Requests` and `WebhookMessage` is in `mailinator_csharp_client.Models.Webhooks.Entities`.

```csharp
var client = new MailinatorClient();
var webhookToken = Environment.GetEnvironmentVariable("MAILINATOR_WEBHOOK_TOKEN");
var payload = new WebhookMessage
{
    To = "orders",
    From = "sender@example.com",
    Subject = "Order notification",
    Text = "Order received",
    Html = "<p>Order received</p>"
};

var domainResult = await client.WebhooksClient.PostWebhookMessageAsync(
    new PostWebhookMessageRequest
    {
        Domain = "your-private-domain.com",
        WebhookToken = webhookToken,
        Webhook = payload
    });

var inboxResult = await client.WebhooksClient.PostWebhookInboxMessageAsync(
    new PostWebhookInboxMessageRequest
    {
        Domain = "your-private-domain.com",
        Inbox = "orders",
        WebhookToken = webhookToken,
        Webhook = payload
    });
```

Both methods also accept the webhook token in `Domain`; omit `WebhookToken` for that form. The payload's `To` field is required by the specification. `Headers` accepts a dictionary of string values, and `AdditionalProperties` accepts custom JSON fields. Both responses expose `Status` and `Id`. Existing private/custom-service webhook methods remain available.
