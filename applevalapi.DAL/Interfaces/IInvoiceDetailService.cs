using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IInvoiceDetailService
    {
        IEnumerable<InvoiceDetail> GetAll();
        InvoiceDetail GetById(int id);
        void Delete(int id);
        InvoiceDetail FindByOrdinal(int id);
    }
}