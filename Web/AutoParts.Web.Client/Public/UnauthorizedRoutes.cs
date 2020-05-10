namespace AutoParts.Web.Client.Public
{
    using System.ComponentModel;

    public enum UnauthorizedRoutes
    {
        [Description("sign-in")]
        SignIn,

        [Description("sign-up")]
        SignUp,

        [Description("supplier-sign-up/*")]
        SupplierSignUp
    }
}
