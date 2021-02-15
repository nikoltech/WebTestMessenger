namespace WebTestMessenger.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebTestMessenger.DataAccess.Entities;

    public interface IRepository
    {
        #region Message
        Task<bool> DeleteMessageAsync(int messageId, int userId);

        Task<bool> SendMessageAsync(Message message, int userId, int userIdTo);

        Task<IList<(string, int)>> GetUsersListAsync();

        Task<List<Message>> GetOwnMessagesAsync(int userId);

        Task<List<Message>> GetMessagesAsync(int userId);
        #endregion

        Task<User> RegisterAsync(User user);

        Task<User> GetUserAsync(string login, string password);
    }
}
