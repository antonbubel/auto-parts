namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using Model.Entities;
    using Model.Repositories;

    public class SupplierInvitationRepository : Repository<long, SupplierInitation>, ISupplierInvitationRepository
    {
        public SupplierInvitationRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}
