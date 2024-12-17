using learning_asp_core.Models.Requests.Inbound;

namespace learning_asp_core.Services
{
    public class ApprovalService
    {
        private readonly ILogger<ApprovalService> _logger;
        private readonly GoogleService _googleService;

        public ApprovalService(ILogger<ApprovalService> logger, GoogleService googleService)
        {
            _logger = logger;
            _googleService = googleService;
        }

        public void CreateApproval()
        {
            _googleService.CreateForm();
        }
    }
}
