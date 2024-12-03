using learning_asp_core.Models.Requests;
using learning_asp_core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace learning_asp_core.Controllers
{
    [Route("api/workflows")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        ILogger<WorkflowController> _logger;

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

            _workflowService.OpenWorkflow(openWorkflowRequest);
            return new JsonResult(Ok());
        }

        [HttpPost("close")]
        public JsonResult CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            _logger.LogInformation("Closing workflow with parameters: {@CloseWorkflowRequest}", closeWorkflowRequest);

            _workflowService.CloseWorkflow(closeWorkflowRequest);
            return new JsonResult(Ok());
        }

        //public JsonResult CreateStartOrderWorkflowRequest(StartOrderWorkflowRequest startOrderWorkflowRequest)
        //{
        //if(startOrderWorkflowRequest.Order.OrderId == 0)
        //{
        //    _context.StartOrderWorkflowRequests.Add(startOrderWorkflowRequest);
        //} else
        //{
        //    var bookingInDb = _context.StartOrderWorkflowRequests.Find(startOrderWorkflowRequest.Order.OrderId);
        //    if (bookingInDb == null)
        //    {
        //     return new JsonResult(NotFound());   
        //    }

        //    bookingInDb = startOrderWorkflowRequest;
        //}

        //_context.SaveChanges();
        //return new JsonResult(Ok(startOrderWorkflowRequest));
        //}
    }
}
