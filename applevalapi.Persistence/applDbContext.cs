using System.Threading;
using System.Threading.Tasks;
using applevalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace applevalApi.Persistence
{
    public class ApplDbContext : DbContext, IApplDbContext
    {
        public ApplDbContext(DbContextOptions<ApplDbContext> options) : base(options) { }
        public ApplDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddPrimaryKeys();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation();
            
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

       
        public DbSet<ActiveToken> ActiveTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<AuditLogPurchase> AuditLogPurchases { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<StockMovementType> StockMovementTypes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}