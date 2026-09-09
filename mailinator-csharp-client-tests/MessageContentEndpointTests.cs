using System.Threading.Tasks;
using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Messages.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mailinator_csharp_client_tests
{
    // Read-only tests against an existing SMTP email with plain-text and HTML parts.
    [TestClass]
    public class MessageContentEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Messages.GetMessageTextAsync")]
        public async Task GetMessageTextAsync()
        {
            if (string.IsNullOrWhiteSpace(PrivateDomain) || string.IsNullOrWhiteSpace(MessageId))
                Assert.Inconclusive("Set MAILINATOR_TEST_DOMAIN_PRIVATE and MAILINATOR_TEST_MESSAGE_ID for an existing multipart email.");

            try
            {
                var response = await mailinatorClient.MessagesClient.GetMessageTextAsync(
                    new GetMessageTextRequest { Domain = PrivateDomain, MessageId = MessageId });

                Assert.IsNotNull(response);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.Text),
                    "The configured email must contain nonempty text content.");
            }
            catch (ApiException exception)
            {
                // Do not expose message content or API error bodies in test output.
                Assert.Fail($"Message content retrieval failed with HTTP status {(int)exception.HttpStatusCode}.");
            }
        }

        [TestMethod, TestCategory("Messages.GetMessageTextPlainAsync")]
        public async Task GetMessageTextPlainAsync()
        {
            if (string.IsNullOrWhiteSpace(PrivateDomain) || string.IsNullOrWhiteSpace(MessageId))
                Assert.Inconclusive("Set MAILINATOR_TEST_DOMAIN_PRIVATE and MAILINATOR_TEST_MESSAGE_ID for an existing multipart email.");

            try
            {
                var response = await mailinatorClient.MessagesClient.GetMessageTextPlainAsync(
                    new GetMessageTextPlainRequest { Domain = PrivateDomain, MessageId = MessageId });

                Assert.IsNotNull(response);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.TextPlain),
                    "The configured email must contain nonempty textplain content.");
            }
            catch (ApiException exception)
            {
                // Do not expose message content or API error bodies in test output.
                Assert.Fail($"Message content retrieval failed with HTTP status {(int)exception.HttpStatusCode}.");
            }
        }

        [TestMethod, TestCategory("Messages.GetMessageTextHtmlAsync")]
        public async Task GetMessageTextHtmlAsync()
        {
            if (string.IsNullOrWhiteSpace(PrivateDomain) || string.IsNullOrWhiteSpace(MessageId))
                Assert.Inconclusive("Set MAILINATOR_TEST_DOMAIN_PRIVATE and MAILINATOR_TEST_MESSAGE_ID for an existing multipart email.");

            try
            {
                var response = await mailinatorClient.MessagesClient.GetMessageTextHtmlAsync(
                    new GetMessageTextHtmlRequest { Domain = PrivateDomain, MessageId = MessageId });

                Assert.IsNotNull(response);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.TextHtml),
                    "The configured email must contain nonempty texthtml content.");
            }
            catch (ApiException exception)
            {
                // Do not expose message content or API error bodies in test output.
                Assert.Fail($"Message content retrieval failed with HTTP status {(int)exception.HttpStatusCode}.");
            }
        }

    }
}
