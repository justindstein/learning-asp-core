namespace learning_asp_core.Models.Requests
{
    public class StartOrderWorkflowRequest
    {
        public int WorkflowId { get; set; }

        public Order Order { get; set; }

        public Customer? Customer { get; set; }

        public StartOrderWorkflowRequest() { }

    }
}
