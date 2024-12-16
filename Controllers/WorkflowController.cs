using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Services;
using Microsoft.AspNetCore.Mvc;

namespace learning_asp_core.Controllers
{
    [Route("api/workflows")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly ILogger<WorkflowController> _logger;

        private readonly WorkflowService _workflowService;

        public WorkflowController(ILogger<WorkflowController> logger, WorkflowService workflowService)
        {
            _logger = logger;
            _workflowService = workflowService;
        }

        [HttpPost("open")]
        public JsonResult OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            _logger.LogInformation("WorkflowController.OpenWorkflow [openWorkflowRequest: {@openWorkflowRequest}]", openWorkflowRequest);

            try 
            {
                _workflowService.OpenWorkflow(openWorkflowRequest);
                return new JsonResult(Ok());

            } catch(Exception e)
            {
                _logger.LogError("WorkflowController.OpenWorkflow [e: {@e}]", e);
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("close")]
        public JsonResult CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            _logger.LogInformation("WorkflowController.CloseWorkflow [closeWorkflowRequest: {@closeWorkflowRequest}]", closeWorkflowRequest);

            try
            {
                _workflowService.CloseWorkflow(closeWorkflowRequest);
                return new JsonResult(Ok());
                            }
            catch (Exception e)
            {
                _logger.LogError("WorkflowController.CloseWorkflow [e: {@e}]", e);
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
