using mailinator_csharp_client.Clients.HttpClient;
using mailinator_csharp_client.Models.Webhooks.Requests;
using mailinator_csharp_client.Models.Webhooks.Responses;
using RestSharp;
using System.Threading.Tasks;

namespace mailinator_csharp_client.Clients.ApiClients.Webhooks
{
    /// <summary>
    /// Client for Webhooks API.
    /// 
    /// Webhooks into your Private System do NOT use your regular API Token.
    /// This is because a typical use case is to enter the Webhook URL into 3rd-party systems(i.e.Twilio, Zapier, IFTTT, etc) and you should never give out your API Token.
    /// Check your Team Settings where you can create "Webhook Tokens" designed for this purpose.
    /// </summary>
    public class WebhooksClient : IApiClient
    {
        private readonly IHttpClient httpClient;
        private readonly string endpointUrl;

        public WebhooksClient(IHttpClient httpClient, string endpointUrl)
        {
            this.httpClient = httpClient;
            this.endpointUrl = endpointUrl;
        }

        /// <summary>
        /// This command will deliver the message to the :to inbox that was set into request object
        /// </summary>
        /// <param name="request">PublicWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PublicWebhookResponse> PublicWebhookAsync(PublicWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/public/webhook", Method.Post);
            
            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PublicWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This command will deliver the message to the :inbox inbox
        /// Note that if the Mailinator system cannot determine the destination inbox via the URL or a "to" field in the payload, the message will be rejected.
        /// If the message contains a "from" and "subject" field, these will be visible on the inbox page.
        /// </summary>
        /// <param name="request">PublicInboxWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PublicWebhookResponse> PublicInboxWebhookAsync(PublicInboxWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/public/webhook/{inbox}", Method.Post);

            requestObject.AddUrlSegment("inbox", request.Inbox);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PublicWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// If you have a Twilio account which receives incoming SMS messages. You may direct those messages through this facility to inject those messages into the Mailinator system.
        /// </summary>
        /// <param name="request">PublicCustomServiceWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PublicCustomServiceWebhookResponse> PublicCustomServiceWebhookAsync(PublicCustomServiceWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/public/{customService}", Method.Post);

            requestObject.AddUrlSegment("customService", request.CustomService);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PublicCustomServiceWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// The SMS message will arrive in the Public Mailinator inbox corresponding to the Twilio Phone Number. (only the digits, if a plus sign precedes the number it will be removed) 
        /// If you wish the message to arrive in a different inbox, you may append the destination inbox to the URL.
        /// </summary>
        /// <param name="request">PublicCustomServiceInboxWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PublicCustomServiceWebhookResponse> PublicCustomServiceInboxWebhookAsync(PublicCustomServiceInboxWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/public/{customService}/{inbox}", Method.Post);

            requestObject.AddUrlSegment("customService", request.CustomService);
            requestObject.AddUrlSegment("inbox", request.Inbox);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PublicCustomServiceWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This command will Webhook messages into your Private Domain
        /// The incoming Webhook will arrive in the inbox designated by the "to" field in the incoming request payload.
        /// Webhooks into your Private System do NOT use your regular API Token.
        /// This is because a typical use case is to enter the Webhook URL into 3rd-party systems(i.e.Twilio, Zapier, IFTTT, etc) and you should never give out your API Token.
        /// Check your Team Settings where you can create "Webhook Tokens" designed for this purpose.
        /// </summary>
        /// <param name="request">PrivateWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PrivateWebhookResponse> PrivateWebhookAsync(PrivateWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{wh-token}/webhook", Method.Post);

            requestObject.AddUrlSegment("wh-token", request.WebhookToken);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PrivateWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// This command will deliver the message to the :inbox inbox
        /// Incoming Webhooks are delivered to Mailinator inboxes and from that point onward are not notably different than other messages in the system (i.e. emails). 
        /// As normal, Mailinator will list all messages in the Inbox page and via the Inbox API calls. 
        /// If the incoming JSON payload does not contain a "from" or "subject", then dummy values will be inserted in these fields.
        /// You may retrieve such messages via the Web Interface, the API, or the Rule System
        /// </summary>
        /// <param name="request">PrivateInboxWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PrivateWebhookResponse> PrivateInboxWebhookAsync(PrivateInboxWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{wh-token}/webhook/{inbox}", Method.Post);

            requestObject.AddUrlSegment("wh-token", request.WebhookToken);
            requestObject.AddUrlSegment("inbox", request.Inbox);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PrivateWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// If you have a Twilio account which receives incoming SMS messages. You may direct those messages through this facility to inject those messages into the Mailinator system.
        /// Mailinator intends to apply specific mappings for certain services that commonly publish webhooks.
        /// If you test incoming Messages to SMS numbers via Twilio, you may use this endpoint to correctly map "to", "from", and "subject" of those messages to the Mailinator system.By default, the destination inbox is the Twilio phone number.
        /// </summary>
        /// <param name="request">PrivateCustomServiceWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PrivateCustomServiceWebhookResponse> PrivateCustomServiceWebhookAsync(PrivateCustomServiceWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{wh-token}/{customService}", Method.Post);

            requestObject.AddUrlSegment("wh-token", request.WebhookToken);
            requestObject.AddUrlSegment("customService", request.CustomService);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PrivateCustomServiceWebhookResponse>(requestObject);
            return response;
        }

        /// <summary>
        /// The SMS message will arrive in the Private Mailinator inbox corresponding to the Twilio Phone Number. (only the digits, if a plus sign precedes the number it will be removed) 
        /// If you wish the message to arrive in a different inbox, you may append the destination inbox to the URL.
        /// </summary>
        /// <param name="request">PrivateCustomServiceInboxWebhookRequest object.</param>
        /// <returns></returns>
        public async Task<PrivateCustomServiceWebhookResponse> PrivateCustomServiceInboxWebhookAsync(PrivateCustomServiceInboxWebhookRequest request)
        {
            var requestObject = httpClient.GetRequest(endpointUrl + "/{wh-token}/{customService}/{inbox}", Method.Post);

            requestObject.AddUrlSegment("wh-token", request.WebhookToken);
            requestObject.AddUrlSegment("customService", request.CustomService);
            requestObject.AddUrlSegment("inbox", request.Inbox);

            requestObject.AddJsonBody(request.Webhook);

            var response = await httpClient.ExecuteAsync<PrivateCustomServiceWebhookResponse>(requestObject);
            return response;
        }
    }
}
