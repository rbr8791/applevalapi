using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IAuditLogService
    {
        IEnumerable<AuditLog> GetAll();
        AuditLog GetById(int id);
        void Delete(int id);
        AuditLog FindByOrdinal(int id);
    }
}