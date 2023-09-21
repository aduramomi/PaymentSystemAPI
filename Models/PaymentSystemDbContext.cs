using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PaymentSystemAPI.Models
{
    public class PaymentSystemDbContext : DbContext
    {
        protected readonly IConfiguration? _configuration;

        public PaymentSystemDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration?.GetConnectionString("AppConnString") ?? "");           
        }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
