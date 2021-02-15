namespace WebTestMessenger.BusinessLogic.Models
{
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.DataAccess.Entities;

    public class MessageModel : IEntityModel<Message>
    {
        public Message ToEntity()
        {
            throw new System.NotImplementedException();
        }

        public void ToModel(Message entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
