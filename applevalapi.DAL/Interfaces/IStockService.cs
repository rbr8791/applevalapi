using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IStockService
    {
        IEnumerable<Stock> GetAll();
        Stock GetById(int id);
        void Delete(int id);
        Stock FindByOrdinal(int id);
    }
}