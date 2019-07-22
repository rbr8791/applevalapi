using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        void Delete(int id);
        OrderDetail FindByOrdinal(int id);
    }
}