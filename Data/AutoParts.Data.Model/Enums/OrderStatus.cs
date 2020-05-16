namespace AutoParts.Data.Model.Enums
{
    using System.ComponentModel;

    public enum OrderStatus
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Processing")]
        Processing = 2,

        [Description("On hold")]
        OnHold = 3,

        [Description("Canceled")]
        Canceled = 4,

        [Description("Completed")]
        Completed = 5
    }
}
