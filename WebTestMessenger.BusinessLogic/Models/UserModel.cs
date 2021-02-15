namespace WebTestMessenger.BusinessLogic.Models
{
    using System.Collections.Generic;
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.DataAccess.Entities;

    public class UserModel : IEntityModel<User>
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<MessageModel> Messages { get; set; }

        public User ToEntity()
        {
            throw new System.NotImplementedException();
        }

        public void ToModel(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
