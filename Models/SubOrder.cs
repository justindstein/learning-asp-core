namespace learning_asp_core.Models
{
    public class Suborder
    {
        public HashSet<Decoration> Decorations { get; set; }

        public HashSet<OrderEntry> OrderEntries { get; set; }

        public Suborder() {
            Decorations = new HashSet<Decoration>();
            OrderEntries = new HashSet<OrderEntry>();
        }
    }
}
