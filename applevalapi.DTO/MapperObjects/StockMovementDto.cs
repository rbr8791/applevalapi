using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class StockMovementDto
    {
        public int Id { get; set; }

        public ProductDto product { get; set; }

        public string DocumentID { get; set; }

        public int Quantity { get; set; }

        public StockMovementTypeDto StockMovementType { get; set; }

        

        public static StockMovementDto FromModel(StockMovement model)
        {
            return new StockMovementDto()
            {
                Id = model.Id,
                product = ProductDto.FromModel(model.product),
                DocumentID = model.DocumentID,
                Quantity = model.Quantity,
                StockMovementType = StockMovementTypeDto.FromModel(model.StockMovementType),
                
            };
        }

        public StockMovement ToModel()
        {
            return new StockMovement()
            {
                Id = Id,
                product = product.ToModel(),
                DocumentID = DocumentID,
                Quantity = Quantity,
                StockMovementType = StockMovementType.ToModel(),
                
            };
        }
    }
}
