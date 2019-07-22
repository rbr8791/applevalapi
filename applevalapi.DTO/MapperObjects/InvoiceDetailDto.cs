using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }

        public ProductDto product { get; set; }

        public InvoiceDto invoice { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }

        

        public static InvoiceDetailDto FromModel(InvoiceDetail model)
        {
            return new InvoiceDetailDto()
            {
                Id = model.Id,
                product = ProductDto.FromModel(model.product),
                invoice = InvoiceDto.FromModel(model.invoice),
                Quantity = model.Quantity,
                Price = model.Price,
               
               
            };
        }

        public InvoiceDetail ToModel()
        {
            return new InvoiceDetail()
            {
                Id = Id,
                product = product.ToModel(),
                invoice = invoice.ToModel(),
                Quantity = Quantity,
                Price = Price,
               
            };
        }
    }
}
