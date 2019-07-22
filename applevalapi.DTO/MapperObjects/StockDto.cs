using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class StockDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ProductDto product { get; set; }

        public int Quantity { get; set; }

        
        public bool Status { get; set; }

        public static StockDto FromModel(Stock model)
        {
            return new StockDto()
            {
                Id = model.Id,
                Description = model.Description,
                product = ProductDto.FromModel(model.product),
                Quantity = model.Quantity,
                Status = model.Status,
            };
        }

        public Stock ToModel()
        {
            return new Stock()
            {
                Id = Id,
                Description = Description,
                product = product.ToModel(),
                Quantity = Quantity,
                Status = Status,
            };
        }
    }
}
