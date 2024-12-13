namespace learning_asp_core.Models.Requests.Outbound
{

    public class CreateSuborderWorkItemRequest : CreateWorkItemRequest
    {

        private readonly string _parentRef;

        public CreateSuborderWorkItemRequest(string customerName, string orderId, string description, string parentRef)
            : base(customerName, orderId, description)
        {
            _parentRef = parentRef;
        }

        public override string ToRequestBody()
        {
            return $@"
            [
                {{'op':'add','path':'/fields/System.Title','from':null,'value':'{base.CustomerName} - {base.OrderId}'}},
                {{'op':'add','path':'/relations/-','value':{{'rel':'System.LinkTypes.Hierarchy-Reverse','url':'{_parentRef}'}}}},
                {{'op':'add','path':'/fields/System.Description','from':null,'value':""{base.Description}""}}
            ]";
            // {{'op':'add','path':'/fields/System.Description','from':null,'value':'{base.Description}'}}
        }
    }
}
