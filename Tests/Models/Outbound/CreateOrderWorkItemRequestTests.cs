using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using Xunit;

namespace learning_asp_core.Tests.Models.Outbound
{
    public class CreateOrderWorkItemRequestTests
    {
        [Fact]
        public void Test1_ToRequestBody()
        {
            // Arange
            CreateOrderWorkItemRequest createOrderWorkItemRequest = new CreateOrderWorkItemRequest("customerName", "orderId", "description", PriorityType.High, new DateTime(2024, 12, 3), new DateTime(2024, 12, 3), new DateTime(2024, 12, 3), "orderRef");

            // Act
            String expected = "";
            String actual = createOrderWorkItemRequest.ToRequestBody();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
