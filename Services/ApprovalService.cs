using learning_asp_core.Data;
using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Models.Responses;
using learning_asp_core.Utils.Extensions;

namespace learning_asp_core.Services
{
    public class ApprovalService
    {
        // TODOS:
        // Add callback url to create workflow request
        // Call this callback in the close workflow request
        // when created CR workitem, call google api

        private readonly ILogger<ApprovalService> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly AzureService _azureService;
        private readonly AheadService _aheadService;
        private readonly GoogleService _googleService;

        public ApprovalService(ILogger<ApprovalService> logger, AppDbContext appDbContext, AzureService azureService, AheadService aheadService, GoogleService googleService)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _azureService = azureService;
            _aheadService = aheadService;
            _googleService = googleService;
        }

        public void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            _logger.LogInformation("WorkflowService.OpenWorkflow [openWorkflowRequest: {@openWorkflowRequest}]", openWorkflowRequest);
            (Workflow workflow, CreateWorkItemRequest createWorkItemRequest) orderTuple = openWorkflowRequest.ToOrderTuple();
            Workflow orderWorkflow = insertWorkflow(orderTuple.workflow);

            CreateWorkflowResponse response = _azureService.CreateWorkItem(orderTuple.createWorkItemRequest);
            orderWorkflow.Update(response.Id, response.Url);
            updateWorkflow(orderWorkflow);

            foreach ((Workflow workflow, CreateWorkItemRequest createWorkItemRequest) suborderTuple in openWorkflowRequest.ToSuborderTuple(response.Url))
            {
                Workflow suborderWorkflow = insertWorkflow(suborderTuple.workflow);

                response = _azureService.CreateWorkItem(suborderTuple.createWorkItemRequest);
                suborderWorkflow.Update(response.Id, response.Url);
                updateWorkflow(suborderWorkflow);
            }
        }

        public void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            _logger.LogInformation("WorkflowService.CloseWorkflow [closeWorkflowRequest: {@closeWorkflowRequest}]", closeWorkflowRequest);
            completeWorkflow(closeWorkflowRequest.WorkItemId);
            // update endpoint  
        }

        private Workflow insertWorkflow(Workflow workflow)
        {
            _logger.LogInformation("WorkflowService.insertWorkflow [workflow: {@workflow}]", workflow);
            _appDbContext.Workflows.Add(workflow);
            _appDbContext.SaveChanges();
            _logger.LogInformation("Record inserted: " + workflow.WorkflowId);
            return workflow;
        }

        private bool completeWorkflow(int workflowId)
        {
            _logger.LogInformation("WorkflowService.completeWorkflow [workflowId: {@workflowId}]", workflowId);
            Workflow? workflow = _appDbContext.Workflows.FirstOrDefault(w => w.IsClosed == false && w.WorkItemType == "Order" && w.WorkItemId == workflowId);

            if (workflow != null)
            {
                workflow.Complete();
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogDebug("WorkflowService.completeWorkflow Record completed [workflowId: {@workflowId}]", workflowId);
            }
            else
            {
                throw new Exception("WorkflowService.completeWorkflow Record does not exist [workflowId: {@workflowId}]");
            }

            return (workflow != null);
        }

        private bool updateWorkflow(Workflow workflow)
        {
            _logger.LogInformation("WorkflowService.updateWorkflow [workflow: {@workflow}]", workflow);
            bool exists = _appDbContext.Workflows.Any(w => w.WorkflowId == workflow.WorkflowId);
            if (exists)
            {
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogDebug("WorkflowService.updateWorkflow Record updated [WorkflowId: {@WorkflowId}]", workflow.WorkflowId);
            }
            else
            {
                _logger.LogError("WorkflowService.updateWorkflow Record does not exist [WorkflowID: {@WorkflowID}]", workflow.WorkflowId);
            }
            return exists;
        }

        private bool completeWorkflow(Workflow workflow)
        {
            _logger.LogInformation("WorkflowService.completeWorkflow [Workflow: {@workflow}]", workflow.Dump());
            bool exists = _appDbContext.Workflows.Any(w => w.WorkItemId == workflow.WorkItemId && w.IsClosed == false);
            if (exists)
            {
                workflow.Complete();
                _appDbContext.Workflows.Update(workflow);
                _appDbContext.SaveChanges();
                _logger.LogDebug("WorkflowService.completeWorkflow Record updated [workflow: {@workflow}]", workflow);
            }
            else
            {
                _logger.LogError("WorkflowService.updateWorkflow Record does not exist [WorkflowId: {@WorkflowId}]", workflow.WorkflowId);
            }
            return exists;
        }
    }
}
