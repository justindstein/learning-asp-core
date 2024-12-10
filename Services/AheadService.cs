using learning_asp_core.Controllers;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Utils.Extensions;
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
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.ApplyBasicCredentials(
                configuration["New.Wave.Group:Ahead.Sales.Platform:Username"] ?? string.Empty
                , configuration["New.Wave.Group:Ahead.Sales.Platform:Password"] ?? string.Empty
            );
            _url = configuration["New.Wave.Group:Ahead.Sales.Platform:Url"] ?? string.Empty;
        }

        public void OrderComplete(OrderCompletedRequest orderCompleteRequest)
        {
            _logger.LogDebug("AheadService.OrderComplete [OrderCompletedRequest: {OrderCompletedRequest}]", orderCompleteRequest);
            HttpContent content = new StringContent(orderCompleteRequest.ToRequestBody(), Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = _httpClient.PostAsync(_url, content).GetAwaiter().GetResult();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            _logger.LogInformation("AheadService.OrderComplete [Status Code: {StatusCode}]", response.StatusCode);
        }
    }
}
