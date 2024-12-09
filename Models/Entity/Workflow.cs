namespace learning_asp_core.Models.Entity
{
        public class Workflow
        {
            public int WorkflowID { get; set; }
            public int WorkItemID { get; set; }
            public string WorkItemUrl { get; set; }
            public string WorkItemType { get; set; }
            public bool IsClosed { get; set; }
            public string Data { get; set; }
        }
}
