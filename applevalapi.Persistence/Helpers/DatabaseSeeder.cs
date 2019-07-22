using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using applevalApi.Entities;

namespace applevalApi.Persistence.Helpers
{
    public class DatabaseSeeder
    {
        private readonly IApplDbContext _context;

        public DatabaseSeeder(ApplDbContext context)
        {
            _context = context;
        }

        

        public async Task<int> SeedRolesFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {filePath} must be supplied to {nameof(SeedRolesFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file { filePath} does not exist");
            }
            var dataSet = File.ReadAllText(filePath);
            var seedData = JsonConvert.DeserializeObject<List<Role>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.RoleName).Select(b => b.First());

            _context.Roles.AddRange(distinctSeedData);

           
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SeedCountriesFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {filePath} must be supplied to {nameof(SeedCountriesFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file { filePath} does not exist");
            }
            var dataSet = File.ReadAllText(filePath);
            var seedData = JsonConvert.DeserializeObject<List<Country>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.Name).Select(b => b.First());

            _context.Countries.AddRange(distinctSeedData);


            return await _context.SaveChangesAsync();
        }

        public async Task<int> SeedProductsFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {filePath} must be supplied to {nameof(SeedProductsFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file { filePath} does not exist");
            }
            var dataSet = File.ReadAllText(filePath);
            var seedData = JsonConvert.DeserializeObject<List<Product>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.Description).Select(b => b.First());

            _context.Products.AddRange(distinctSeedData);


            return await _context.SaveChangesAsync();
        }

        public async Task<int> SeedStockMovementTypeFromJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"Value of {filePath} must be supplied to {nameof(SeedStockMovementTypeFromJson)}");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"The file { filePath} does not exist");
            }
            var dataSet = File.ReadAllText(filePath);
            var seedData = JsonConvert.DeserializeObject<List<StockMovementType>>(dataSet);

            // ensure that we only get the distinct books (based on their name)
            var distinctSeedData = seedData.GroupBy(b => b.Name).Select(b => b.First());

            _context.StockMovementTypes.AddRange(distinctSeedData);


            return await _context.SaveChangesAsync();
        }


        

        
    }
}