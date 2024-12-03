namespace learning_asp_core.Models.Requests.Inbound
{
    public class CloseWorkflowRequest
    {
        public int WorkflowId { get; set; }

        public Order Order { get; set; }

        public Customer Customer { get; set; }

        public CloseWorkflowRequest()
        {
            Order = new Order();
            Customer = new Customer();
        }

    }
}
