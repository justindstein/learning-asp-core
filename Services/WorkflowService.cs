using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
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
            // Create db entry for order and each suborder

            insertWorkflow(new Workflow("Order", new HashSet<string> { "OrderId: 1001", "CustomerName: John Doe", "Priority: Event", "SubmitDate: 1/1/2024" }));

            string parentRef = _azureService.CreateOrder(openWorkflowRequest.ToCreateOrderWorkItemRequest());

            // Update order db entry as created
            

            foreach (CreateSuborderWorkItemRequest createSuborderWorkItemRequest in openWorkflowRequest.ToCreateSuborderWorkItemRequests(parentRef))
            {
                insertWorkflow(new Workflow("Suborder", new HashSet<string> { "OrderId: 1001", "CustomerName: John Doe", "Priority: Event", "SubmitDate: 1/1/2024" }));
                _azureService.CreateSubOrder(createSuborderWorkItemRequest);

                

                // Update suborder db entry as created 
            }
        }

        private async void insertWorkflow(Workflow workflow)
        {
            _appDbContext.Workflows.Add(workflow);
            await _appDbContext.SaveChangesAsync();
            Console.WriteLine("Record inserted.");
        }

        private async void updateWorkflow(Workflow workflow)
        {
            bool exists = await _appDbContext.Workflows.AnyAsync(w => w.WorkItemID == workflow.WorkItemID);
            if (exists)
            {
                _appDbContext.Workflows.Update(workflow);
                await _appDbContext.SaveChangesAsync();
                Console.WriteLine("Record updated.");
            }
            else
            {
                Console.WriteLine("Record does not exist.");
            }





            Workflow? workflowToUpdate = await _appDbContext.Workflows.FirstOrDefaultAsync(w => w.WorkflowID == workflow.WorkflowID);
            if (workflowToUpdate != null)
            {
                workflowToUpdate.Update(workflow.WorkItemID, workflow.WorkItemUrl);


            }
            else
            {
                Console.WriteLine("Record not found.");
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
