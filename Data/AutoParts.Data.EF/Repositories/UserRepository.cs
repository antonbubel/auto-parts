namespace AutoParts.Data.EF.Repositories
{
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;

    using Model.Entities;
    using Model.Repositories;

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext context;

        public UserRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public async Task<User> FindAsync(long key)
        {
            return await context.DbSet<User>()
                .FindAsync(key);
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            var normalizedEmail = email.ToUpperInvariant();

            return await context.DbSet<User>()
                .AnyAsync(user => user.NormalizedEmail == normalizedEmail);
        }
    }
}
