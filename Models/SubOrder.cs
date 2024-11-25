namespace learning_asp_core.Models
{
    public class SubOrder
    {
        public HashSet<Decoration> Decorations;

        public HashSet<OrderEntry> OrderEntries;

        public SubOrder() { }
    }
}
