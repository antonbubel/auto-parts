namespace AutoParts.Web.Client.Public.User
{
    using System.ComponentModel;

    public enum UnauthorizedUserRoutes
    {
        [Description("sign-in")]
        SignIn,

        [Description("sign-up")]
        SignUp,

        [Description("supplier-sign-up/*")]
        SupplierSignUp
    }
}
