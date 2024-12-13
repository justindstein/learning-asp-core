using learning_asp_core.Models.Enums;

namespace learning_asp_core.Models
{
    public class Decoration
    {
        public LogoType LogoType {  get; set; }

        public LogoPlacement LogoPlacement { get; set; }

        public string ImageUrl { get; set; }

        public string LogoUrl { get; set; }

        public string Notes { get; set; }
    }
}