using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IStockMovementTypeService
    {
        IEnumerable<StockMovementType> GetAll();
        StockMovementType GetById(int id);
        void Delete(int id);
        StockMovementType FindByOrdinal(int id);
    }
}