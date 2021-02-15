namespace WebTestMessenger.DataAccess.Repositories
{
    using System.Threading.Tasks;
    using WebTestMessenger.DataAccess.Entities;

    public interface IRepository
    {
        Task<User> GetUserAsync(string login, string password);
    }
}
