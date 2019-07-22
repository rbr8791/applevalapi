using System.Collections.Generic;
using applevalApi.Entities;

namespace applevalApi.DAL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Delete(int id);
        Order FindByOrdinal(int id);

        //Order Create(Order order, IEnumerable<OrderDetail> details, User user);
        Order Create(Order order, User user);
    }
}