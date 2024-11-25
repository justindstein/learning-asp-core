using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using learning_asp_core.Data;
using learning_asp_core.Models.Requests;
using learning_asp_core.Services;

namespace learning_asp_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderWorkflow : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly WorkflowService _orderWorkflowService;

        public OrderWorkflow(ApiContext context, WorkflowService orderWorkflowService)
        {
            _context = context;
            _orderWorkflowService = orderWorkflowService;
        }

        [HttpPost]
        public JsonResult CreateOpenWorkflowRequest(OpenWorkflowRequest startOrderWorkflowRequest)
        {
            return new JsonResult(Ok());
        }

        public JsonResult CreateCloseWorkflowRequest(CloseWorkflowRequest startOrderWorkflowRequest)
        {
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
