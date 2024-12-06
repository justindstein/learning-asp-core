namespace learning_asp_core.Models.Requests.Outbound
{

    public class CreateSuborderWorkItemRequest
    {
        private readonly string _customerName;
        private readonly string _orderId;
        private readonly string _parentRef;

        public CreateSuborderWorkItemRequest(string customerName, string orderId, string parentRef)
        {
            _customerName = customerName;
            _orderId = orderId;
            _parentRef = parentRef;
        }

        public string ToRequestBody()
        {
            return $@"
            [
                {{'op':'add','path':'/fields/System.Title','from':null,'value':'{_customerName} - {_orderId}'}},
                {{'op':'add','path':'/relations/-','value':{{'rel':'System.LinkTypes.Hierarchy-Reverse','url':'{_parentRef}'}}}},
                {{'op':'add','path':'/fields/System.Description','from':null,'value':'test'}}
            ]";
        }
    }
}
