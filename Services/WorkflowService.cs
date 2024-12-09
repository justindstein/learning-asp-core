using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Polly;
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
            Workflow workflow = new Workflow("Order", new HashSet<string> { "OrderId: 1001", "CustomerName: John Doe", "Priority: Event", "SubmitDate: 1/1/2024" });
            insertWorkflow(workflow);

            CreateWorkflowResponse response = _azureService.CreateOrder(openWorkflowRequest.ToCreateOrderWorkItemRequest());
            workflow.Update(response.Id, response.Url);
            updateWorkflow(workflow);

            foreach (CreateSuborderWorkItemRequest createSuborderWorkItemRequest in openWorkflowRequest.ToCreateSuborderWorkItemRequests(response.Url))
            {
                workflow = new Workflow("Suborder", new HashSet<string> { "OrderId: 2001", "CustomerName: Jane Doe", "Priority: Low", "SubmitDate: 1/1/2025" });
                insertWorkflow(workflow);

                response = _azureService.CreateSuborder(createSuborderWorkItemRequest);
                workflow.Update(response.Id, response.Url);
                updateWorkflow(workflow);
            }
        }

        private void insertWorkflow(Workflow workflow)
        {
            _appDbContext.Workflows.Add(workflow);
            _appDbContext.SaveChanges();
            Console.WriteLine("Record inserted.");
        }

        private void updateWorkflow(Workflow workflow)
        {
            bool exists = _appDbContext.Workflows.Any(w => w.WorkItemID == workflow.WorkItemID);
            if (exists)
            {
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChangesAsync();
                Console.WriteLine("Record updated.");
            }
            else
            {
                Console.WriteLine("Record does not exist.");
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
