﻿using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using System.Text;

namespace learning_asp_core.Services
{
    public class AheadService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;

        private readonly string _url;

        public AheadService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("retryClient");

            // Retrieve credentials from configuration
            string username = configuration["New.Wave.Group:Ahead:Username"];
            string password = configuration["New.Wave.Group:Ahead:Password"];
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

            _url = configuration["New.Wave.Group:Ahead:Url"];
        }

        public string CreateOrder(CreateOrderWorkItemRequest createOrderWorkItemRequest)
        {
            HttpContent content = new StringContent(createOrderWorkItemRequest.ToRequestBody(), Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = _httpClient.PostAsync(_url.Replace("{workItem}", "$Order"), content)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Models.Responses.CreateWorkflowResponse createWorkflowResponse = System.Text.Json.JsonSerializer.Deserialize<Models.Responses.CreateWorkflowResponse>(responseBody, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _logger.LogInformation("Status Code: {StatusCode} Url: {Url} Response Body: {ResponseBody}", response.StatusCode, createWorkflowResponse.Url, responseBody);

            return createWorkflowResponse.Url;
        }

        public async void CreateSubOrder(CreateSuborderWorkItemRequest createSuborderWorkItemRequest)
        {
            HttpContent content = new StringContent(createSuborderWorkItemRequest.ToRequestBody(), Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = await _httpClient.PostAsync(_url.Replace("{workItem}", "$Suborder"), content);

            string responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Status Code: {StatusCode} Response Body: {ResponseBody}", response.StatusCode, responseBody);
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