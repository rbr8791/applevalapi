using System.IO;
using System.Linq;
using applevalApi.Persistence.Helpers;

namespace applevalApi.Persistence
{
    public static class ApplDbContextExtensions
    {
        public static int EnsureSeedData(this ApplDbContext context)
        {
            var bookCount = default(int);
            var characterCount = default(int);
            var bookSeriesCount = default(int);
            var rolesCount = default(int);
            var countriesCount = default(int);
            var stockMovementTypesCount = default(int);
            var productsCount = default(int);

            // Because each of the following seed method needs to do a save
            // (the data they're importing is relational), we need to call
            // SaveAsync within each method.
            // So let's keep tabs on the counts as they come back

            var dbSeeder = new DatabaseSeeder(context);
           
            if (!context.Countries.Any())
            {
                var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CountrySeedData.json");
                countriesCount = dbSeeder.SeedCountriesFromJson(pathToSeedData).Result;
            }
            if (!context.StockMovementTypes.Any())
            {
                var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "StockMovementType.json");
                stockMovementTypesCount = dbSeeder.SeedStockMovementTypeFromJson(pathToSeedData).Result;
            }
            if (!context.Products.Any())
            {
                var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "ProductSeedData.json");
                productsCount = dbSeeder.SeedProductsFromJson(pathToSeedData).Result;
            }
            if (!context.Roles.Any())
            {
                var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "RoleSeedData.json");
                rolesCount = dbSeeder.SeedRolesFromJson(pathToSeedData).Result;
                if ( rolesCount >0)
                {


                    int adminId = context
                      .Roles
                      .Where(r => r.RoleName == "Admin")
                      .Select(r => r.Id)
                      .SingleOrDefault();

                    applevalApi.Entities.Role role = context.Roles.Find(adminId);

                    if ( adminId > 0 && role != null)
                    {
                        byte[] passwordHash, passwordSalt;
                        var password = "admin";
                        Tools.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                        // Add user Admin
                        var user = new applevalApi.Entities.User();
                        user.FirstName = "admin";
                        user.LastName = "admin";
                        user.Username = "admin";
                        user.Password = "";
                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                        user.Role = role;
                        user.Enabled = true;

                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    int operatorId = context
                      .Roles
                      .Where(r => r.RoleName == "Operator")
                      .Select(r => r.Id)
                      .SingleOrDefault();

                    applevalApi.Entities.Role roleOperator = context.Roles.Find(operatorId);

                    int customerId = context
                     .Roles
                     .Where(r => r.RoleName == "Customer")
                     .Select(r => r.Id)
                     .SingleOrDefault();

                    applevalApi.Entities.Role roleCustomer = context.Roles.Find(customerId);

                    if (operatorId > 0 && roleOperator != null)
                    {
                        byte[] passwordHash, passwordSalt;
                        var password = "operator";
                        Tools.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                        // Add user Admin
                        var userOperator = new applevalApi.Entities.User();
                        userOperator.FirstName = "operator";
                        userOperator.LastName = "operator";
                        userOperator.Username = "operator";
                        userOperator.Password = "";
                        userOperator.PasswordHash = passwordHash;
                        userOperator.PasswordSalt = passwordSalt;
                        userOperator.Role = roleOperator;
                        userOperator.Enabled = true;

                        context.Users.Add(userOperator);
                        context.SaveChanges();
                    }


                    if (operatorId > 0 && roleOperator != null)
                    {
                        byte[] passwordHash, passwordSalt;
                        var password = "operator";
                        Tools.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                        // Add user Admin
                        var userOperator = new applevalApi.Entities.User();
                        userOperator.FirstName = "operator";
                        userOperator.LastName = "operator";
                        userOperator.Username = "operator";
                        userOperator.Password = "";
                        userOperator.PasswordHash = passwordHash;
                        userOperator.PasswordSalt = passwordSalt;
                        userOperator.Role = roleOperator;
                        userOperator.Enabled = true;

                        context.Users.Add(userOperator);
                        context.SaveChanges();
                    }

                    if (customerId > 0 && roleCustomer != null)
                    {
                        byte[] passwordHash, passwordSalt;
                        var password = "customer";
                        Tools.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                        // Add user Admin
                        var userCustomer = new applevalApi.Entities.User();
                        userCustomer.FirstName = "customer";
                        userCustomer.LastName = "customer";
                        userCustomer.Username = "customer";
                        userCustomer.Password = "";
                        userCustomer.PasswordHash = passwordHash;
                        userCustomer.PasswordSalt = passwordSalt;
                        userCustomer.Role = roleCustomer;
                        userCustomer.Enabled = true;

                        context.Users.Add(userCustomer);
                        context.SaveChanges();
                    }


                }
            }

            // Existen los productus pero el stock esta vacio
            if (!context.Stocks.Any() && context.Products.Any())
            {
                foreach (applevalApi.Entities.Product product in context.Products) {
                    //sw.WriteLine(sw.QuoteId.ToString() + "," + sw.Quotation);
                    var stockItem = new applevalApi.Entities.Stock();
                    var stockMovement = new applevalApi.Entities.StockMovement();

                    int inStockMovementTypeId = context
                     .StockMovementTypes
                     .Where(r => r.Name == "In")
                     .Select(r => r.Id)
                     .SingleOrDefault();

                    applevalApi.Entities.StockMovementType stockMovementType = context.StockMovementTypes.Find(inStockMovementTypeId);

                    stockItem.Description = product.Description;
                    stockItem.product = product;
                    stockItem.Quantity = 1000;
                    context.Stocks.Add(stockItem);


                    stockMovement.product = product;
                    stockMovement.DocumentID = "Database-Initial-Seed-0001";
                    stockMovement.Quantity = 1000;
                    stockMovement.StockMovementType = stockMovementType;

                    context.StockMovements.Add(stockMovement);
                    context.SaveChanges();

                }
            }

                return bookCount + characterCount + bookSeriesCount;
        }
    }
}