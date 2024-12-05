using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests.Inbound;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Xml.Linq;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly string? createOrderUrl = "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/$Order?api-version={api-version}&$expand=all";

        public WorkflowService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");
            _configuration = configuration;
        }

        public async void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            // convert OpenWorkflowRequest to API request object for azure devops
            // send message to devops

            HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();

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
