using learning_asp_core.Controllers;
using learning_asp_core.Models.Requests;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;

        private readonly string? createOrderUrl = "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/$Order?api-version={api-version}&$expand=all";

        public WorkflowService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");
        }

        public async void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            // convert OpenWorkflowRequest to API request object for azure devops
            // send message to devops

            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Status Code: {StatusCode} Response Body: {ResponseBody}", response.StatusCode, responseBody);
        }

        public async void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            // convert closeWorkflowRequest to object that needs to be run against db
            // update db
            // message ASP

        }
    }
}
