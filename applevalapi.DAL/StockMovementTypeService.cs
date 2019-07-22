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
    public class StockMovementTypeService : IStockMovementTypeService
    {
        private readonly ApplDbContext _context;


        public StockMovementTypeService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public StockMovementType FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(stockMovementType => stockMovementType.Id == id);
        }

        public StockMovementType FindByName(string Name)
        {
            return BaseQuery()
                .FirstOrDefault(stockMovementType => stockMovementType.Name==Name);
        }

        private IEnumerable<StockMovementType> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.StockMovementTypes;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }
        public IEnumerable<StockMovementType> GetAll()
        {
            return BaseQuery();
                //.FirstOrDefault(stockMovementType => stockMovementType.Id == id);
        } // End IEnumerable

        public StockMovementType GetById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(stockMovementType => stockMovementType.Id == id);
        } // End GetById



        public void Delete(int id)
        {
            var stockMovementType = _context.StockMovementTypes.Find(id);
            if (stockMovementType != null)
            {
                _context.StockMovementTypes.Remove(stockMovementType);
                _context.SaveChanges();
            }
        } // End Delete




    } // End Class
}