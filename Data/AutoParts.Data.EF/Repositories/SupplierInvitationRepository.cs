namespace AutoParts.Data.EF.Repositories
{
    using Base;

    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Model.Entities;
    using Model.Repositories;

    public class SupplierInvitationRepository : Repository<long, SupplierInvitation>, ISupplierInvitationRepository
    {
        public SupplierInvitationRepository(IDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> SupplierInvitationExistsByEmail(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return await GetQueryable()
                .AnyAsync(supplierInvitation => supplierInvitation.NormalizedEmail == normalizedEmail);
        }

        public async Task<SupplierInvitation> GetInvitationByToken(string token)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(supplierInvitation => supplierInvitation.Token == token);
        }
    }
}
