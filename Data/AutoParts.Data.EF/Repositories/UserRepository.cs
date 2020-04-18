namespace AutoParts.Data.EF.Repositories
{
    using System.Threading.Tasks;

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
    }
}
