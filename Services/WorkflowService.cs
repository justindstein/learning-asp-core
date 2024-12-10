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

        public void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            (Workflow workflow, CreateWorkItemRequest createOrderWorkItemRequest) orderTuple = openWorkflowRequest.ToOrderTuple();
            Workflow orderWorkflow = insertWorkflow(orderTuple.workflow);

            CreateWorkflowResponse response = _azureService.CreateOrder(orderTuple.createOrderWorkItemRequest);
            orderWorkflow = insertWorkflow(orderWorkflow);
            updateWorkflow(orderWorkflow);

            foreach ((Workflow workflow, CreateSuborderWorkItemRequest createSuborderWorkItemRequest) suborderTuple in openWorkflowRequest.ToSuborderTuple(response.Url))
            {
                Workflow suborderWorkflow = insertWorkflow(suborderTuple.workflow);
                suborderWorkflow = insertWorkflow(suborderWorkflow);

                response = _azureService.CreateSuborder(suborderTuple.createSuborderWorkItemRequest);
                suborderWorkflow.Update(response.Id, response.Url);
                updateWorkflow(suborderWorkflow);
            }
        }

        public void UpdateWorkflow()
        {
            // Check if order
            // Check 
        }

        public void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
   
        }


        private Workflow insertWorkflow(Workflow workflow)
        {
            _appDbContext.Workflows.Add(workflow);
            _appDbContext.SaveChanges();
            _logger.LogInformation("Record inserted: " + workflow.WorkflowID);
            return workflow;
        }

        private bool updateWorkflow(Workflow workflow)
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
            return exists;
        }

        private bool CompleteWorkflow(Workflow workflow)
        {
            bool exists = _appDbContext.Workflows.Any(w => w.WorkItemID == workflow.WorkItemID && w.IsClosed == false);
            if (exists)
            {
                workflow.Complete();
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogInformation("Record updated " + workflow.WorkflowID);
            }
            else
            {
                _logger.LogInformation("Record does not exist " + workflow.WorkflowID);
            }
            return exists;
        }
    }
}
