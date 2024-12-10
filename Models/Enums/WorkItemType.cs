using System.ComponentModel;

namespace learning_asp_core.Models.Enums
{
    public enum WorkItemType
    {
        [Description("Order")]
        Order,

        [Description("Suborder")]
        Suborder
    }
}
