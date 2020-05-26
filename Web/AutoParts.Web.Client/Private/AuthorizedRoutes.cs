namespace AutoParts.Web.Client.Private
{
    using System.ComponentModel;

    public enum AuthorizedRoutes
    {
        [Description("profile")]
        Profile,

        [Description("orders")]
        Orders,

        [Description("order-details/*")]
        OrderDetails
    }
}
