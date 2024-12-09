using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;

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
            Workflow workflow = new Workflow("Order", new HashSet<string> { "OrderId: 1001", "CustomerName: John Doe", "Priority: Event", "SubmitDate: 1/1/2024" });
            workflow = insertWorkflow(workflow);

            CreateWorkflowResponse response = _azureService.CreateOrder(openWorkflowRequest.ToCreateOrderWorkItemRequest());
            workflow.Update(response.Id, response.Url);
            updateWorkflow(workflow);

            foreach (CreateSuborderWorkItemRequest createSuborderWorkItemRequest in openWorkflowRequest.ToCreateSuborderWorkItemRequests(response.Url))
            {
                workflow = new Workflow("Suborder", new HashSet<string> { "OrderId: 2001", "CustomerName: Jane Doe", "Priority: Low", "SubmitDate: 1/1/2025" });
                workflow = insertWorkflow(workflow);

                response = _azureService.CreateSuborder(createSuborderWorkItemRequest);
                workflow.Update(response.Id, response.Url);
                updateWorkflow(workflow);
            }
        }

        private Workflow insertWorkflow(Workflow workflow)
        {
            _appDbContext.Workflows.Add(workflow);
            _appDbContext.SaveChanges();
            _logger.LogInformation("Record inserted: " + workflow.WorkflowID);
            return workflow;
        }

        private void updateWorkflow(Workflow workflow)
        {
            bool exists = _appDbContext.Workflows.Any(w => w.WorkflowID == workflow.WorkflowID);
            if (exists)
            {
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogInformation("Record updated " + workflow.WorkflowID);
            }
            else
            {
                _logger.LogInformation("Record does not exist " + workflow.WorkflowID);
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


            var workflows = _appDbContext.Workflows.ToList();
            foreach (var workflow in workflows)
            {
                _logger.LogInformation(workflow.WorkflowID.ToString());
            }
        }
    }
}
