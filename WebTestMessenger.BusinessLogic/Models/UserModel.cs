namespace WebTestMessenger.BusinessLogic.Models
{
    using System.Collections.Generic;
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.DataAccess.Entities;

    public class UserModel : IEntityModel<User>
    {
        public UserModel()
        {
            this.Messages = new List<MessageModel>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<MessageModel> Messages { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Id = this.Id,
                Login = this.Login,
                Password = this.Password
            };
        }

        public void ToModel(User entity)
        {
            if (entity != null)
            {
                this.Id = entity.Id;
                this.Login = entity.Login;
                this.Password = entity.Password;

                foreach (var msg in entity.Messages)
                {
                    MessageModel model = new MessageModel();
                    model.ToModel(msg);

                    this.Messages.Add(model);
                }
            }
        }
    }
}
