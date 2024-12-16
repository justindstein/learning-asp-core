using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;
using learning_asp_core.Utils.Extensions;
using System.Text;
using System.Text.Json;

namespace learning_asp_core.Services
{
    public class AzureService
    {
        private static readonly string APPLICATION_JSON_PATCH_JSON = "application/json-patch+json";

        private readonly ILogger<AzureService> _logger;
        private readonly HttpClient _httpClient;

        private readonly string _orderUrl;
        private readonly string _suborderUrl;

        private readonly Dictionary<Type, string> urls = new Dictionary<Type, string>();

        public AzureService(ILogger<AzureService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");
            _httpClient.ApplyBasicCredentials(
                (configuration["Microsoft:Azure:Username"] ?? string.Empty)
                , (configuration["Microsoft:Azure:Password"] ?? string.Empty)
            );

            // Add 'CreateOrderWorkItemRequest' url
            urls.Add(
                typeof(CreateOrderWorkItemRequest)
                , (configuration["Microsoft:Azure:Url"] ?? string.Empty)
                  .Replace("{organization}", configuration["Microsoft:Azure:Organization"] ?? string.Empty)
                  .Replace("{project}", configuration["Microsoft:Azure:Project"] ?? string.Empty)
                  .Replace("{apiVersion}", configuration["Microsoft:Azure:Api.Version"] ?? string.Empty)
                  .Replace("{workItem}", "$Order")
            );

            // Add 'CreateSuborderWorkItemRequest' url
            urls.Add(
                typeof(CreateSuborderWorkItemRequest)
                , (configuration["Microsoft:Azure:Url"] ?? string.Empty)
                  .Replace("{organization}", configuration["Microsoft:Azure:Organization"] ?? string.Empty)
                  .Replace("{project}", configuration["Microsoft:Azure:Project"] ?? string.Empty)
                  .Replace("{apiVersion}", configuration["Microsoft:Azure:Api.Version"] ?? string.Empty)
                  .Replace("{workItem}", "$Suborder")
            );
        }

        public CreateWorkflowResponse CreateWorkItem(CreateWorkItemRequest createOrderWorkItemRequest)
        {
            _logger.LogDebug("AzureService.CreateWorkItem [CreateWorkItemRequest: {CreateWorkItemRequest}]", createOrderWorkItemRequest);

            string unencodedBody = createOrderWorkItemRequest.ToRequestBody();
            HttpContent encodedBody = new StringContent(unencodedBody, Encoding.UTF8, APPLICATION_JSON_PATCH_JSON);

            String url = urls[createOrderWorkItemRequest.GetType()];
            HttpResponseMessage response = _httpClient.PostAsync(url, encodedBody)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            CreateWorkflowResponse createWorkflowResponse = JsonSerializer.Deserialize<CreateWorkflowResponse>(responseBody, new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true 
            });
            _logger.LogInformation("AzureService.CreateWorkflowResponse [Status Code: {StatusCode}] [Url: {Url}] [Response Body: {ResponseBody}]", response.StatusCode, url, responseBody);

            return createWorkflowResponse;
        }
    }
}
