using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTestMessenger.DataAccess.Entities;

namespace WebTestMessenger.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        #region Message

        /// <summary>
        /// get received messages
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<Message></returns>
        public async Task<List<Message>> GetMessagesAsync(int userId)
        {
            try
            {
                return await this.context.Messages.Where(m => m.UserId == userId && m.UserIdFrom != 0).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Message>> GetOwnMessagesAsync(int userId)
        {
            try
            {
                return await this.context.Messages.Where(m => m.UserId == userId && m.UserIdFrom == 0).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  get list of users with count of messages
        /// </summary>
        public async Task<IList<(string, int)>> GetUsersListAsync()
        {
            try
            {
                List<User> users = await this.context.Users.ToListAsync();

                var result = from user in users
                        select (user.Login, user.Messages.Count);

                return result.ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SendMessageAsync(Message message, int userId, int recipientUserId)
        {
            message = message ?? throw new ArgumentNullException(nameof(message));

            try
            {
                User recipient = await this.context.Users.Where(u => u.Id == recipientUserId).FirstOrDefaultAsync();
                if (recipient == null)
                {
                    throw new Exception($"User with id {recipientUserId} not found!");
                }

                Message existMessage = await this.context.Messages.Where(m => m.Id == message.Id).FirstOrDefaultAsync();
                if (existMessage == null)
                {
                    Message msg = new Message
                    {
                        Text = message.Text,
                        UserId = userId,
                        IsSend = true
                    };
                    this.context.Messages.Add(msg);
                }

                message.UserIdFrom = userId;
                message.User = recipient;
                this.context.Messages.Add(message);

                return await this.context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// delete message if you send it
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMessageAsync(int messageId, int userId)
        {
            try
            {
                Message existMessage = await this.context.Messages.Where(m => m.Id == messageId && m.UserId == userId).FirstOrDefaultAsync();
                if (existMessage == null)
                {
                    throw new Exception($"Message with id {messageId} not found!");
                }

                if (!existMessage.IsSend)
                {
                    throw new Exception($"Message must be sended before deletion!");
                }

                this.context.Remove(existMessage);

                return await this.context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public async Task<User> RegisterAsync(User user)
        {
            user = user ?? throw new ArgumentNullException(nameof(user));

            try
            {
                User existUsr = await this.context.Users.Where(u => u.Login.Equals(user.Login)).FirstOrDefaultAsync();
                if (existUsr != null)
                {
                    throw new Exception($"User with login {user.Login} already exists!");
                }

                await this.context.AddAsync(user);

                return user;
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> GetUserAsync(string login, string password)
        {
            login = login ?? throw new ArgumentNullException(nameof(login));
            password = password ?? throw new ArgumentNullException(nameof(password));

            return await this.context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login) && u.Password.Equals(password));
        }
    }
}
