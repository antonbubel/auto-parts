namespace AutoParts.Data.Model.Repositories
{
    using System.Threading.Tasks;

    using Entities;

    public interface IUserRepository
    {
        Task<User> FindAsync(long key);

        Task<bool> UserExistsByEmail(string email);
    }
}
