using System.Threading;
using System.Threading.Tasks;
using applevalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace applevalApi.Persistence
{
    public interface IApplDbContext
    {
        /// <summary>
        /// Asynchronously saves all changes made in the DwContext to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true,
            CancellationToken cancellationToken = default(CancellationToken));

       

        DbSet<ActiveToken> ActiveTokens { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }

        DbSet<AuditLogPurchase> AuditLogPurchases { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<StockMovement> StockMovements { get; set; }
        DbSet<StockMovementType> StockMovementTypes { get; set; }
        DbSet<User> Users { get; set; }


    }
}