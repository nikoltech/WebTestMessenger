using Microsoft.EntityFrameworkCore;
using System;
using WebTestMessenger.DataAccess.Entities;

namespace WebTestMessenger.DataAccess
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
