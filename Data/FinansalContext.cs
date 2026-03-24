using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class FinansalContext:DbContext
    {
        public FinansalContext(DbContextOptions<FinansalContext> options)
             : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
