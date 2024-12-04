using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Utils;
using Xunit;

namespace learning_asp_core.Tests.Models.Outbound
{
    public class CreateOrderWorkItemRequestTests
    {
        [Fact]
        public void Test1_ToRequestBody()
        {
            // Arange
            string customerName = "testCustomerName";
            string orderId = "testOrderId";
            string description = "testDescription";
            PriorityType priority = PriorityType.High;
            DateTime submitDate = new DateTime(2024, 12, 3);
            DateTime productionDate = new DateTime(2024, 12, 3);
            DateTime bssDate = new DateTime(2024, 12, 3);
            string orderRef = "testOrderRef";
            CreateOrderWorkItemRequest createOrderWorkItemRequest = new CreateOrderWorkItemRequest(customerName, orderId, description, priority, submitDate, productionDate, bssDate, orderRef);

            // Act
            String expected = $@"
            [
                {{'op':'add','path':'/fields/System.Title','value':'{customerName} - {orderId}'}},
                {{'op':'add','path':'/fields/System.Description','value':'{description}'}},
                {{'op':'add','path':'/fields/Custom.Customer','value':'{customerName}'}},
                {{'op':'add','path':'/fields/Custom.WorkPriority','value':'{priority.GetDescription()}'}},
                {{'op':'add','path':'/fields/Custom.SubmitDate','value':'{submitDate}'}},
                {{'op':'add','path':'/fields/Custom.ProductionDate','value':'{productionDate}'}},
                {{'op':'add','path':'/fields/Custom.BSS','value':'{bssDate}'}},
                {{'op':'add','path':'/fields/Custom.OrderReference','value':'{orderRef}'}} 
            ]";
            String actual = createOrderWorkItemRequest.ToRequestBody();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
