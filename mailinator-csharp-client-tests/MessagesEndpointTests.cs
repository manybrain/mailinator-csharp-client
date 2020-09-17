using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class MessagesEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Messages.InjectMessageAsync")]
        public async Task InjectMessageAsync()
        {
            var message = new MessageToPost()
            {
                Subject = "Testing message",
                From = "test_email@test.com",
                Text = "Hello World!"
            };
            var request = new InjectMessageRequest() { Domain = Domain.Name, Inbox = InboxAll, Message = message };
            var response = await mailinatorClient.MessagesClient.InjectMessageAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Messages.FetchInboxAsync")]
        public async Task FetchInboxAsync()
        {
            var request = new FetchInboxRequest() { Domain = Domain.Name, Inbox = InboxAll, Skip = 0, Limit = 20, Sort = Sort.asc };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageAsync")]
        public async Task FetchMessageAsync()
        {
            var request = new FetchMessageRequest() { Domain = Domain.Name, Inbox = InboxAll, MessageId = Message.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchSMSMessagesAsync")]
        public async Task FetchSMSMessagesAsync()
        {
            var request = new FetchSMSMessagesRequest() { Domain = Domain.Name, TeamSMSNumber = TeamSMSNumber };
            var response = await mailinatorClient.MessagesClient.FetchSMSMessagesAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchAttachmentsAsync")]
        public async Task FetchAttachmentsAsync()
        {
            var request = new FetchAttachmentsRequest() { Domain = Domain.Name, Inbox = PrivateInbox, MessageId = MessageIdWithAttachment };
            var response = await mailinatorClient.MessagesClient.FetchAttachmentsAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchAttachmentAsync")]
        public async Task FetchAttachmentAsync()
        {
            var request = new FetchAttachmentRequest() { Domain = Domain.Name, Inbox = PrivateInbox, MessageId = MessageIdWithAttachment, AttachmentId = AttachmentId };
            var response = await mailinatorClient.MessagesClient.FetchAttachmentAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageLinksAsync")]
        public async Task FetchMessageLinksAsync()
        {
            var request = new FetchMessageLinksRequest() { Domain = Domain.Name, Inbox = PrivateInbox, MessageId = MessageIdWithAttachment };
            var response = await mailinatorClient.MessagesClient.FetchMessageLinksAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.DeleteMessageAsync")]
        public async Task DeleteMessageAsync()
        {
            var request = new DeleteMessageRequest() { Domain = DeleteDomain, Inbox = InboxAll, MessageId = Message.Id };
            var response = await mailinatorClient.MessagesClient.DeleteMessageAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.DeleteAllInboxMessagesAsync")]
        public async Task DeleteAllInboxMessagesAsync()
        {
            var request = new DeleteAllInboxMessagesRequest() { Domain = DeleteDomain, Inbox = PrivateInbox };
            var response = await mailinatorClient.MessagesClient.DeleteAllInboxMessagesAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.DeleteAllDomainMessagesAsync")]
        public async Task DeleteAllDomainMessagesAsync()
        {
            var request = new DeleteAllDomainMessagesRequest() { Domain = DeleteDomain };
            var response = await mailinatorClient.MessagesClient.DeleteAllDomainMessagesAsync(request);
            Assert.IsTrue(response != null);
        }
    }
}
