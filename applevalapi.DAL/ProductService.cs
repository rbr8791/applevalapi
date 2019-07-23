using System.Collections.Generic;
using System.Linq;
using applevalApi.Entities;
using applevalApi.Persistence;
using Microsoft.EntityFrameworkCore;
using applevalApi.DAL.Interfaces;
using System;

using System.Threading.Tasks;


namespace applevalApi.DAL 
{
    public class ProductService : IProductService
    {
        private readonly ApplDbContext _context;


        public ProductService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public Product FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(product => product.Id == id);
        }

        public IEnumerable<Product> FindByName(string productName)
        {
            return BaseQuery()
                .Where(product => product.Description.Contains(productName));
        }

        public Product FindByOrdinalAsNoTracking(int id)
        {
            return BaseQueryAsNoTracking()
                .FirstOrDefault(product => product.Id == id);
        }

        private IEnumerable<Product> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Products;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }

        private IEnumerable<Product> BaseQueryAsNoTracking()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Products.AsNoTracking();
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }


        public IEnumerable<Product> GetAll()
        {
            return BaseQuery();
                //.FirstOrDefault(product => product.Id == id);
        } // End IEnumerable

        public IEnumerable<Product> GetAllSortedByNameAndLikes()
        {
            return BaseQuery()
                .OrderBy(p =>  p.Description).ThenBy( p => p.Likes);
            
            //.FirstOrDefault(product => product.Id == id);
        } // End IEnumerable


        public Product GetById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(product => product.Id == id);
        } // End GetById



        public Product Delete(int id, User user)
        {
            RoleService rService = new RoleService(_context);
            Role role = new Role();
            role = rService.GetById(user.Role.Id);
           
            Product returnProduct = new Product();
            var product = _context.Products.Find(id);

            if (role.RoleName == "Admin")
            {
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();

                    returnProduct.Description = "Delete product Successfully";
                    returnProduct.Status = false;
                    returnProduct.Id = 0;
                    returnProduct.Price = 0;
                    return returnProduct;

                } else
                {
                    returnProduct.Description = "The product doesn't exists!";
                    returnProduct.Status = false;
                    returnProduct.Id = 0;
                    returnProduct.Price = 0;
                    return returnProduct;

                }
            } else
            {
                returnProduct.Description = "Only Admin users can delete products!";
                returnProduct.Status = false;
                returnProduct.Id = 0;
                returnProduct.Price = 0;
                return returnProduct;
            }

            
        } // End Delete

        /*
         * 
         * 
         *  Id = Id,
                Description = Description,
                Price = Price,
                Status = Status, 
         */
        public Product Create(Product product, User user)
        {
            RoleService rService = new RoleService(_context);
            StockService stockService = new StockService(_context);
            StockMovementService stockMovementService = new StockMovementService(_context);
            AuditLogService auditLogService = new AuditLogService(_context);
            StockMovementTypeService stockMovementTypeService = new StockMovementTypeService(_context);


            Role role = new Role();
            role = rService.GetById(user.Role.Id);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(product);

            StockMovementType stockMovementType = stockMovementTypeService.FindByName("In");
            AuditLog auditLog = new AuditLog();
            auditLog.createdBy = user.Username;
            auditLog.CreationDate = DateTime.Now;
            auditLog.Description = "Auditing Product Creation: " + product.Description;
            auditLog.RawParams = jsonString;
            auditLog.EventType = "Product Creation";
            auditLog.UserAudited = user.Username;




            if (role.RoleName == "Admin")
            {
                // Validation
                if (string.IsNullOrWhiteSpace(product.Description))
                {
                    //if (float.IsNaN(product.Price))
                    //{
                    //    product.Price = float.Parse("0.00");
                    //}
                    throw new System.ApplicationException("The product description can not be empty");
                }
               
                product.Status = true;

                _context.Products.Add(product);
                _context.SaveChanges();


                // Create Stock
                Stock stock = new Stock();
                stock.Description = product.Description;
                stock.createdBy = user.Username;
                stock.product = product;
                // We are creating a new product in here, not handling stock movements, but We will create this record to match the product creation
                // for it's own stock register.
                // The stock quantity needs to be handled by StockMovementService
                stock.Quantity = 0;
                stock.Status = true;

                _context.Stocks.Add(stock);
                _context.SaveChanges();
                // Create the auditLog

                _context.AuditLogs.Add(auditLog);
                _context.SaveChanges();

                return product;
            } else
            {
                Product errorProduct = new Product();
                errorProduct.Description = "Only Admin users can update a product!";
                errorProduct.Status = false;
                errorProduct.Id = -1;
                errorProduct.Price = 0;
                return errorProduct;

            }
           

        } // End Create


        public Product Update(Product product, User user)
        {
            RoleService rService = new RoleService(_context);
            Role role = new Role();
            role = rService.GetById(user.Role.Id);

            Product p = this.FindByOrdinalAsNoTracking(product.Id);

            StockService stockService = new StockService(_context);
            StockMovementService stockMovementService = new StockMovementService(_context);
            AuditLogService auditLogService = new AuditLogService(_context);
            StockMovementTypeService stockMovementTypeService = new StockMovementTypeService(_context);

            Product errorResponse = new Product();
           


            if (role.RoleName == "Admin")
            {
                // Validation
                if (string.IsNullOrWhiteSpace(product.Description))
                {
                    //throw new System.ApplicationException("The product description can not be empty");
                    //Product errorResponse = new Product();
                    errorResponse.Description = "The product description can not be empty";
                    errorResponse.Id = -1;
                    return errorResponse;
                }
              
                if (product.Price.HasValue) 
                {
                    p.Price = product.Price;
                }
                if (product.Status.HasValue)
                {
                    p.Status = product.Status;
                }
                if(product.Description != p.Description)
                {
                    p.Description = product.Description;
                }
                if (product.Likes != p.Likes)
                {
                    p.Likes = product.Likes;
                }
                p.Modified = DateTime.Now;
                


                _context.Products.Update(p);
                _context.SaveChanges();

                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(product);
                AuditLog auditLog = new AuditLog();
                auditLog.createdBy = user.Username;
                auditLog.CreationDate = DateTime.Now;
                auditLog.Description = "Auditing Product Update: " + product.Description;
                auditLog.RawParams = jsonString;
                auditLog.EventType = "Product Update";
                auditLog.UserAudited = user.Username;

                _context.AuditLogs.Add(auditLog);
                _context.SaveChanges();

                return p;
            }
            else
            {
                // throw new System.ApplicationException("You need admin rights to add a new product");
                // Product error
                
                errorResponse.Description = "Only Administrator users can update products";
                errorResponse.Id = -1;
                return errorResponse;
            }


        } // End Update


        public Product LikeProductById(Product product, User user)
        {
            RoleService rService = new RoleService(_context);
            Role role = new Role();
            role = rService.GetById(user.Role.Id);

            Product p = this.FindByOrdinalAsNoTracking(product.Id);
            int? currentLikes = p.Likes;
            if (!currentLikes.HasValue)
            {
                currentLikes = 0;
            }

            currentLikes = currentLikes + 1;

            p.Likes = currentLikes;
            p.Modified = DateTime.Now;

            try
            {
                _context.Products.Update(p);
                _context.SaveChanges();

                return p;
            } catch (Exception ex)
            {
                // throw new System.ApplicationException("You need admin rights to add a new product");
                // Product error
                Product errorResponse = new Product();
                errorResponse.Description = ex.Message;
                errorResponse.Id = -1;
                return errorResponse;
            }
            


        } // End 


        public Product ChangePriceById(Product product, User user)
        {
            RoleService rService = new RoleService(_context);
            Role role = new Role();
            role = rService.GetById(user.Role.Id);

            Product p = this.FindByOrdinalAsNoTracking(product.Id);

            StockService stockService = new StockService(_context);
            StockMovementService stockMovementService = new StockMovementService(_context);
            AuditLogService auditLogService = new AuditLogService(_context);
            StockMovementTypeService stockMovementTypeService = new StockMovementTypeService(_context);





            if (role.RoleName == "Admin")
            {
                

                if (product.Price.HasValue)
                {
                    p.Price = product.Price;
                }
               
                p.Modified = DateTime.Now;



                _context.Products.Update(p);
                _context.SaveChanges();

                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                AuditLog auditLog = new AuditLog();
                auditLog.createdBy = user.Username;
                auditLog.CreationDate = DateTime.Now;
                auditLog.Description = "Auditing Product Update [Price Change]: " + product.Description;
                auditLog.RawParams = jsonString;
                auditLog.EventType = "Product Update";
                auditLog.UserAudited = user.Username;

                _context.AuditLogs.Add(auditLog);
                _context.SaveChanges();

                return p;
            }
            else
            {
                // throw new System.ApplicationException("You need admin rights to add a new product");
                // Product error
                Product errorResponse = new Product();
                errorResponse.Description = "Only Administrator users can update product prices";
                errorResponse.Id = -1;
                return errorResponse;
            }


        } // End ChangePriceById




    } // End Class
}