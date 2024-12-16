namespace learning_asp_core.Models.Requests.Outbound
{
    public abstract class CreateWorkItemRequest
    {
        protected readonly string CustomerName;
        protected readonly string OrderId;

        public CreateWorkItemRequest(string customerName, string orderId)
        {
            CustomerName = customerName;
            OrderId = orderId;
        }
        public abstract string ToRequestBody();
    }
}
