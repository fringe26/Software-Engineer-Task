using Microsoft.EntityFrameworkCore;
using TaskWebAPI.Models;

namespace TaskWebAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
