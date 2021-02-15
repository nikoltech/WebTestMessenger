namespace WebTestMessenger.BusinessLogic.Managements
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.BusinessLogic.Models;
    using WebTestMessenger.DataAccess.Entities;
    using WebTestMessenger.DataAccess.Repositories;

    public class MessageManagement : IMessageManagement
    {
        private readonly IRepository repo;

        public MessageManagement(IRepository repository)
        {
            this.repo = repository;
        }

        public async Task<List<MessageModel>> GetMessagesAsync(int userId)
        {
            try
            {
                List<Message> entities = await this.repo.GetMessagesAsync(userId).ConfigureAwait(false);

                List<MessageModel> models = new List<MessageModel>();
                foreach (var entity in entities)
                {
                    MessageModel model = new MessageModel();
                    model.ToModel(entity);

                    models.Add(model);
                }
                
                return models;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<MessageModel>> GetOwnMessagesAsync(int userId)
        {
            try
            {
                List<Message> entities = await this.repo.GetOwnMessagesAsync(userId).ConfigureAwait(false);

                List<MessageModel> models = new List<MessageModel>();
                foreach (var entity in entities)
                {
                    MessageModel model = new MessageModel();
                    model.ToModel(entity);

                    models.Add(model);
                }

                return models;
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
                return await this.repo.GetUsersListAsync().ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SendMessageAsync(MessageModel messageModel, int userId, int recipientUserId)
        {
            try 
            {
                messageModel = messageModel ?? throw new ArgumentNullException(nameof(messageModel));

                return await this.repo.SendMessageAsync(messageModel.ToEntity(), userId, recipientUserId);
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
        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            try
            {
                return await this.repo.DeleteMessageAsync(messageId).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}
