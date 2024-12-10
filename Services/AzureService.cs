using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;
using learning_asp_core.Utils.Extensions;
using System.Text;

namespace learning_asp_core.Services
{
    public class AzureService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;

        private readonly string _url;

        public AzureService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");
            _httpClient.ApplyBasicCredentials(
                configuration["New.Wave.Group:Ahead.Sales.Platform:Username"] ?? string.Empty
                , configuration["New.Wave.Group:Ahead.Sales.Platform:Password"] ?? string.Empty
            );
            _url = configuration["Microsoft:Azure:Url"] ?? string.Empty
              .Replace("{organization}", configuration["Microsoft:Azure:Organization"]) ?? string.Empty
              .Replace("{project}", configuration["Microsoft:Azure:Project"]) ?? string.Empty
              .Replace("{apiVersion}", configuration["Microsoft:Azure:Api.Version"]) ?? string.Empty; 
        }

        public CreateWorkflowResponse CreateOrder(CreateWorkItemRequest createOrderWorkItemRequest)
        {
            HttpContent content = new StringContent(createOrderWorkItemRequest.ToRequestBody(), Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = _httpClient.PostAsync(_url.Replace("{workItem}", "$Order"), content)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Models.Responses.CreateWorkflowResponse createWorkflowResponse = System.Text.Json.JsonSerializer.Deserialize<Models.Responses.CreateWorkflowResponse>(responseBody, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _logger.LogInformation("Status Code: {StatusCode} Url: {Url} Response Body: {ResponseBody}", response.StatusCode, createWorkflowResponse.Url, responseBody);

            //return createWorkflowResponse.Url;
            return createWorkflowResponse;
        }

        public CreateWorkflowResponse CreateSuborder(CreateWorkItemRequest createSuborderWorkItemRequest)
        {
            HttpContent content = new StringContent(createSuborderWorkItemRequest.ToRequestBody(), Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = _httpClient.PostAsync(_url.Replace("{workItem}", "$Suborder"), content)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Models.Responses.CreateWorkflowResponse createWorkflowResponse = System.Text.Json.JsonSerializer.Deserialize<Models.Responses.CreateWorkflowResponse>(responseBody, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _logger.LogInformation("Status Code: {StatusCode} Url: {Url} Response Body: {ResponseBody}", response.StatusCode, createWorkflowResponse.Url, responseBody);

            return createWorkflowResponse;
        }

        public async void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            //string? organization = _configuration["Microsoft:Azure:Organization"];
            //string? project = _configuration["Microsoft:Azure:Project"];
            //string? apiVersion = _configuration["Microsoft:Azure:Api.Version"];
            //string? url = _configuration["Microsoft:Azure:Url"];
            // string.Format("https://dev.azure.com/{0}/{1}/_apis/wit/workitems/$Order?api-version={2}&$expand=all", organization, project, apiVersion);

            // convert closeWorkflowRequest to object that needs to be run against db
            // update db
            // message ASP
        }
    }
}
