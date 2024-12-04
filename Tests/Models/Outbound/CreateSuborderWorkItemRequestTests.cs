using learning_asp_core.Models.Enums;
using learning_asp_core.Models.Requests.Outbound;
using Xunit;

namespace learning_asp_core.Tests.Models.Outbound
{
    public class CreateSuborderWorkItemRequestTests
    {
        [Fact]
        public void Test1_ToRequestBody()
        {
            // Arange
            string customerName = "testCustomerName";
            string orderId = "testOrderId";
            string parentRef = "testParentRef";
            CreateSuborderWorkItemRequest createSuborderWorkItemRequest = new CreateSuborderWorkItemRequest(customerName, orderId, parentRef);
            // string customerName, string orderId, string parentRef

            // Act
            String expected = $@"
            [
                {{'op':'add','path':'/fields/System.Title','from':null,'value':'{customerName} - {orderId}'}},
                {{'op':'add','path':'/relations/-','value':{{'rel':'System.LinkTypes.Hierarchy-Reverse','url':'{parentRef}'}}}},
                {{'op':'add','path':'/fields/System.Description','from':null,'value':'<table style='width:100%;text-align:center;font-size:8pt;border:1px solid black;border-collapse:collapse'><caption><strong>Logo</strong></caption><tr style='border:1px solid black'><th style='width:20%;border:1px solid black'>Image</th><th style='width:30%;border:1px solid black'>Modification Notes</th><th style='width:20%;border:1px solid black'>Logo Type</th><th style='width:20%;border:1px solid black'>Logo Placement</th></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/emb/thumbnails/FIRSTTEESC-EMB-tn.PNG' alt='Image 1' style='max-width:300px;max-height:600px'></a></td><td style='word-wrap:break-word;border:1px solid black'>N/A</td><td style='border:1px solid black'>Type 1</td><td style='border:1px solid black'>Front</td></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/template/thumbnails/IDVIN128.gif' alt='Image 2' style='max-width:300px;max-height:600px'></a></td><td style='word-wrap:break-word;border:1px solid black'>top line est '1999' middle line 'first tee' lower case (their custom font) bottom line 'greater austin' lower case text. use the custom icon between est and 1999</td><td style='border:1px solid black'>Type 2</td><td style='border:1px solid black'>Back</td></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/template/thumbnails/IDAPP061.gif' alt='Image 3' style='max-width:300px;max-height:600px'></a></td><td style='word-wrap:break-word;border:1px solid black'>3D/BOUNCE: 3D BOUNCE NOTES raised embroidery for top line of text</td><td style='border:1px solid black'>Type 3</td><td style='border:1px solid black'>Sleeve</td></tr></table><br><br><table style='width:100%;text-align:center;font-size:8pt;border:1px solid black;border-collapse:collapse'><caption><strong>Products</strong></caption><tr style='border:1px solid black'><th style='width:15%;border:1px solid black'>Image</th><th style='width:20%;border:1px solid black'>Approval</th><th style='width:15%;border:1px solid black'>Color</th><th style='width:10%;border:1px solid black'>Quantity</th></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/items/1-A/C18HLM-01BG.GIF' alt='Image 1' style='max-width:300px;max-height:600px'></a></td><td style='border:1px solid black'>PA</td><td style='border:1px solid black'>Blue</td><td style='border:1px solid black'>50</td></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/items/1-A/C18HLM-04GT.GIF' alt='Image 2' style='max-width:300px;max-height:600px'></a></td><td style='border:1px solid black'>None</td><td style='border:1px solid black'>Green</td><td style='border:1px solid black'>100</td></tr><tr style='border:1px solid black'><td style='border:1px solid black'><a href='https://www.google.com'><img src='https://images.aheadorder.com/aheadonline/images/items/1-A/C18HLM-30CW.GIF' alt='Image 3' style='max-width:300px;max-height:600px'></a></td><td style='border:1px solid black'>PA</td><td style='border:1px solid black'>Red</td><td style='border:1px solid black'>75</td></tr></table>'}}]
            ]";
            String actual = createSuborderWorkItemRequest.ToRequestBody();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
