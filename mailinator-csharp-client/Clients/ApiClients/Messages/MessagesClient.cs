﻿using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Helpers;
using mailinator_csharp_client.Models.Messages.Requests;
using mailinator_csharp_client.Models.Responses;
using RestSharp;
using System.IO;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Messages
{
    /// <summary>
    /// Client for Messages API
    /// </summary>
    public class MessagesClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public MessagesClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// This endpoint retrieves a list of messages summaries. You can retreive a list by inbox, inboxes, or entire domain.
        /// :domain	
        /// public	Fetch Message Summaries from the Public Mailinator System
        /// private Fetch Message Summaries from all Your Private Domains
        /// [your_private_domain.com]   Fetch Message Summaries from a specific Private Domain
        /// :inbox	
        /// null	Fetch All Messages summaries for an entire domain
        /// * Fetch All Messages summaries for an entire domain
        /// [inbox_name] Fetch All Messages summaries for a given Inbox
        /// [inbox_name *] Fetch All Messages summaries for a given Inbox Prefix
        /// </summary>
        /// <param name="request">FetchInboxRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxResponse> FetchInboxAsync(FetchInboxRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddSafeQueryParameter("skip", request.Skip.ToString());
            requestObject.AddSafeQueryParameter("limit", request.Limit.ToString());
            requestObject.AddSafeQueryParameter("sort", request.Sort.ToString());
            requestObject.AddSafeQueryParameter("decode_subject", request.DecodeSubject.ToString());
            requestObject.AddSafeQueryParameter("cursor", request.Cursor?.ToString());
            requestObject.AddSafeQueryParameter("full", request.Full?.ToString());
            requestObject.AddSafeQueryParameter("delete", request.Delete?.ToString());
            requestObject.AddSafeQueryParameter("wait", request.Wait?.ToString());

            var response = await httpClient.ExecuteAsync<FetchInboxResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves a specific message by id for specific inbox.
        /// </summary>
        /// <param name="request">FetchInboxMessageRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageResponse> FetchInboxMessageAsync(FetchInboxMessageRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves a specific message by id.
        /// </summary>
        /// <param name="request">FetchMessageRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageResponse> FetchMessageAsync(FetchMessageRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            requestObject.AddSafeQueryParameter("delete", request.Delete?.ToString());

            var response = await httpClient.ExecuteAsync<FetchMessageResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// SMS messages go into an inbox by the name of their phone number. Retrieving them is the same as any other message, simply use the phone number as the Inbox you are fetching.
        /// </summary>
        /// <param name="request">FetchSMSMessagesRequest object.</param>
        /// <returns></returns>
        public async Task<FetchSMSMessagesResponse> FetchSMSMessagesAsync(FetchSMSMessagesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{teamSMSNumber}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("teamSMSNumber", request.TeamSMSNumber);

            var response = await httpClient.ExecuteAsync<FetchSMSMessagesResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves a list of attachments for a message for specific inbox. Note attachments are expected to be in Email format.
        /// </summary>
        /// <param name="request">FetchInboxMessageAttachmentsRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageAttachmentsResponse> FetchInboxMessageAttachmentsAsync(FetchInboxMessageAttachmentsRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}/attachments", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageAttachmentsResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves a list of attachments for a message. Note attachments are expected to be in Email format.
        /// </summary>
        /// <param name="request">FetchMessageAttachmentsRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageAttachmentsResponse> FetchMessageAttachmentsAsync(FetchMessageAttachmentsRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/attachments", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchMessageAttachmentsResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves attachment for a message for specific inbox. Note attachment is expected to be in Email format.
        /// </summary>
        /// <param name="request">FetchInboxMessageAttachmentRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageAttachmentResponse> FetchInboxMessageAttachmentAsync(FetchInboxMessageAttachmentRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}/attachments/{attachmentId}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);
            requestObject.AddUrlSegment("attachmentId", request.AttachmentId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageAttachmentResponse>(requestObject, (restResponse) => 
            {
                return new FetchInboxMessageAttachmentResponse
                { 
                    Bytes = restResponse.RawBytes, 
                    Content = restResponse.Content,
                    ContentType = restResponse.ContentType
                };
            });

            return response;
        }

        /// <summary>
        /// This endpoint retrieves attachment for a message. Note attachment is expected to be in Email format.
        /// </summary>
        /// <param name="request">FetchMessageAttachmentRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageAttachmentResponse> FetchMessageAttachmentAsync(FetchMessageAttachmentRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/attachments/{attachmentId}", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);
            requestObject.AddUrlSegment("attachmentId", request.AttachmentId);

            var response = await httpClient.ExecuteAsync<FetchMessageAttachmentResponse>(requestObject, (restResponse) =>
            {
                return new FetchMessageAttachmentResponse
                {
                    Bytes = restResponse.RawBytes,
                    Content = restResponse.Content,
                    ContentType = restResponse.ContentType
                };
            });

            return response;
        }

        /// <summary>
        /// This endpoint retrieves all the links parsed from the email.
        /// </summary>
        /// <param name="request">FetchMessageLinksRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageLinksResponse> FetchMessageLinksAsync(FetchMessageLinksRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/links", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchMessageLinksResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves all the links parsed from the email for specific inbox.
        /// </summary>
        /// <param name="request">FetchInboxMessageLinksRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageLinksResponse> FetchInboxMessageLinksAsync(FetchInboxMessageLinksRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}/links", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageLinksResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves all the links info parsed from the email.
        /// </summary>
        /// <param name="request">FetchMessageLinksRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageLinksFullResponse> FetchMessageLinksFullAsync(FetchMessageLinksFullRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/linksfull", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchMessageLinksFullResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint deletes ALL messages from a Private Domain. Caution: This action is irreversible.
        /// </summary>
        /// <param name="request">DeleteAllDomainMessagesRequest object.</param>
        /// <returns></returns>
        public async Task<DeleteAllDomainMessagesResponse> DeleteAllDomainMessagesAsync(DeleteAllDomainMessagesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes", Method.Delete);
            requestObject.AddUrlSegment("domain", request.Domain);

            var response = await httpClient.ExecuteAsync<DeleteAllDomainMessagesResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint deletes ALL messages from a specific private inbox.
        /// </summary>
        /// <param name="request">DeleteAllInboxMessagesRequest object.</param>
        /// <returns></returns>
        public async Task<DeleteAllInboxMessagesResponse> DeleteAllInboxMessagesAsync(DeleteAllInboxMessagesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}", Method.Delete);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);

            var response = await httpClient.ExecuteAsync<DeleteAllInboxMessagesResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint deletes a specific messages
        /// </summary>
        /// <param name="request">DeleteMessageRequest object.</param>
        /// <returns></returns>
        public async Task<DeleteMessageResponse> DeleteMessageAsync(DeleteMessageRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}", Method.Delete);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<DeleteMessageResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint allows you to deliver a JSON message into your private domain. 
        /// This is similar to simply emailing a message to your private domain, 
        /// except that you use HTTP Post and can programmatically inject the message.
        /// Note that injected JSON Messages can have any schema they choose.
        /// However, if you want the Web interface to display them, 
        /// they must follow a general email format with the fields of From, 
        /// Subject, and Parts(see "Fetch Message" above).
        /// </summary>
        /// <param name="request">PostMessageRequest object.</param>
        /// <returns></returns>
        public async Task<PostMessageResponse> PostMessageAsync(PostMessageRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages", Method.Post);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            
            requestObject.AddJsonBody(request.Message);

            var response = await httpClient.ExecuteAsync<PostMessageResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves smtp log from the email.
        /// </summary>
        /// <param name="request">FetchMessageSmtpLogRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageSmtpLogResponse> FetchMessageSmtpLogAsync(FetchMessageSmtpLogRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/smtplog", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchMessageSmtpLogResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves smtp log from the email for specific inbox.
        /// </summary>
        /// <param name="request">FetchInboxMessageSmtpLogRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageSmtpLogResponse> FetchInboxMessageSmtpLogAsync(FetchInboxMessageSmtpLogRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}/smtplog", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageSmtpLogResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This endpoint retrieves raw info from the email.
        /// </summary>
        /// <param name="request">FetchMessageRawRequest object.</param>
        /// <returns></returns>
        public async Task<FetchMessageRawResponse> FetchMessageRawAsync(FetchMessageRawRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/{messageId}/raw", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchMessageRawResponse>(requestObject, (restResponse) =>
            {
                return new FetchMessageRawResponse
                {
                    RawData = restResponse.Content
                };
            });

            return response;
        }

        /// <summary>
        /// This endpoint retrieves raw info from the email for specific inbox.
        /// </summary>
        /// <param name="request">FetchInboxMessageRawRequest object.</param>
        /// <returns></returns>
        public async Task<FetchInboxMessageRawResponse> FetchInboxMessageRawAsync(FetchInboxMessageRawRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/{messageId}/raw", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);
            requestObject.AddUrlSegment("messageId", request.MessageId);

            var response = await httpClient.ExecuteAsync<FetchInboxMessageRawResponse>(requestObject, (restResponse) =>
            {
                return new FetchInboxMessageRawResponse
                {
                    RawData = restResponse.Content
                };
            });

            return response;
        }

        /// <summary>
        /// That fetches the latest 5 FULL messages.
        /// </summary>
        /// <param name="request">FetchLatestMessagesResponse object.</param>
        /// <returns></returns>
        public async Task<FetchLatestMessagesResponse> FetchLatestMessagesAsync(FetchLatestMessagesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/messages/*", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);

            var response = await httpClient.ExecuteAsync<FetchLatestMessagesResponse>(requestObject);

            return response;
        }

        /// <summary>
        /// That fetches the latest 5 FULL messages for specific inbox.
        /// </summary>
        /// <param name="request">FetchLatestInboxMessagesResponse object.</param>
        /// <returns></returns>
        public async Task<FetchLatestInboxMessagesResponse> FetchLatestInboxMessagesAsync(FetchLatestInboxMessagesRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{domain}/inboxes/{inbox}/messages/*", Method.Get);
            requestObject.AddUrlSegment("domain", request.Domain);
            requestObject.AddUrlSegment("inbox", request.Inbox);

            var response = await httpClient.ExecuteAsync<FetchLatestInboxMessagesResponse>(requestObject);

            return response;
        }
    }
}
