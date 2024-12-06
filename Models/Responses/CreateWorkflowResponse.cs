namespace learning_asp_core.Models.Responses
{
    public class CreateWorkflowResponse
    {
        public int Id { get; set; }
        public int Rev { get; set; }
        public Fields Fields { get; set; }
        public List<Relation> Relations { get; set; }
        public Links _Links { get; set; }
        public string Url { get; set; }
    }

    public class Fields
    {
        public string SystemAreaPath { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public User SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public User SystemChangedBy { get; set; }
        public int SystemCommentCount { get; set; }
        public string SystemTitle { get; set; }
        public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
        public bool CustomCreateArtTask { get; set; }
        public bool CustomCreateConversionTask { get; set; }
        public bool CustomCreateDigitizationTask { get; set; }
        public string SystemDescription { get; set; }
        public int SystemParent { get; set; }
    }

    public class User
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public Links _Links { get; set; }
        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string ImageUrl { get; set; }
        public string Descriptor { get; set; }
    }

    public class Relation
    {
        public string Rel { get; set; }
        public string Url { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class Attributes
    {
        public bool IsLocked { get; set; }
        public string Name { get; set; }
    }

    public class Links
    {
        public Self Self { get; set; }
        public WorkItemUpdates WorkItemUpdates { get; set; }
        public WorkItemRevisions WorkItemRevisions { get; set; }
        public WorkItemComments WorkItemComments { get; set; }
        public Html Html { get; set; }
        public WorkItemType WorkItemType { get; set; }
        public Fields Fields { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class WorkItemUpdates
    {
        public string Href { get; set; }
    }

    public class WorkItemRevisions
    {
        public string Href { get; set; }
    }

    public class WorkItemComments
    {
        public string Href { get; set; }
    }

    public class Html
    {
        public string Href { get; set; }
    }

    public class WorkItemType
    {
        public string Href { get; set; }
    }

}
