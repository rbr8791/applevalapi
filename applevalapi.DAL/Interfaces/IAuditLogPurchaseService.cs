using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IAuditLogPurchaseService
    {
        IEnumerable<AuditLogPurchase> GetAll();
        AuditLogPurchase GetById(int id);
        void Delete(int id);
        AuditLogPurchase FindByOrdinal(int id);
    }
}