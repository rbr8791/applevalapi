using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class OrderDetailDto
    {
        public int Id { get; set; }

        public InvoiceDto invoice { get; set; }

        public ProductDto product { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }

       

        public static OrderDetailDto FromModel(OrderDetail model)
        {
            return new OrderDetailDto()
            {
                Id = model.Id,
                product = ProductDto.FromModel(model.product),
                Quantity = model.Quantity,
               
            };
        }

        public OrderDetail ToModel()
        {
            return new OrderDetail()
            {
                Id = Id,
                product = product.ToModel(),
                Quantity = Quantity,
               
            };
        }
    }
}
