using learning_asp_core.Models.Enums;
using Newtonsoft.Json;

namespace learning_asp_core.Models
{
    public class Order
    {
        public string OrderId { get; set; }

        public string OrderRef {  get; set; }

        public DateTime SubmitDate { get; set; }

        public DateTime ProductionDate { get; set; }

        public DateTime BestStartShipDate { get; set; }

        public PriorityType Priority { get; set; }

        public HashSet<Suborder> Suborders { get; set; }

        public Order() {
            Suborders = new HashSet<Suborder>();
        }
    }
}
