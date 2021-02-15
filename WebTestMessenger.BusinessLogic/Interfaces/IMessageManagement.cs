namespace WebTestMessenger.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebTestMessenger.BusinessLogic.Models;

    public interface IMessageManagement
    {
        Task<List<MessageModel>> GetMessagesAsync(int userId);

        Task<List<MessageModel>> GetOwnMessagesAsync(int userId);

        Task<IList<(string, int)>> GetUsersListAsync();

        Task<bool> SendMessageAsync(MessageModel messageModel, int userId, int recipientUserId);

        Task<bool> DeleteMessageAsync(int messageId, int userId);
    }
}
