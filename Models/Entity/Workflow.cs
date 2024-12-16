using learning_asp_core.Models.Requests.Outbound;
using System.Text.Json;

namespace learning_asp_core.Models.Entity
{
    public class Workflow
    {
        public int WorkflowId { get; set; }
        
        public int WorkItemId { get; set; }
        
        public string WorkItemUrl { get; set; }
        
        public string WorkItemType { get; set; }

        public bool IsClosed { get; set; }
        
        public string Data { get; set; }
      
        public Workflow() { }

        public Workflow(string workItemType, HashSet<string> data)
        {
            WorkItemType = workItemType;
            Data = JsonSerializer.Serialize(data);
        }

        public void Update(int workItemId, string workItemUrl)
        {
            WorkItemId = workItemId;
            WorkItemUrl = workItemUrl;
        }

        public void Complete()
        {
            IsClosed = true;
        }
    }
}
