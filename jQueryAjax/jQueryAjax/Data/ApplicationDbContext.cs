using jQueryAjax.Models;
using Microsoft.EntityFrameworkCore;

namespace jQueryAjax.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BankingModel> Bankings { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
