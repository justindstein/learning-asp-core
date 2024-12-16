using learning_asp_core.Models.Entity;
using System.Text.Json;

namespace learning_asp_core.Models.Requests.Outbound
{
    public class OrderCompletedRequest
    {
        private readonly string _orderId;

        public OrderCompletedRequest(string orderId)
        {
            _orderId = orderId;
        }

        public OrderCompletedRequest(Workflow workflow)
        {
            try
            {
                Dictionary<string, string> data = JsonSerializer.Deserialize<Dictionary<string, string>>(workflow.Data);
                _orderId = data["OrderId"];
            }
            catch (Exception e)
            {
                throw new Exception("OrderCompletedRequest.OrderCompletedRequest failed. @{e}");
            }
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
