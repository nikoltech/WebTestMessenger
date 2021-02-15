using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<User> GetUserAsync(string login, string password)
        {
            login = login ?? throw new ArgumentNullException(nameof(login));
            password = password ?? throw new ArgumentNullException(nameof(password));

            return await this.context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login) && u.Password.Equals(password));
        }
    }
}
