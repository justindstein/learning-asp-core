namespace learning_asp_core.Models.Requests
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public OpenWorkflowRequest() {
            Order = new Order();
        }
    }
}
