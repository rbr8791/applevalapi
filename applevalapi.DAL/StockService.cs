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
    public class StockService : IStockService
    {
        private readonly ApplDbContext _context;


        public StockService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public Stock FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(stock => stock.Id == id);
        }

        private IEnumerable<Stock> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.Stocks;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }
        public IEnumerable<Stock> GetAll()
        {
            return BaseQuery();
                //.FirstOrDefault(stock => stock.Id == id);
        } // End IEnumerable

        public Stock GetById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(stock => stock.Id == id);
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