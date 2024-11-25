using learning_asp_core.Models.Enums;

namespace learning_asp_core.Models
{
    public class Decoration
    {
        public LogoType LogoType {  get; set; }

        public LogoPlacement LogoPlacement { get; set; }

        public string? decorationUrl { get; set; }

        public string? decorationNotes {  get; set; }

    }
}