using learning_asp_core.Models.Enums;
using learning_asp_core.Utils;

namespace learning_asp_core.Models.Requests.Outbound
{
    public class CreateOrderWorkItemRequest
    {
        private readonly string _customerName;
        private readonly int _orderNumber;
        private readonly string _description;
        private readonly PriorityType _priority;
        private readonly DateTime _submitDate;
        private readonly DateTime _productionDate;
        private readonly DateTime _bssDate;
        private readonly string _orderRef;

        public CreateOrderWorkItemRequest(string customerName, int orderNumber, string description, PriorityType priority, DateTime submitDate, DateTime productionDate, DateTime bssDate, string orderRef)
        {
            _customerName = customerName;
            _orderNumber = orderNumber;
            _description = description;
            _priority = priority;
            _submitDate = submitDate;
            _productionDate = productionDate;
            _bssDate = bssDate;
            _orderRef = orderRef;
        }

        public string toRequestBody()
        {
            return $@"
            [
                {{'op':'add','path':'/fields/System.Title','value':'{_customerName} - {_orderNumber}'}},
                {{'op':'add','path':'/fields/System.Description','value':'{_description}'}},
                {{'op':'add','path':'/fields/Custom.Customer','value':'{_customerName}'}},
                {{'op':'add','path':'/fields/Custom.WorkPriority','value':'{_priority.GetDescription()}'}},
                {{'op':'add','path':'/fields/Custom.SubmitDate','value':'{_submitDate}'}},
                {{'op':'add','path':'/fields/Custom.ProductionDate','value':'{_productionDate}'}},
                {{'op':'add','path':'/fields/Custom.BSS','value':'{_bssDate}'}},
                {{'op':'add','path':'/fields/Custom.OrderReference','value':'{_orderRef}'}} 
            ]";
        }
    }
}
