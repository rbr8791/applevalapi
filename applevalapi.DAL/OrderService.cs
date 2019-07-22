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
    public class OrderService : IOrderService
    {
        private readonly ApplDbContext _context;


        public OrderService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public Order FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(order => order.Id == id);
        }

        private IEnumerable<Order> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Orders;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }
        public IEnumerable<Order> GetAll()
        {
            return BaseQuery();
            //.FirstOrDefault(order => order.Id == id);
        } // End IEnumerable

        public Order GetById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(order => order.Id == id);
        } // End GetById



        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        } // End Delete


        //public Order Create(Order order, IEnumerable<OrderDetail> details, User user)

        public Order Create(Order order,  User user)
        {
            RoleService rService = new RoleService(_context);
            StockService stockService = new StockService(_context);
            StockMovementService stockMovementService = new StockMovementService(_context);
            AuditLogService auditLogService = new AuditLogService(_context);
            StockMovementTypeService stockMovementTypeService = new StockMovementTypeService(_context);
            OrderDetailService orderDetailService = new OrderDetailService(_context);
            ProductService productService = new ProductService(_context);

            Role role = new Role();
            role = rService.GetById(user.Role.Id);
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(order);

            StockMovementType stockMovementType = stockMovementTypeService.FindByName("Out");
            AuditLog auditLog = new AuditLog();
            auditLog.createdBy = user.Username;
            auditLog.CreationDate = DateTime.Now;
            auditLog.Description = "Auditing Purchase Order Creation: " + order.Description;
            auditLog.RawParams = jsonString;
            auditLog.EventType = "Purchase Order Creation";
            auditLog.UserAudited = user.Username;


            AuditLogPurchase auditLogPurchase = new AuditLogPurchase();
            auditLogPurchase.createdBy = user.Username;
            auditLogPurchase.PurchaseDate = DateTime.Now;
            auditLogPurchase.Description = "Auditing Purchase Order header Creation: " + order.Description;
            auditLogPurchase.RawParams = jsonString;
            auditLogPurchase.EventType = "Auditing Purchase Order,header,Creation";
            auditLogPurchase.UserAudited = user.Username;
            auditLogPurchase.Quantity = 0;
            auditLogPurchase.product = null;
            auditLogPurchase.UserAudited = user.Username;
            auditLogPurchase.Customer = user;



            Order errorProduct = new Order();


            // Only users with customer or admin role can create an order, everyone else is just an internal user
            if (role.RoleName.ToLower() == "customer" || role.RoleName.ToLower() =="admin")
            {
                // Validation
                //if (string.IsNullOrWhiteSpace(product.Description))
                //{
                //    //if (float.IsNaN(product.Price))
                //    //{
                //    //    product.Price = float.Parse("0.00");
                //    //}
                //    throw new System.ApplicationException("The product description can not be empty");
                //}

                //product.Status = true;

                //_context.Products.Add(product);
                //_context.SaveChanges();


                //// Create Stock
                //Stock stock = new Stock();
                //stock.Description = product.Description;
                //stock.createdBy = user.Username;
                //stock.product = product;
                //// We are creating a new product in here, not handling stock movements, but We will create this record to match the product creation
                //// for it's own stock register.
                //// The stock quantity needs to be handled by StockMovementService
                //stock.Quantity = 0;
                //stock.Status = true;

                //_context.Stocks.Add(stock);
                //_context.SaveChanges();
                //// Create the auditLog

                //_context.AuditLogs.Add(auditLog);
                //_context.SaveChanges();

                //return product;

                // First save the order header

                order.createdBy = user.Username;
                order.Created = DateTime.Now;
                order.Status = true;

                foreach (OrderDetail d in order.Details)
                {
                    d.createdBy = user.Username;
                    d.Created = DateTime.Now;

                    // Check if there is stock available for the purchase
                    Stock currentStock = stockService.FindByOrdinal(d.product.Id);
                    if (currentStock.Quantity <= 0 || currentStock.Quantity < d.Quantity)
                    {
                        errorProduct.Description = "Error: Product: " + d.product.Description + " has not stock available";
                        errorProduct.Status = false;
                        errorProduct.Id = -1;

                        return errorProduct;
                    } 
                    // Update stock
                    currentStock.Quantity = currentStock.Quantity - d.Quantity;
                    
                    // Get the product by Id in the order request
                    Product pr = productService.FindByOrdinal(d.product.Id);
                    if (pr != null)
                    {
                        d.product.Description = pr.Description;
                        d.product.Likes = pr.Likes;
                        d.product.Price = pr.Price;
                        d.product.Status = pr.Status;
                        
                    }
                    // TODO: Add transactions and rollback handler scenarios (THIS IS FOR THE PRO VERSION!!!) XD
                    // R@ul Berrios 
                    // Why we need this, because we don't know when something was fail, in that case we need to handle the rolling back
                    // scenarios, restore previous stock values to their original state.


                        _context.Stocks.Update(currentStock);
                        _context.SaveChanges();
                    
                    // Update current stock
                  
                    // Add stock movement
                    StockMovement stockMovement = new StockMovement();
                    stockMovement.product = d.product;
                    stockMovement.Modified = DateTime.Now;
                    stockMovement.Quantity = d.Quantity;
                    stockMovement.StockMovementType = stockMovementType;
                    stockMovement.DocumentID = order.OrderNumber;

                    _context.StockMovements.Add(stockMovement);
                    _context.SaveChanges();


                    // Add Log Purchase Record
                    AuditLogPurchase auditLogPurchaseItem = new AuditLogPurchase();
                    auditLogPurchaseItem.createdBy = user.Username;
                    auditLogPurchaseItem.PurchaseDate = DateTime.Now;
                    auditLogPurchaseItem.Description = "Auditing Purchase Order,Detail,Creation: " + d.product.Description;
                    //auditLogPurchaseItem.RawParams = jsonString;
                    auditLogPurchaseItem.EventType = "Purchase Order Detail Creation";
                    auditLogPurchaseItem.UserAudited = user.Username;
                    auditLogPurchaseItem.Quantity = d.Quantity;
                    auditLogPurchaseItem.product = d.product;
                    auditLogPurchaseItem.UserAudited = user.Username;
                    auditLogPurchaseItem.Customer = user;

                    _context.AuditLogPurchases.Add(auditLogPurchaseItem);
                    _context.SaveChanges();
                    // add Detail
                    _context.OrderDetails.Add(d);
                    _context.SaveChanges();






                    //order.OrderDetails.Add(d);

                }

                //_context.Orders.Add(order);
                //_context.SaveChanges();

                // Now Save the order details
                //foreach (OrderDetail d in details)
                //{
                //    d.createdBy = user.Username;
                //    d.Created = DateTime.Now;
                //    order.OrderDetails.Add(d);
                //}

                
                _context.Orders.Add(order);
                _context.SaveChanges();

                return order;
            }
            else
            {
               
                errorProduct.Description = "Only customers or administrators users can create an order!";
                errorProduct.Status = false;
                errorProduct.Id = -1;
               
                return errorProduct;

            }


        } // End Create


    } // End Class
}