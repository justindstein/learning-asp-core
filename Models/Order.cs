using learning_asp_core.Models.Enums;

namespace learning_asp_core.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string OrderRef {  get; set; }

        public DateTime SubmitDate { get; set; }

        public DateTime ProductionDate { get; set; }

        public DateTime BestStartShipDate { get; set; }

        public PriorityType Priority { get; set; }

        public HashSet<SubOrder> SubOrders { get; set; }

        public Order() {
            SubOrders = new HashSet<SubOrder>();
        }
    }
}
