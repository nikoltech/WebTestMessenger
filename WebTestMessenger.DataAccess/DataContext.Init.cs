namespace WebTestMessenger.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public partial class DataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }


    }
}
