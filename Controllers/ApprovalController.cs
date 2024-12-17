using learning_asp_core.Models.Requests.Inbound;
using learning_asp_core.Services;
using Microsoft.AspNetCore.Mvc;

namespace learning_asp_core.Controllers
{
    [Route("api/approvals")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly ILogger<ApprovalController> _logger;

        private readonly ApprovalService _approvalService;

        public ApprovalController(ILogger<ApprovalController> logger, ApprovalService approvalSevice)
        {
            _logger = logger;
            _approvalService = approvalSevice;
        }

        [HttpPost("create")]
        public JsonResult CreateApproval()
        {
            return new JsonResult(Ok());
        }

        [HttpPost("update")]
        public JsonResult UpdateApproval()
        {
            return new JsonResult(Ok());
        }

        //[HttpPost("delete")]
        //public JsonResult DeleteApproval(CloseWorkflowRequest closeWorkflowRequest)
        //{
        //    return new JsonResult(Ok());
        //}

        [HttpPost("create/azure")]
        public JsonResult AzureCreateApproval()
        {
            // workitemid
            // [
            // {}
            // {}
            // {}
            // {}
            // ]
            return new JsonResult(Ok());
        }

        [HttpPost("update/azure")]
        public JsonResult AzureUpdateApproval()
        {
            return new JsonResult(Ok());
        }
    }
}
