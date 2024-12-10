using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Utils;

namespace learning_asp_core.Models.Requests.Inbound
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public Customer Customer { get; set; }

        public OpenWorkflowRequest() { }

        public (Workflow workflow, CreateWorkItemRequest createWorkItemRequest) ToOrderTuple()
        {
            return (
                new Workflow(WorkItemType.Order.GetDescription(), new HashSet<string> { "OrderId: " + Order.OrderId, "CustomerName: " + Customer.CustomerName, "Priority: " + Order.Priority.GetDescription(), "SubmitDate: " + Order.SubmitDate, "ProductionDate: " + Order.SubmitDate, "BssDate: " + Order.BestStartShipDate })
                , new CreateOrderWorkItemRequest(Customer.CustomerName, Order.OrderId, "some description", Order.Priority, Order.SubmitDate, Order.ProductionDate, Order.BestStartShipDate, Order.OrderRef)
            );
        }

        public HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)> ToSuborderTuple(string parentRef)
        {
            HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)> suborderTuples = new HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)>();
            foreach (SubOrder s in Order.SubOrders)
            {
                suborderTuples.Add((
                    new Workflow(WorkItemType.Suborder.GetDescription(), new HashSet<string> { "OrderId: " + Order.OrderId, "CustomerName: " + Customer.CustomerName, "Priority: " + Order.Priority.GetDescription(), "SubmitDate: " + Order.SubmitDate, "ProductionDate: " + Order.SubmitDate, "BssDate: " + Order.BestStartShipDate })
                    , new CreateSuborderWorkItemRequest(Customer.CustomerName, Order.OrderId, "description", parentRef)
                ));
            }
            return suborderTuples;
        }
    }
}
