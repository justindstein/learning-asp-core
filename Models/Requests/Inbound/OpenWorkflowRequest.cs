namespace learning_asp_core.Models.Requests
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public OpenWorkflowRequest() {
            Order = new Order();
        }

        public CreateOrderWorkItemRequest toCreateOrderWorkItemRequest()
        {
            return new CreateOrderWorkItemRequest();
        }

        public HashSet<CreateSuborderWorkItemRequest> toCreateSuborderWorkItemRequests()
        {
            CreateSuborderWorkItemRequest foo = new CreateSuborderWorkItemRequest();
            return null;
        }
    }
}
