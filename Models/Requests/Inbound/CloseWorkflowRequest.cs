using learning_asp_core.Models.Requests.Inbound.Azure;

namespace learning_asp_core.Models.Requests.Inbound
{
    public class CloseWorkflowRequest
    {
        public int WorkItemId { get; set; }

        public CloseWorkflowRequest(int workItemId)
        {
            WorkItemId = workItemId;
        }
    }
}
