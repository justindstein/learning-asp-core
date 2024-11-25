using System.ComponentModel;

namespace learning_asp_core.Models.Enums
{
    public enum ApprovalType
    {
        [Description("Product Approval")]
        ProductApproval,

        [Description("Swatch Photo")]
        SwatchPhoto,

        [Description("Swatch Ship")]
        SwatchShip,

        [Description("Hat Photo")]
        HatPhoto,

        [Description("Hat Ship")]
        HatShip,

        [Description("Garment Photo")]
        GarmentPhoto,

        [Description("Garment Ship")]
        GarmentShip
    }
}
