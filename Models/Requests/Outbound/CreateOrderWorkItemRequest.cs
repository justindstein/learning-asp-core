using learning_asp_core.Models.Enums;
using learning_asp_core.Utils.Extensions;

namespace learning_asp_core.Models.Requests.Outbound
{
    public class CreateOrderWorkItemRequest : CreateWorkItemRequest
    {
        private readonly PriorityType _priority;
        private readonly DateTime _submitDate;
        private readonly DateTime _productionDate;
        private readonly DateTime _bssDate;
        private readonly string _orderRef;

        public CreateOrderWorkItemRequest(string customerName, string orderId, string description, PriorityType priority, DateTime submitDate, DateTime productionDate, DateTime bssDate, string orderRef) 
            : base(customerName, orderId, description)
        {
            _priority = priority;
            _submitDate = submitDate;
            _productionDate = productionDate;
            _bssDate = bssDate;
            _orderRef = orderRef;
        }

        public override string ToRequestBody()
        {
            return $@"
            [
                {{'op':'add','path':'/fields/System.Title','value':'{base.CustomerName} - {base.OrderId}'}},
                {{'op':'add','path':'/fields/System.Description','value':'{base.Description}'}},
                {{'op':'add','path':'/fields/Custom.Customer','value':'{base.CustomerName}'}},
                {{'op':'add','path':'/fields/Custom.WorkPriority','value':'{_priority.GetDescription()}'}},
                {{'op':'add','path':'/fields/Custom.SubmitDate','value':'{_submitDate}'}},
                {{'op':'add','path':'/fields/Custom.ProductionDate','value':'{_productionDate}'}},
                {{'op':'add','path':'/fields/Custom.BSS','value':'{_bssDate}'}},
                {{'op':'add','path':'/fields/Custom.OrderReference','value':'{_orderRef}'}} 
            ]";
        }
    }
}
