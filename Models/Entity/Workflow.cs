namespace learning_asp_core.Models.Entity
{
    public class Workflow
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Bss { get; set; }

        public Workflow(int id, string name, DateTime bss)
        {
            Id = id;
            Name = name;
            Bss = bss;
        }
    }
}
