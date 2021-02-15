namespace WebTestMessenger.BusinessLogic.Models
{
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.DataAccess.Entities;

    public class UserModel : IEntityModel<User>
    {
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
