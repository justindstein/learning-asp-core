using learning_asp_core.Controllers;
using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;
using learning_asp_core.Utils;

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
            _logger.LogDebug("WorkflowService.OpenWorkflow [OpenWorkflowRequest: {@OpenWorkflowRequest}]", openWorkflowRequest.Dump());
            (Workflow workflow, CreateWorkItemRequest createWorkItemRequest) orderTuple = openWorkflowRequest.ToOrderTuple();
            Workflow orderWorkflow = insertWorkflow(orderTuple.workflow);

            CreateWorkflowResponse response = _azureService.CreateOrder(orderTuple.createWorkItemRequest);
            orderWorkflow.Update(response.Id, response.Url);
            updateWorkflow(orderWorkflow);

            _logger.LogDebug("foreach");
            foreach ((Workflow workflow, CreateWorkItemRequest createWorkItemRequest) suborderTuple in openWorkflowRequest.ToSuborderTuple(response.Url))
            {
                Workflow suborderWorkflow = insertWorkflow(suborderTuple.workflow);

                response = _azureService.CreateSuborder(suborderTuple.createWorkItemRequest);
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
            _logger.LogDebug("WorkflowService.insertWorkflow [Workflow: {@workflow}]", workflow.Dump());
            _appDbContext.Workflows.Add(workflow);
            _appDbContext.SaveChanges();
            _logger.LogInformation("Record inserted: " + workflow.WorkflowID);
            return workflow;
        }

        private bool updateWorkflow(Workflow workflow)
        {
            _logger.LogDebug("WorkflowService.updateWorkflow [Workflow: {@workflow}]", workflow.Dump());
            bool exists = _appDbContext.Workflows.Any(w => w.WorkflowID == workflow.WorkflowID);
            if (exists)
            {
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogDebug("WorkflowService.updateWorkflow Record updated [WorkflowID: {@WorkflowID}]", workflow.WorkflowID);
            }
            else
            {
                _logger.LogError("WorkflowService.updateWorkflow Record does not exist [WorkflowID: {@WorkflowID}]", workflow.WorkflowID);
            }
            return exists;
        }

        private bool completeWorkflow(Workflow workflow)
        {
            _logger.LogDebug("WorkflowService.completeWorkflow [Workflow: {@workflow}]", workflow.Dump());
            bool exists = _appDbContext.Workflows.Any(w => w.WorkItemID == workflow.WorkItemID && w.IsClosed == false);
            if (exists)
            {
                workflow.Complete();
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogDebug("WorkflowService.completeWorkflow Record updated [Workflow: {@workflow}]", workflow.Dump());
            }
            else
            {
                _logger.LogError("WorkflowService.updateWorkflow Record does not exist [WorkflowID: {@WorkflowID}]", workflow.WorkflowID);
            }
            return exists;
        }
    }
}
