using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Utils;
using System.Data.SqlClient;
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

        [Fact]
        public void Test2_dbconection()
        {

            string connectionString = "Server=localhost\\MSSQLLocalDB;Database=aws;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");

                    // Example query
                    string query = "SELECT * FROM workflow";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }


        [Fact]
        public void Test3_dbconection()
        {
            Console.WriteLine("");
        }
    }
}
