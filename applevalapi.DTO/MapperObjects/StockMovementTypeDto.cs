using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class StockMovementTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static StockMovementTypeDto FromModel(StockMovementType model)
        {
            return new StockMovementTypeDto()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public StockMovementType ToModel()
        {
            return new StockMovementType()
            {
                Id = Id,
                Name = Name,
            };
        }
    }
}
