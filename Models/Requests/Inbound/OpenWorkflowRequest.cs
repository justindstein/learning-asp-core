using learning_asp_core.Models.Requests.Outbound;

namespace learning_asp_core.Models.Requests.Inbound
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public Customer Customer { get; set; }

        public OpenWorkflowRequest()
        {
            Order = new Order();
        }

        public CreateOrderWorkItemRequest toCreateOrderWorkItemRequest()
        {
            return new CreateOrderWorkItemRequest(Customer.CustomerName, Order.OrderId, "some description", Order.Priority, Order.SubmitDate, Order.ProductionDate, Order.BestStartShipDate, Order.OrderRef);
        }

        public HashSet<CreateSuborderWorkItemRequest> toCreateSuborderWorkItemRequests()
        {
            HashSet<CreateSuborderWorkItemRequest> createSuborderWorkItemRequests = new HashSet<CreateSuborderWorkItemRequest>();
            foreach (SubOrder s in Order.SubOrders)
            {
                createSuborderWorkItemRequests.Add(new CreateSuborderWorkItemRequest());
            }

            return createSuborderWorkItemRequests;
        }
    }
}
