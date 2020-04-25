namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Base;
    using Entities;

    public interface ISupplierInvitationRepository : IRepository<long, SupplierInvitation>
    {
        Task<bool> SupplierInvitationExistsByEmail(string email);
    }
}
