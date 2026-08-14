
# Mailinator API Client Library

C# Client Library used to interact with the [Mailinator](https://www.mailinator.com/) API
Please read our [documentation](https://manybrain.github.io/m8rdocs/#mailinator/) for instructions on how to start using the API.

## How to Install

`PM> Install-Package MailinatorApiClient`

## Usage

To start using the API you need to first create an account at [mailinator.com](https://www.mailinator.com/).

Once you have an account you will need an API Token which you can generate in [mailinator.com/v3/#/#team_settings_pane](https://www.mailinator.com/v3/#/#team_settings_pane).

Usage examples live in [EXAMPLES.md](https://github.com/manybrain/mailinator-csharp-client/blob/master/EXAMPLES.md).

##### Build with tests

The tests are live integration tests. Configure them in a repository-root `.env` file (which is ignored by Git); process environment variables take precedence. If `MAILINATOR_TEST_API_TOKEN` is missing, the entire integration suite is skipped. Copy `.env.example` as a starting point.

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
