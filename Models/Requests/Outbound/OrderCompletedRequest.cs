namespace learning_asp_core.Models.Requests.Outbound
{
    public class OrderCompletedRequest
    {
        private readonly string _orderId;

        public OrderCompletedRequest(string orderId)
        {
            _orderId = orderId;
        }

        public string ToRequestBody()
        {
            return $@"
            [
                {{""orderId"":""{_orderId}""}}
            ]";
        }
    }
}
