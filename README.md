
# Mailinator API Client Library

C# Client Library used to interact with the [Mailinator](https://www.mailinator.com/) API
Please read our [documentation](https://manybrain.github.io/m8rdocs/#mailinator/) for instructions on how to start using the API.

## How to Install

`PM> Install-Package MailinatorApiClient`

## Usage

To start using the API you need to first create an account at [mailinator.com](https://www.mailinator.com/).

Once you have an account you will need an API Token which you can generate in [mailinator.com/v3/#/#team_settings_pane](https://www.mailinator.com/v3/#/#team_settings_pane).

Then you can configure the library with:

```csharp
  using mailinator_csharp_client;

  MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");
```

## Examples

##### Authenticators methods:

- InstantTOTP2FACode / Get Authenticators / Get Authenticators By Id:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

	MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //InstantTOTP2FACode
	InstantTOTP2FACodeRequest instantTOTP2FACodeRequest = new InstantTOTP2FACodeRequest() { TotpSecretKey = "yourAuthSecret" };
	var instantTOTP2FACodeResponse = await mailinatorClient.AuthenticatorsClient.InstantTOTP2FACodeAsync(instantTOTP2FACodeRequest);
	
	//Get Authenticators
	GetAuthenticatorsResponse getAuthenticatorsResponse = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorsAsync();
	
	//Get Authenticators By Id
	GetAuthenticatorsByIdRequest getAuthenticatorsByIdRequest = new GetAuthenticatorsByIdRequest() { Id = "yourAuthId" };
	GetAuthenticatorsByIdResponse getAuthenticatorsByIdResponse = await mailinatorClient.AuthenticatorsClient.GetAuthenticatorsByIdAsync(getAuthenticatorsByIdRequest);
	
    // ...
  ```

##### Domains methods:

- Get AllDomains / Domain:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

	MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Get All Domains
	GetAllDomainsResponse getAllDomainsResponse = await mailinatorClient.DomainsClient.GetAllDomainsAsync();
	
	//Get Domain
	GetDomainRequest getDomainRequest = new GetDomainRequest() { DomainId = "yourDomainIdHere" };
    GetDomainResponse getDomainResponse = await mailinatorClient.DomainsClient.GetDomainAsync(getDomainRequest);
    // ...
  ```

- Create / Delete Domain:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

	MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Create Domain
    CreateDomainRequest createDomainRequest = new CreateDomainRequest() 
	    { 
		    Name = DateTime.UtcNow.Ticks.ToString(), 
		    Description = "Description", 
		    Enabled = true, 
		    Rules = new System.Collections.Generic.List<mailinator_csharp_client.Models.Rules.Entities.Rule>() 
		 };
	CreateDomainResponse createDomainResponse = await mailinatorClient.DomainsClient.CreateDomainAsync(createDomainRequest );
	
	//Delete Domain
	var deleteDomainRequest = new DeleteDomainRequest() { DomainId = "yourDomainIdHere" };
	DeleteDomainResponse deleteDomainResponse = await mailinatorClient.DomainsClient.DeleteDomainAsync(deleteDomainRequest);
    // ...
  ```

##### Rules methods:

- Create / Delete Rule:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Rules.Entities;
    using mailinator_csharp_client.Models.Rules.Requests;
    using mailinator_csharp_client.Models.Rules.Responses;
    using System.Collections.Generic;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Create Rule
    RuleToCreate ruleToCreate = new RuleToCreate()
            {
                Name = "RuleName",
                Priority = 15,
                Description = "Description",
                Conditions = new List<Condition>()
                {
                    new Condition()
                    {
                        Operation = OperationType.PREFIX,
                        ConditionData = new ConditionData()
                        {
                            Field = "to",
                            Value = "raul"
                        }
                    }
                },
                Enabled = true,
                Match = MatchType.ANY,
                Actions = new List<ActionRule>() { new ActionRule() { Action = ActionType.WEBHOOK, ActionData = new ActionData() { Url = "https://www.google.com" } } }
            };
            CreateRuleRequest createRuleRequest = new CreateRuleRequest() { DomainId = "yourDomainIdHere", Rule = ruleToCreate };
            CreateRuleResponse createRuleResponse = await mailinatorClient.RulesClient.CreateRuleAsync(createRuleRequest);
            
        //Delete Rule
        DeleteRuleRequest deleteRuleRequest = new DeleteRuleRequest() { DomainId = "yourDomainIdHere", RuleId = "yourRuleIdHere" };
        DeleteRuleResponse deleteRuleResponse = await mailinatorClient.RulesClient.DeleteRuleAsync(deleteRuleRequest);
        // ...
  ```

- Enable / Disable Rule:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Rules.Requests;
    using mailinator_csharp_client.Models.Rules.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Enable Rule
    EnableRuleRequest enableRuleRequest = new EnableRuleRequest() { DomainId = "yourDomainIdHere", RuleId = "yourRuleIdHere" };
    EnableRuleResponse enableRuleResponse = await mailinatorClient.RulesClient.EnableRuleAsync(enableRuleRequest);
    
    //Disable Rule
    DisableRuleRequest disableRuleRequest = new DisableRuleRequest() { DomainId = "yourDomainIdHere", RuleId = "yourRuleIdHere" };
    DisableRuleResponse disableRuleResponse = await mailinatorClient.RulesClient.DisableRuleAsync(disableRuleRequest);
  ```

- Get All Rules / Rule:

```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Rules.Requests;
    using mailinator_csharp_client.Models.Rules.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Get All Rules
    var getAllRulesRequest = new GetAllRulesRequest() { DomainId = "yourDomainIdHere" };
    var getAllRulesResponse = await mailinatorClient.RulesClient.GetAllRulesAsync(getAllRulesRequest);
    
    //Get Rule
    var getRuleRequest = new GetRuleRequest() { DomainId = "yourDomainIdHere", RuleId = "yourRuleIdHere" };
    var getRuleResponse = await mailinatorClient.RulesClient.GetRuleAsync(getRuleRequest);
```

##### Messages methods:

- Post Message:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Messages.Entities;
    using mailinator_csharp_client.Models.Messages.Requests;
    using mailinator_csharp_client.Models.Messages.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    MessageToPost messageToPost = new MessageToPost()
            {
                Subject = "Testing message",
                From = "test_email@test.com",
                Text = "Hello World!"
            };
    PostMessageRequest postMessageRequest = new PostMessageRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", Message = messageToPost };
    PostMessageResponse postMessageResponse = await mailinatorClient.MessagesClient.PostNewMessageAsync(postMessageRequest);
    // ...
  ```

- Fetch Inbox / Message / SMS Messages / Attachments / Attachment / Smtp Log / Raw / Latest:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Messages.Entities;
    using mailinator_csharp_client.Models.Messages.Requests;
    using mailinator_csharp_client.Models.Messages.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Fetch Inbox
    FetchInboxRequest fetchInboxRequest = new FetchInboxRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", Skip = 0, Limit = 20, Sort = Sort.asc };
    FetchInboxResponse fetchInboxResponse = await mailinatorClient.MessagesClient.FetchInboxAsync(fetchInboxRequest);
    
    //Fetch Message
    FetchMessageRequest fetchMessageRequest = new FetchMessageRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdHere" };
    FetchMessageResponse fetchMessageResponse = await mailinatorClient.MessagesClient.FetchMessageAsync(fetchMessageRequest);
    
    //Fetch SMS Messages
    FetchSMSMessagesRequest fetchSMSMessagesRequest = new FetchSMSMessagesRequest() { Domain = "yourDomainNameHere", TeamSMSNumber = "yourTeamSMSNumberHere" };
    FetchSMSMessagesResponse fetchSMSMessagesResponse = await mailinatorClient.MessagesClient.FetchSMSMessagesAsync(fetchSMSMessagesRequest);
    
    //Fetch Attachments
    FetchAttachmentsRequest fetchAttachmentsRequest = new FetchAttachmentsRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdWithAttachmentHere" };
    FetchAttachmentsResponse fetchAttachmentsResponse = await mailinatorClient.MessagesClient.FetchAttachmentsAsync(fetchAttachmentsRequest);
    
    //Fetch Attachment
    FetchAttachmentRequest fetchAttachmentRequest = new FetchAttachmentRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdWithAttachmentHere", AttachmentId = "yourAttachmentIdHere" };
    FetchAttachmentResponse fetchAttachmentResponse = await mailinatorClient.MessagesClient.FetchAttachmentAsync(fetchAttachmentRequest);
            
    //Fetch Message Links
    FetchMessageLinksRequest fetchMessageLinksRequest = new FetchMessageLinksRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdWithAttachmentHere" };
    FetchMessageLinksResponse fetchMessageLinksResponse = await mailinatorClient.MessagesClient.FetchMessageLinksAsync(fetchMessageLinksRequest);
	
	//Fetch Message Smtp Log
	FetchMessageSmtpLogRequest fetchMessageSmtpLogRequest = new FetchMessageSmtpLogRequest() { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" };
	FetchMessageSmtpLogResponse fetchMessageSmtpLogResponse= await mailinatorClient.MessagesClient.FetchMessageSmtpLogAsync(fetchMessageSmtpLogRequest);
	
	//Fetch Message Raw
	FetchMessageRawRequest fetchMessageRawRequest = new FetchMessageRawRequest() { Domain = "yourDomainNameHere", MessageId = "yourMessageIdHere" };
	FetchMessageRawResponse fetchMessageRawResponse= await mailinatorClient.MessagesClient.FetchMessageRawAsync(fetchMessageRawRequest);
	
	//Fetch Latest Messages
	FetchLatestMessagesRequest fetchLatestMessagesRequest = new FetchLatestMessagesRequest() { Domain = "yourDomainNameHere" };
	FetchLatestMessagesResponse fetchLatestMessagesResponse = await mailinatorClient.MessagesClient.FetchLatestMessagesAsync(fetchLatestMessagesRequest);
  ```

- Delete Message / AllInboxMessages / AllDomainMessages

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Messages.Requests;
    using mailinator_csharp_client.Models.Messages.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Delete Message
    DeleteMessageRequest deleteMessageRequest = new DeleteMessageRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere", MessageId = "yourMessageIdHere" };
    DeleteMessageResponse deleteMessageResponse = await mailinatorClient.MessagesClient.DeleteMessageAsync(deleteMessageRequest);
    
    //Delete All Inbox Messages
    DeleteAllInboxMessagesRequest deleteAllInboxMessagesRequest = new DeleteAllInboxMessagesRequest() { Domain = "yourDomainNameHere", Inbox = "yourInboxHere" };
    DeleteAllInboxMessagesResponse deleteAllInboxMessagesResponse = await mailinatorClient.MessagesClient.DeleteAllInboxMessagesAsync(deleteAllInboxMessagesRequest);
    
    //Delete All Domain Messages
    DeleteAllDomainMessagesRequest deleteAllDomainMessagesRequest = new DeleteAllDomainMessagesRequest() { Domain = "yourDomainNameHere" };
    DeleteAllDomainMessagesResponse deleteAllDomainMessagesResponse = await mailinatorClient.MessagesClient.DeleteAllDomainMessagesAsync(deleteAllDomainMessagesRequest);
  ```
  
##### Stats methods:

- Get Team / Team Stats:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");

    //Get Team
    GetTeamResponse getTeamResponse = await mailinatorClient.StatsClient.GetTeamAsync();
	
    //Get TeamStats
    GetTeamStatsResponse getTeamStatsResponse = await mailinatorClient.StatsClient.GetTeamStatsAsync();
    // ...
  ```

##### Webhooks methods:

- Public Webhook / Public Inbox Webhook / Public Custom Service Webhook / Public Custom Service Inbox Webhook:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");
    Webhook webhookToAdd = new Webhook { From = "MyMailinatorCSharpTest", Subject = "testing message", Text = "hello world", To = "jack" };

    //Public Webhook
    PublicWebhookRequest publicWebhookRequest = new PublicWebhookRequest() { Webhook = webhookToAdd };
	PublicWebhookResponse publicWebhookResponse = await mailinatorClient.WebhooksClient.PublicWebhookAsync(publicWebhookRequest);
	
    //Public Inbox Webhook
    PublicInboxWebhookRequest publicInboxWebhookRequest = new PublicInboxWebhookRequest() { Inbox = "yourWebhookInbox", Webhook = webhookToAdd };
	PublicWebhookResponse publicInboxWebhookResponse = await mailinatorClient.WebhooksClient.PublicInboxWebhookAsync(publicInboxWebhookRequest);
	
	//Public Custom Service Webhook
    PublicCustomServiceWebhookRequest publicCustomServiceWebhookRequest = new PublicCustomServiceWebhookRequest() { CustomService = "yourWebhookCustomService", Webhook = webhookToAdd };
	PublicCustomServiceWebhookResponse publicCustomServiceWebhookResponse = await mailinatorClient.WebhooksClient.PublicCustomServiceWebhookAsync(publicCustomServiceWebhookRequest );
	
	//Public Custom Service Inbox Webhook
	PublicCustomServiceInboxWebhookRequest publicCustomServiceInboxWebhookRequest = new PublicCustomServiceInboxWebhookRequest() { CustomService = "yourWebhookCustomService", Inbox = "yourWebhookInbox", Webhook = webhookToAdd };
	PublicCustomServiceWebhookResponse publicCustomServiceInboxWebhookResponse = await mailinatorClient.WebhooksClient.PublicCustomServiceInboxWebhookAsync(publicCustomServiceInboxWebhookRequest);
    // ...
  ```
  
- Private Webhook / Private Inbox Webhook / Private Custom Service Webhook / Private Custom Service Inbox Webhook:

  ```csharp
    using mailinator_csharp_client;
    using mailinator_csharp_client.Models.Domains.Requests;
    using mailinator_csharp_client.Models.Domains.Responses;

    MailinatorClient mailinatorClient = new MailinatorClient("yourApiTokenHere");
    Webhook webhookToAdd = new Webhook { From = "MyMailinatorCSharpTest", Subject = "testing message", Text = "hello world", To = "jack" };

    //Private Webhook
    PrivateWebhookRequest privateWebhookRequest = new PrivateWebhookRequest() { WebhookToken = "yourWebhookTokenPrivateDomain", Webhook = webhookToAdd };
	PrivateWebhookResponse privateWebhookResponse = await mailinatorClient.WebhooksClient.PrivateWebhookAsync(privateWebhookRequest);
	
    //Private Inbox Webhook
    PrivateInboxWebhookRequest privateInboxWebhookRequest = new PrivateInboxWebhookRequest() { WebhookToken = "yourWebhookTokenPrivateDomain", Inbox = "yourWebhookInbox", Webhook = webhookToAdd };
	PrivateWebhookResponse privateInboxWebhookResponse = await mailinatorClient.WebhooksClient.PrivateInboxWebhookAsync(privateInboxWebhookRequest);
	
	//Private Custom Service Webhook
    PrivateCustomServiceWebhookRequest privateCustomServiceWebhookRequest = new PrivateCustomServiceWebhookRequest() { WebhookToken = "yourWebhookTokenCustomService", CustomService = "yourWebhookCustomService", Webhook = webhookToAdd };
	PrivateCustomServiceWebhookResponse privateCustomServiceWebhookResponse = await mailinatorClient.WebhooksClient.PrivateCustomServiceWebhookAsync(privateCustomServiceWebhookRequest);
	
	//Private Custom Service Inbox Webhook
	PrivateCustomServiceInboxWebhookRequest privateCustomServiceInboxWebhookRequest = new PrivateCustomServiceInboxWebhookRequest() { WebhookToken = "yourWebhookTokenCustomService", CustomService = "yourWebhookCustomService", Inbox = "yourWebhookInbox", Webhook = webhookToAdd };
	PrivateCustomServiceWebhookResponse privateCustomServiceInboxWebhookResponse = await mailinatorClient.WebhooksClient.PrivateCustomServiceInboxWebhookAsync(privateCustomServiceInboxWebhookRequest);
    // ...
  ```

##### Build with tests

Some of the tests require env variables with valid values. Visit tests source code and review `TastBase.cs` class. The more env variables you set, the more tests are run.

* `MAILINATOR_TEST_API_TOKEN` - API tokens for authentication; basic requirement across many tests;see also https://manybrain.github.io/m8rdocs/#api-authentication
* `MAILINATOR_TEST_DOMAIN_PRIVATE` - private domain; visit https://www.mailinator.com/
* `MAILINATOR_TEST_INBOX` - some already existing inbox within the private domain
* `MAILINATOR_TEST_PHONE_NUMBER` - associated phone number within the private domain; see also https://manybrain.github.io/m8rdocs/#fetch-an-sms-messages
* `MAILINATOR_TEST_MESSAGE_WITH_ATTACHMENT_ID` - existing message id within inbox (see above) within private domain (see above); see also https://manybrain.github.io/m8rdocs/#fetch-message
* `MAILINATOR_TEST_ATTACHMENT_ID` - existing message id within inbox (see above) within private domain (see above); see also https://manybrain.github.io/m8rdocs/#fetch-message
* `MAILINATOR_TEST_DELETE_DOMAIN` - don't use it unless you are 100% sure what you are doing
* `MAILINATOR_TEST_WEBHOOKTOKEN_PRIVATEDOMAIN` - private domain for webhook token
* `MAILINATOR_TEST_WEBHOOKTOKEN_CUSTOMSERVICE` - custom service for webhook token
* `MAILINATOR_TEST_AUTH_SECRET` - authenticator secret
* `MAILINATOR_TEST_AUTH_ID` - authenticator id
* `MAILINATOR_TEST_WEBHOOK_INBOX` - inbox for webhook
* `MAILINATOR_TEST_WEBHOOK_CUSTOMSERVICE` - custom service for webhook