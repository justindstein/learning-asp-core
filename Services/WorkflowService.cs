using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests.Inbound;
using System.Text;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        private readonly string? createOrderUrl = "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/$Order?api-version={api-version}&$expand=all";

        public WorkflowService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, AppDbContext appDbContext)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");
            _configuration = configuration;
            _appDbContext = appDbContext;

            // Retrieve credentials from configuration
            string? username = _configuration["Azure:Username"];
            string? password = _configuration["Azure:Password"];

            // Encode credentials in Base64
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"justin.stein@newwavena.com:G7Xkgrag4kCA0kV55aaZpA5Kpt2RIeibcnsFtdVhHk3qIenhcwkJJQQJ99AKACAAAAAw6rZpAAASAZDO9hta"));

            // Add Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
        }

        public async void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            // Convert the request object to JSON
            //string requestBody = System.Text.Json.JsonSerializer.Serialize(openWorkflowRequest);
            string requestBody = openWorkflowRequest.ToCreateOrderWorkItemRequest().ToRequestBody();
            _logger.LogInformation("requestBody: " + requestBody);

            // Create an HTTP content object
            HttpContent content = new StringContent(requestBody, Encoding.UTF8, "application/json-patch+json");

            // convert OpenWorkflowRequest to API request object for azure devops
            // send message to devops
            HttpResponseMessage response = await _httpClient.PostAsync("https://dev.azure.com/aheadapparel/order-workflow/_apis/wit/workitems/$Order?api-version=7.1&$expand=all", content);
            // response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Status Code: {StatusCode} Response Body: {ResponseBody}", response.StatusCode, responseBody); 
        }

        public async void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            string? organization = _configuration["Microsoft:Azure:Organization"];
            string? project = _configuration["Microsoft:Azure:Project"];
            string? apiVersion = _configuration["Microsoft:Azure:Api.Version"];
            string? url = _configuration["Microsoft:Azure:Url"];
            // string.Format("https://dev.azure.com/{0}/{1}/_apis/wit/workitems/$Order?api-version={2}&$expand=all", organization, project, apiVersion);

            // convert closeWorkflowRequest to object that needs to be run against db
            // update db
            // message ASP
        }
    }
}
