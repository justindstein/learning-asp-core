using learning_asp_core.Controllers;
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

        public void Post()
        {
        }
    }
}
