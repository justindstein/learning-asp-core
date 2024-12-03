using learning_asp_core.Models.Requests;
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
            _logger.LogInformation("Opening workflow with parameters: {@OpenWorkflowRequest}", openWorkflowRequest);

            try 
            {
                // write to db
                // organize requests
                // issue requests
                // respond with epic link maybe

                _workflowService.OpenWorkflow(openWorkflowRequest);
                return new JsonResult(Ok());

            } catch(Exception e)
            {
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("close")]
        public JsonResult CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            _logger.LogInformation("Closing workflow with parameters: {@CloseWorkflowRequest}", closeWorkflowRequest);

            try
            {
                // if order, verify all suborders are closed?
                // update db
                // notify callback url

                _workflowService.CloseWorkflow(closeWorkflowRequest);
                return new JsonResult(Ok());

            }
            catch (Exception e)
            {
                return new JsonResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
