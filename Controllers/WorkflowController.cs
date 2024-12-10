using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Services;
using learning_asp_core.Utils.Extensions;
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
            _logger.LogInformation("WorkflowController.OpenWorkflow [OpenWorkflowRequest: {@OpenWorkflowRequest}]", openWorkflowRequest.Dump());

            try 
            {
                _workflowService.OpenWorkflow(openWorkflowRequest);
                return new JsonResult(Ok());

            } catch(Exception e)
            {
                _logger.LogError("WorkflowController.OpenWorkflow [Exception: {@e}]", e.Dump());
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("close")]
        public JsonResult CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            _logger.LogInformation("WorkflowController.CloseWorkflow [CloseWorkflowRequest: {@CloseWorkflowRequest}]", closeWorkflowRequest.Dump());

            try
            {
                _workflowService.CloseWorkflow(closeWorkflowRequest);
                return new JsonResult(Ok());
                            }
            catch (Exception e)
            {
                _logger.LogError("WorkflowController.CloseWorkflow [Exception: {@e}]", e.Dump());
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
