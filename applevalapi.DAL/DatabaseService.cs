using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using applevalApi.Entities;
using applevalApi.Persistence;
using applevalApi.DAL.Interfaces;

namespace applevalApi.DAL
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplDbContext _context;
        public DatabaseService(ApplDbContext context)
        {
            _context = context;
        }
        public bool ClearDatabase()
        {
            var cleared = _context.Database.EnsureDeleted();
            var created = _context.Database.EnsureCreated();
            var entitiesadded = _context.SaveChanges();

            return (cleared && created && entitiesadded == 0);
        }

        public int SeedDatabase()
        {
            return _context.EnsureSeedData();
        }

       

        public async Task<int> SaveAnyChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}