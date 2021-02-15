using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTestMessenger.DataAccess.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [MaxLength()]
        public string Text { get; set; }

        public bool IsSend { get; set; }

        public int UserIdFrom { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [NotMapped]
        public bool IsReceived 
        {
            get
            {
                return UserIdFrom != 0;
            }
        }
    }
}
