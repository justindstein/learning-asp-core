using learning_asp_core.Models.Requests.Outbound;

namespace learning_asp_core.Models.Requests.Inbound
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public Customer Customer { get; set; }

        public OpenWorkflowRequest() { }

        public CreateOrderWorkItemRequest ToCreateOrderWorkItemRequest()
        {
            return new CreateOrderWorkItemRequest(Customer.CustomerName, Order.OrderId, "some description", Order.Priority, Order.SubmitDate, Order.ProductionDate, Order.BestStartShipDate, Order.OrderRef);
        }

        public HashSet<CreateSuborderWorkItemRequest> ToCreateSuborderWorkItemRequests(string parentRef)
        {
            HashSet<CreateSuborderWorkItemRequest> createSuborderWorkItemRequests = new HashSet<CreateSuborderWorkItemRequest>();
            foreach (SubOrder s in Order.SubOrders)
            {
                createSuborderWorkItemRequests.Add(new CreateSuborderWorkItemRequest(Customer.CustomerName, Order.OrderId, parentRef)); //string customerName, string orderNumber, string parentRef
            }
            return createSuborderWorkItemRequests;
        }
    }
}
