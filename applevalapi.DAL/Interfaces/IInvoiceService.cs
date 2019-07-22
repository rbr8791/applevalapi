using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice GetById(int id);
        void Delete(int id);
        Invoice FindByOrdinal(int id);
    }
}