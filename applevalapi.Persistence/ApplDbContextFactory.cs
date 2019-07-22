using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using applevalApi.Common;

namespace applevalApi.Persistence
{
    /// <summary>
    /// This factory is provided so that the EF Core tools can build a full context
    /// without having to have access to where the DbContext is being created (i.e.
    /// in the UI layer).
    /// </summary>
    /// <remarks>
    /// Please see the following URL for more information:
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-idbcontextfactorytcontext
    /// </remarks>
    public class ApplDbContextFactory : IDesignTimeDbContextFactory<ApplDbContext>
    {
        private static string DbConnectionString => new DatabaseConfiguration().GetDatabaseConnectionString();

        public ApplDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplDbContext>();

            optionsBuilder.UseSqlite(DbConnectionString);

            return new ApplDbContext(optionsBuilder.Options);
        }
    }
}