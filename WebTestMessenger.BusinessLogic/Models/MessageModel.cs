namespace WebTestMessenger.BusinessLogic.Models
{
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.DataAccess.Entities;

    public class MessageModel : IEntityModel<Message>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsSend { get; set; }

        public int UserIdFrom { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public bool IsReceived { get; private set; }

        public Message ToEntity()
        {
            return new Message
            {
                Id = this.Id,
                Text = this.Text,
                IsSend = this.IsSend,
                UserIdFrom = this.UserIdFrom,
                UserId = this.UserId,
                User = this.User?.ToEntity()
            };
        }

        public void ToModel(Message entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.Text = entity.Text;
                this.IsSend = entity.IsSend;
                this.UserIdFrom = entity.UserIdFrom;
                this.UserId = entity.UserId;
            }
        }
    }
}
