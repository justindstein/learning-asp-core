namespace learning_asp_core.Models.Requests.Outbound
{
    public abstract class CreateWorkItemRequest
    {
        protected readonly string CustomerName;
        protected readonly string OrderId;
        protected readonly string Description;

        public CreateWorkItemRequest(string customerName, string orderId, string description)
        {
            CustomerName = customerName;
            OrderId = orderId;
            Description = description;
        }

        public abstract string ToRequestBody();
    }
}
