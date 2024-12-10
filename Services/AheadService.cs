using learning_asp_core.Controllers;

namespace learning_asp_core.Services
{
    public class AheadService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly HttpClient _httpClient;

        public AheadService(ILogger<WorkflowController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public void Post()
        {
        }
    }
}
