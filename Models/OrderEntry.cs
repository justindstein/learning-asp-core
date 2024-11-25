using learning_asp_core.Models.Enums;

namespace learning_asp_core.Models
{
    public class OrderEntry
    {
        public string? ProductUrl { get; set; }

        public string? ProductName { get; set; }

        public string? Color { get; set; }

        public int Quantity { get; set; }

        public ApprovalType ApprovalType { get; set; } 
    }
}