using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using System.Text;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly AzureService _azureService;
        private readonly AheadService _aheadService;
        private readonly GoogleService _googleService;

        public WorkflowService(ILogger<WorkflowController> logger, AppDbContext appDbContext, AzureService azureService, AheadService aheadService, GoogleService googleService)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _azureService = azureService;
            _aheadService = aheadService;
            _googleService = googleService;
        }

        public async void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            // Create db entry for order and each suborder

            string parentRef = _azureService.CreateOrder(openWorkflowRequest.ToCreateOrderWorkItemRequest());

            // Update order db entry as created

            foreach (CreateSuborderWorkItemRequest createSuborderWorkItemRequest in openWorkflowRequest.ToCreateSuborderWorkItemRequests(parentRef))
            {
                _azureService.CreateSubOrder(createSuborderWorkItemRequest);

                // Update suborder db entry as created 
            }
        }

        public async void UpdateWorkflow()
        {
            // Check if order
            // Check 
        }

        public async void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            // Check if order and state is complete, and db entry is not already complete

            // Update order db entry as completed

            // Send POST to ahead api

            
            //var workflows = _appDbContext.Workflows.ToList();
            //foreach (var workflow in workflows)
            //{
            //    _logger.LogInformation(workflow.Name);
            //}
        }
    }
}
