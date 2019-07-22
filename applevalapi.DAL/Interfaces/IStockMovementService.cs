using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IStockMovementService
    {
        IEnumerable<StockMovement> GetAll();
        StockMovement GetById(int id);
        void Delete(int id);
        StockMovement FindByOrdinal(int id);
    }
}