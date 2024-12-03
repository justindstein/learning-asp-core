using learning_asp_core.Models.Enums;
using learning_asp_core.Utils;

namespace learning_asp_core.Models.Requests.Outbound
{
    public class CreateOrderWorkItemRequest
    {
        string RequestBody { get; set; }
        public CreateOrderWorkItemRequest(string customerName, string orderNumber, string description, PriorityType priority, DateTime submitDate, DateTime productionDate, DateTime bssDate, string orderRef)
        {
            //string priorityDescription = priority.GetDescription();

            RequestBody = $@"
            [
                {{'op':'add','path':'/fields/System.Title','value':'{customerName} - {orderNumber}'}},
                {{'op':'add','path':'/fields/System.Description','value':'{description}'}},
                {{'op':'add','path':'/fields/Custom.Customer','value':'{customerName}'}},
                {{'op':'add','path':'/fields/Custom.WorkPriority','value':'{priority.GetDescription()}'}},
                {{'op':'add','path':'/fields/Custom.SubmitDate','value':'{submitDate}'}},
                {{'op':'add','path':'/fields/Custom.ProductionDate','value':'{productionDate}'}},
                {{'op':'add','path':'/fields/Custom.BSS','value':'{bssDate}'}},
                {{'op':'add','path':'/fields/Custom.OrderReference','value':'{orderRef}'}} 
            ]";
        }
    }
}
