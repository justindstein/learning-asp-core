using learning_asp_core.Models.Requests;

namespace learning_asp_core.Services
{
    public class WorkflowService
    {
        public void OpenWorkflow(OpenWorkflowRequest openWorkflowRequest)
        {
            // convert OpenWorkflowRequest to API request object for azure devops
            // send message to devops
        }

        public void CloseWorkflow(CloseWorkflowRequest closeWorkflowRequest)
        {
            // convert closeWorkflowRequest to object that needs to be run against db
            // update db
            // message ASP

        }
    }
}
