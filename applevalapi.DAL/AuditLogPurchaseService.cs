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
    public class AuditLogPurchaseService : IAuditLogPurchaseService
    {
        private readonly ApplDbContext _context;


        public AuditLogPurchaseService(ApplDbContext context)
        {
            _context = context;
        } // End DataContext

        public AuditLogPurchase FindByOrdinal(int id)
        {
            return BaseQuery()
                .FirstOrDefault(auditLog => auditLog.Id == id);
        }

        private IEnumerable<AuditLogPurchase> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _context.AuditLogPurchases;
            //.Include(book => book.BookCharacter)
            //.ThenInclude(bookCharacter => bookCharacter.Character)
            //.Include(activeToken => .BookSeries)
            //.ThenInclude(bookSeries => bookSeries.Series
            //);
        }

        public IEnumerable<AuditLogPurchase> GetAll()
        {
            //return _context.AuditLogs;
            return BaseQuery();
              // .FirstOrDefault(auditLog => auditLog.Id == id);
        } // End IEnumerable

        public AuditLogPurchase GetById(int id)
        {
            // return _context.AuditLogs.Find(id);
            return BaseQuery()
                .FirstOrDefault(auditLog => auditLog.Id == id);
        } // End GetById


        public void Delete(int id)
        {
            var auditLogPurchase = _context.AuditLogPurchases.Find(id);
            if (auditLogPurchase != null)
            {
                _context.AuditLogPurchases.Remove(auditLogPurchase);
                _context.SaveChanges();
            }
        } // End Delete



    } // End Class
}