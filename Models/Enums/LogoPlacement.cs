using System.ComponentModel;

namespace learning_asp_core.Models.Enums
{
    public enum LogoPlacement
    {
        [Description("Just Front")]
        JustFront,

        [Description("Back")]
        Back,

        [Description("Left Side")]
        LeftSide,

        [Description("Right Side")]
        RightSide
    }
}
