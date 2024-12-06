using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using System.Text;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly AzureService _azureService;

        public WorkflowService(ILogger<WorkflowController> logger, AzureService azureService)
        {
            _logger = logger;
            _azureService = azureService;
        }

        public async void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            string parentRef = _azureService.CreateOrder(openWorkflowRequest.ToCreateOrderWorkItemRequest());


            foreach (CreateSuborderWorkItemRequest createSuborderWorkItemRequest in openWorkflowRequest.ToCreateSuborderWorkItemRequests(parentRef))
            {
                _azureService.CreateSubOrder(createSuborderWorkItemRequest);
            }
        }

        public async void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            // convert closeWorkflowRequest to object that needs to be run against db
            // update db
            // message ASP
        }
    }
}
