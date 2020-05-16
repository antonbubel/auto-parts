namespace AutoParts.Web.Client.Private.Administrator
{
    using System.ComponentModel;

    public enum AdministratorRoutes
    {
        [Description("manage-car-brands")]
        ManageCarBrandsPage,

        [Description("manage-car-models/*")]
        ManageCarModelsPage,

        [Description("manage-car-modifications/*")]
        ManageCarModificationsPage,

        [Description("manage-suppliers")]
        ManageSuppliersPage,

        [Description("manage-manufacturers")]
        ManageManufacturers
    }
}
