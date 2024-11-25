using System.ComponentModel;

namespace learning_asp_core.Models.Enums
{
    public enum PriorityType
    {
        [Description("Event")]
        Event,

        [Description("Non-Event")]
        NonEvent,

        [Description("High")]
        High
    }
}
