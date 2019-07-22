using applevalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace applevalApi.Persistence
{
    public static class ModelBuilderExtentions
    {
        /// <summary>
        /// Used to create the the primary keys for applevalApi's database model
        /// </summary>
        /// <param name="builder">An instance of the <see cref="ModelBuilder"/> to act on</param>
        public static void AddPrimaryKeys(this ModelBuilder builder)
        {
            // Empty
        }
    }
}