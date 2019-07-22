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
    public class StockMovementService : IStockMovementService
    {
        private readonly ApplDbContext _context;


        public StockMovementService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public StockMovement FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(stockMovement => stockMovement.Id == id);
        }

        private IEnumerable<StockMovement> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.StockMovements;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }
        public IEnumerable<StockMovement> GetAll()
        {
            return BaseQuery();
                 //.FirstOrDefault(stockMovement => stockMovement.Id == id);
        } // End IEnumerable

        public StockMovement GetById(int id)
        {
            return BaseQuery()
                 .FirstOrDefault(stockMovement => stockMovement.Id == id);
        } // End GetById



        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        } // End Delete




    } // End Class
}