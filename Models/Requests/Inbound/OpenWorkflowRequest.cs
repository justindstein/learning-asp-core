using learning_asp_core.Models.Entity;
using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using learning_asp_core.Utils.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace learning_asp_core.Models.Requests.Inbound
{
    public class OpenWorkflowRequest
    {
        public Order Order { get; set; }

        public Customer Customer { get; set; }

        public OpenWorkflowRequest() { }

        public (Workflow workflow, CreateWorkItemRequest createWorkItemRequest) ToOrderTuple()
        {
            return (
                new Workflow(
                    WorkItemType.Order.GetDescription()
                    , new HashSet<string> { 
                        "OrderId: " + Order.OrderId
                        , "CustomerName: " + Customer.CustomerName
                        , "Priority: " + Order.Priority.GetDescription()
                        , "SubmitDate: " + Order.SubmitDate
                        , "ProductionDate: " + Order.SubmitDate
                        , "BssDate: " + Order.BestStartShipDate 
                    }
                )
                , new CreateOrderWorkItemRequest(Customer.CustomerName, Order.OrderId, Order.Priority, Order.SubmitDate, Order.ProductionDate, Order.BestStartShipDate, Order.OrderRef)
            );
        }

        public HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)> ToSuborderTuple(string parentRef)
        {
            HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)> suborderTuples = new HashSet<(Workflow workflow, CreateWorkItemRequest createWorkItemRequest)>();
            foreach (Suborder suborder in Order.Suborders)
            {
                suborderTuples.Add((
                    new Workflow(WorkItemType.Suborder.GetDescription(), new HashSet<string> { "OrderId: " + Order.OrderId, "CustomerName: " + Customer.CustomerName, "Priority: " + Order.Priority.GetDescription(), "SubmitDate: " + Order.SubmitDate, "ProductionDate: " + Order.SubmitDate, "BssDate: " + Order.BestStartShipDate })
                    , new CreateSuborderWorkItemRequest(Customer.CustomerName, Order.OrderId, generateSuborderDescription(suborder), parentRef)
                ));
            }
            return suborderTuples;
        }

        private string generateSuborderDescription(Suborder suborder)
        {
            string decorationTable = "";
            if (!suborder.Decorations.IsNullOrEmpty())
            {
                StringBuilder decorationRows = new StringBuilder();
                foreach (Decoration decoration in suborder.Decorations)
                {
                    decorationRows.AppendLine($@"
                        <tr style='border:1px solid black'>
                            <td style='border:1px solid black'><a href='{decoration.LogoUrl}'><img src='{decoration.ImageUrl}' style='max-width:300px;max-height:600px'></a></td>
                            <td style='word-wrap:break-word;border:1px solid black'>{decoration.Notes}</td>
                            <td style='border:1px solid black'>{decoration.LogoType.GetDescription()}</td>
                            <td style='border:1px solid black'>{decoration.LogoPlacement.GetDescription()}</td>
                        </tr>
                    ");
                }

                decorationTable = $@"
                <table style='width:100%;text-align:center;font-size:8pt;border:1px solid black;border-collapse:collapse'>
                    <caption><strong>Logos</strong></caption>
                    <tr style='border:1px solid black'>
                        <th style='width:20%;border:1px solid black'>Image</th>
                        <th style='width:30%;border:1px solid black'>Modification Notes</th>
                        <th style='width:20%;border:1px solid black'>Logo Type</th>
                        <th style='width:20%;border:1px solid black'>Logo Placement</th>
                    </tr>
                   {decorationRows}
                </table>
                ";
            }

            string productTable = "";
            if (!suborder.OrderEntries.IsNullOrEmpty())
            {
                StringBuilder productRows = new StringBuilder();
                foreach (OrderEntry orderEntry in suborder.OrderEntries)
                {
                    productRows.AppendLine($@"
                        <tr style='border:1px solid black'>
                            <td style='border:1px solid black'><a href='{orderEntry.ProductUrl}'><img src='{orderEntry.ImageUrl}' style='max-width:300px;max-height:600px'></a></td>
                            <td style='word-wrap:break-word;border:1px solid black'>{orderEntry.ApprovalType.GetDescription()}</td>
                            <td style='border:1px solid black'>{orderEntry.Color}</td>
                            <td style='border:1px solid black'>{orderEntry.Quantity}</td>
                        </tr>
                    ");
                }

                productTable = $@"
                    <table style='width:100%;text-align:center;font-size:8pt;border:1px solid black;border-collapse:collapse'>
                        <caption><strong>Products</strong></caption>
                        <tr style='border:1px solid black'>
                            <th style='width:15%;border:1px solid black'>Image</th>
                            <th style='width:20%;border:1px solid black'>Approval</th>
                            <th style='width:15%;border:1px solid black'>Color</th>
                            <th style='width:10%;border:1px solid black'>Quantity</th>
                        </tr>
                        {productRows}
                    </table>
                ";
            }

            return $@"
                {decorationTable}
                {productTable}
            ";
        }
    }
}
