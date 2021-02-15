namespace WebTestMessenger.DataAccess.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        public int Login { get; set; }

        public int Password { get; set; }

        public List<Message> Messages { get; set; }
    }
}
