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
            // Usage
            DecorTech decorTech = DecorTech.THREE_D_BOUNCE_RAISED_EMBROIDERY;
            string description = decorTech.GetDescription();
            Console.WriteLine(description); // Outputs: "3D/Bounce (Raised Embroidery)"







            return new CreateOrderWorkItemRequest(Customer.CustomerName, Order.OrderId, "some description", Order.Priority, string submitDate, string productionDate, string bssDate, string orderRef);
        }

        public HashSet<CreateSuborderWorkItemRequest> toCreateSuborderWorkItemRequests()
        {
            CreateSuborderWorkItemRequest foo = new CreateSuborderWorkItemRequest();
            return null;
        }
    }
}
