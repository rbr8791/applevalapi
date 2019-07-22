using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace applevalApi.DTO.MapperObjects
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string OrderNumber { get; set; } = null;

        public IEnumerable<OrderDetail> Details { get; set; } = new Collection<OrderDetail>();
        public bool Status { get; set; }

        public static OrderDto FromModel(Order model)
        {
            return new OrderDto()
            {
                Id = model.Id,
                Description = model.Description,
                OrderNumber = model.OrderNumber,
                Details = model.Details,
                Status = model.Status,
            };
        }

        public Order ToModel()
        {
            return new Order()
            {
                Id = Id,
                Description = Description,
                OrderNumber = OrderNumber,
                Details = Details,
                Status = Status,
            };
        }
    }
}
