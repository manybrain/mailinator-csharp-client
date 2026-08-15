# Mailinator C# SDK reference

This file lists the public asynchronous operations exposed by the SDK. Request and response types use the matching `mailinator_csharp_client.Models.<Area>.Requests` and `mailinator_csharp_client.Models.<Area>.Responses` namespaces.

For endpoint behavior and schemas, consult the [Mailinator API reference](https://www.mailinator.com/documentation/docs/api/index.html). For complete code samples, see [EXAMPLES.md](EXAMPLES.md).

## Client construction

```csharp
new MailinatorClient(apiToken); // Authenticated API clients
new MailinatorClient();         // WebhooksClient only
```

An authenticated client exposes `MessagesClient`, `DomainsClient`, `AuthenticatorsClient`, `StatsClient`, and `RulesClient`. Both constructors expose `WebhooksClient`.

## MessagesClient

| Operation | Request | Response |
| --- | --- | --- |
| `FetchInboxAsync` | `FetchInboxRequest` | `FetchInboxResponse` |
| `FetchInboxMessageAsync` | `FetchInboxMessageRequest` | `FetchInboxMessageResponse` |
| `FetchMessageAsync` | `FetchMessageRequest` | `FetchMessageResponse` |
| `FetchSMSMessagesAsync` | `FetchSMSMessagesRequest` | `FetchSMSMessagesResponse` |
| `FetchInboxMessageAttachmentsAsync` | `FetchInboxMessageAttachmentsRequest` | `FetchInboxMessageAttachmentsResponse` |
| `FetchMessageAttachmentsAsync` | `FetchMessageAttachmentsRequest` | `FetchMessageAttachmentsResponse` |
| `FetchInboxMessageAttachmentAsync` | `FetchInboxMessageAttachmentRequest` | `FetchInboxMessageAttachmentResponse` |
| `FetchMessageAttachmentAsync` | `FetchMessageAttachmentRequest` | `FetchMessageAttachmentResponse` |
| `FetchMessageLinksAsync` | `FetchMessageLinksRequest` | `FetchMessageLinksResponse` |
| `FetchInboxMessageLinksAsync` | `FetchInboxMessageLinksRequest` | `FetchInboxMessageLinksResponse` |
| `FetchMessageLinksFullAsync` | `FetchMessageLinksFullRequest` | `FetchMessageLinksFullResponse` |
| `FetchMessageSmtpLogAsync` | `FetchMessageSmtpLogRequest` | `FetchMessageSmtpLogResponse` |
| `FetchInboxMessageSmtpLogAsync` | `FetchInboxMessageSmtpLogRequest` | `FetchInboxMessageSmtpLogResponse` |
| `FetchMessageRawAsync` | `FetchMessageRawRequest` | `FetchMessageRawResponse` |
| `FetchInboxMessageRawAsync` | `FetchInboxMessageRawRequest` | `FetchInboxMessageRawResponse` |
| `PostMessageAsync` | `PostMessageRequest` | `PostMessageResponse` |
| `DeleteMessageAsync` | `DeleteMessageRequest` | `DeleteMessageResponse` |
| `DeleteAllInboxMessagesAsync` | `DeleteAllInboxMessagesRequest` | `DeleteAllInboxMessagesResponse` |
| `DeleteAllDomainMessagesAsync` | `DeleteAllDomainMessagesRequest` | `DeleteAllDomainMessagesResponse` |
| `FetchLatestMessagesAsync` | `FetchLatestMessagesRequest` | `FetchLatestMessagesResponse` |
| `FetchLatestInboxMessagesAsync` | `FetchLatestInboxMessagesRequest` | `FetchLatestInboxMessagesResponse` |

Attachment download responses expose the returned bytes, content, and content type. Raw-message responses expose the raw message data.

## DomainsClient

| Operation | Request | Response |
| --- | --- | --- |
| `GetAllDomainsAsync` | None | `GetAllDomainsResponse` |
| `GetDomainAsync` | `GetDomainRequest` | `GetDomainResponse` |
| `CreateDomainAsync` | `CreateDomainRequest` | `CreateDomainResponse` |
| `DeleteDomainAsync` | `DeleteDomainRequest` | `DeleteDomainResponse` |

## AuthenticatorsClient

| Operation | Request | Response |
| --- | --- | --- |
| `InstantTOTP2FACodeAsync` | `InstantTOTP2FACodeRequest` | `InstantTOTP2FACodeResponse` |
| `GetAuthenticatorsAsync` | None | `GetAuthenticatorsResponse` |
| `GetAuthenticatorsByIdAsync` | `GetAuthenticatorsByIdRequest` | `GetAuthenticatorsByIdResponse` |
| `GetAuthenticatorAsync` | None | `GetAuthenticatorResponse` |
| `GetAuthenticatorByIdAsync` | `GetAuthenticatorByIdRequest` | `GetAuthenticatorByIdResponse` |

## StatsClient

| Operation | Request | Response |
| --- | --- | --- |
| `GetTeamStatsAsync` | None | `GetTeamStatsResponse` |
| `GetTeamAsync` | None | `GetTeamResponse` |
| `GetTeamInfoAsync` | None | `GetTeamInfoResponse` |

## WebhooksClient

These operations use webhook tokens rather than the API token supplied to `MailinatorClient`.

| Operation | Request | Response |
| --- | --- | --- |
| `PrivateWebhookAsync` | `PrivateWebhookRequest` | `PrivateWebhookResponse` |
| `PrivateInboxWebhookAsync` | `PrivateInboxWebhookRequest` | `PrivateWebhookResponse` |
| `PrivateCustomServiceWebhookAsync` | `PrivateCustomServiceWebhookRequest` | `PrivateCustomServiceWebhookResponse` |
| `PrivateCustomServiceInboxWebhookAsync` | `PrivateCustomServiceInboxWebhookRequest` | `PrivateCustomServiceWebhookResponse` |

## RulesClient

All `RulesClient` operations are deprecated because the corresponding endpoints are not in the current OpenAPI specification.

| Operation | Request | Response |
| --- | --- | --- |
| `CreateRuleAsync` | `CreateRuleRequest` | `CreateRuleResponse` |
| `EnableRuleAsync` | `EnableRuleRequest` | `EnableRuleResponse` |
| `DisableRuleAsync` | `DisableRuleRequest` | `DisableRuleResponse` |
| `GetAllRulesAsync` | `GetAllRulesRequest` | `GetAllRulesResponse` |
| `GetRuleAsync` | `GetRuleRequest` | `GetRuleResponse` |
| `DeleteRuleAsync` | `DeleteRuleRequest` | `DeleteRuleResponse` |

## Deprecated operations

The following public methods are marked `[Obsolete]`. They remain in the current release for backward compatibility.

| Client | Operations | Reason |
| --- | --- | --- |
| `MessagesClient` | `FetchLatestMessagesAsync`, `FetchLatestInboxMessagesAsync` | The wildcard “latest” endpoints are not in the current OpenAPI specification. |
| `DomainsClient` | `CreateDomainAsync`, `DeleteDomainAsync` | Domain create/delete endpoints are not in the current OpenAPI specification. |
| `RulesClient` | All operations | Rules endpoints are not in the current OpenAPI specification. |

See [ROADMAP.md](ROADMAP.md) for known specification gaps and planned alignment work.
