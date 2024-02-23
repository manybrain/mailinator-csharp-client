using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Domains.Entities;
using mailinator_csharp_client.Models.Messages.Entities;
using mailinator_csharp_client.Models.Messages.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace mailinator_csharp_client_tests
{
    [TestClass]
    public class MessagesEndpointTests : TestBase
    {
        [TestMethod, TestCategory("Messages.PostMessageAsync")]
        public async Task PostMessageAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;

            var response = await PostNewMessageAsync(domain, inbox);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Status == "ok");

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = response?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxWithPrivateDomainAsync")]
        public async Task FetchInboxWithPrivateDomainAsync()
        {
            var request = new FetchInboxRequest() { Domain = PrivateDomain };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchInboxWithPrivateDomainAndInboxAsync")]
        public async Task FetchInboxWithPrivateDomainAndInboxAsync()
        {
            var request = new FetchInboxRequest() { Domain = PrivateDomain, Inbox = PrivateInbox };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchInboxWithPrivateDomainCommonAsync")]
        public async Task FetchInboxWithPrivateDomainCommonAsync()
        {
            var request = new FetchInboxRequest() { Domain = DomainType.PRIVATE };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchInboxWithPublicDomainCommonAsync")]
        public async Task FetchInboxWithPublicDomainCommonAsync()
        {
            var request = new FetchInboxRequest() { Domain = DomainType.PUBLIC };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchInboxWithPrivateDomainWithQueryParamsAsync")]
        public async Task FetchInboxWithPrivateDomainWithQueryParamsAsync()
        {
            var request = new FetchInboxRequest() { Domain = PrivateDomain, /*Inbox = null,*/ Skip = 10, Limit = 20, Sort = Sort.desc, DecodeSubject = true };
            var response = await mailinatorClient.MessagesClient.FetchInboxAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageAsync")]
        public async Task FetchMessageAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchMessageRequest() { Domain = domain, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchMessageWhenMessageDoesNotExistAsync")]
        public async Task FetchMessageWhenMessageDoesNotExistAsync()
        {
            var request = new FetchMessageRequest() { Domain = PrivateDomain, MessageId = DateTime.UtcNow.Ticks.ToString() };
            var exception = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
            {
                await mailinatorClient.MessagesClient.FetchMessageAsync(request);
            });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageAsync")]
        public async Task FetchInboxMessageAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchInboxMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageWhenMessageDoesNotExistAsync")]
        public async Task FetchInboxMessageWhenMessageDoesNotExistAsync()
        {
            var request = new FetchInboxMessageRequest() { Domain = PrivateDomain, Inbox = PrivateInbox, MessageId = DateTime.UtcNow.Ticks.ToString() };
            var exception = await Assert.ThrowsExceptionAsync<ApiException>(async () =>
            {
                await mailinatorClient.MessagesClient.FetchInboxMessageAsync(request);
            });
        }

        [TestMethod, TestCategory("Messages.FetchSMSMessagesAsync")]
        public async Task FetchSMSMessagesAsync()
        {
            var request = new FetchSMSMessagesRequest() { Domain = DomainType.PUBLIC, TeamSMSNumber = TeamSMSNumber };
            var response = await mailinatorClient.MessagesClient.FetchSMSMessagesAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageAttachmentsAsync")]
        public async Task FetchAttachmentsAsync()
        {
            var request = new FetchMessageAttachmentsRequest() { Domain = PrivateDomain, MessageId = MessageIdWithAttachment };
            var response = await mailinatorClient.MessagesClient.FetchMessageAttachmentsAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageAttachmentsMissingAsync")]
        public async Task FetchAttachmentsMissingAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchMessageAttachmentsRequest() { Domain = domain, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageAttachmentsAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Attachments.Count == 0);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageAttachmentsAsync")]
        public async Task FetchInboxMessageAttachmentsAsync()
        {
            var request = new FetchInboxMessageAttachmentsRequest() { Domain = PrivateDomain, Inbox = PrivateInbox, MessageId = MessageIdWithAttachment };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageAttachmentsAsync(request);
            Assert.IsTrue(response != null);
        }


        [TestMethod, TestCategory("Messages.FetchInboxMessageAttachmentsMissingAsync")]
        public async Task FetchInboxMessageAttachmentsMissingAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchInboxMessageAttachmentsRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageAttachmentsAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Attachments.Count == 0);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchMessageAttachmentAsync")]
        public async Task FetchMessageAttachmentAsync()
        {
            var request = new FetchMessageAttachmentRequest() { Domain = PrivateDomain, MessageId = MessageIdWithAttachment, AttachmentId = AttachmentId };
            var response = await mailinatorClient.MessagesClient.FetchMessageAttachmentAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageAttachmentAsync")]
        public async Task FetchInboxMessageAttachmentAsync()
        {
            var request = new FetchInboxMessageAttachmentRequest() { Domain = PrivateDomain, Inbox = PrivateInbox, MessageId = MessageIdWithAttachment, AttachmentId = AttachmentId };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageAttachmentAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageLinksAsync")]
        public async Task FetchMessageLinksAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchMessageLinksRequest() { Domain = domain, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageLinksAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageLinksAsync")]
        public async Task FetchInboxMessageLinksAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchInboxMessageLinksRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageLinksAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.DeleteMessageAsync")]
        public async Task DeleteMessageAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.DeleteMessageAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Count == 1);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Messages.DeleteAllInboxMessagesAsync")]
        public async Task DeleteAllInboxMessagesAsync()
        {
            var domain = PrivateDomain;
            var inbox = $"inbox {DateTime.UtcNow.Ticks}";
            await PostNewMessageAsync(domain, inbox);
            await PostNewMessageAsync(domain, inbox);
            await PostNewMessageAsync(domain, inbox);

            var request = new DeleteAllInboxMessagesRequest() { Domain = domain, Inbox = inbox };
            var response = await mailinatorClient.MessagesClient.DeleteAllInboxMessagesAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Count == 3);
            Assert.IsTrue(response.Status == "ok");
        }

        [TestMethod, TestCategory("Messages.DeleteAllDomainMessagesAsync")]
        public async Task DeleteAllDomainMessagesAsync()
        {
            var request = new DeleteAllDomainMessagesRequest() { Domain = DeleteDomain };
            var response = await mailinatorClient.MessagesClient.DeleteAllDomainMessagesAsync(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod, TestCategory("Messages.FetchMessageSmtpLogAsync")]
        public async Task FetchMessageSmtpLogAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchMessageSmtpLogRequest() { Domain = domain, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageSmtpLogAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageSmtpLogAsync")]
        public async Task FetchInboxMessageSmtpLogAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchInboxMessageSmtpLogRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageSmtpLogAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchInboxMessageRawAsync")]
        public async Task FetchInboxMessageRawAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchInboxMessageRawRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchInboxMessageRawAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchMessageRawAsync")]
        public async Task FetchMessageRawAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchMessageRawRequest() { Domain = domain, MessageId = message?.Id };
            var response = await mailinatorClient.MessagesClient.FetchMessageRawAsync(request);
            Assert.IsTrue(response != null);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchLatestMessagesAsync")]
        public async Task FetchLatestMessagesAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchLatestMessagesRequest() { Domain = domain };
            var response = await mailinatorClient.MessagesClient.FetchLatestMessagesAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Messages?.Count > 0);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }

        [TestMethod, TestCategory("Messages.FetchLatestInboxMessagesAsync")]
        public async Task FetchLatestInboxMessagesAsync()
        {
            var domain = PrivateDomain;
            var inbox = PrivateInbox;
            var message = await PostNewMessageAsync(domain, inbox);

            var request = new FetchLatestInboxMessagesRequest() { Domain = domain, Inbox = inbox };
            var response = await mailinatorClient.MessagesClient.FetchLatestInboxMessagesAsync(request);
            Assert.IsTrue(response != null);
            Assert.IsTrue(response.Messages?.Count > 0);

            await mailinatorClient.MessagesClient.DeleteMessageAsync(new DeleteMessageRequest() { Domain = domain, Inbox = inbox, MessageId = message?.Id });
        }
    }
}
