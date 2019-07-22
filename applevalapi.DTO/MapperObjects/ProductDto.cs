using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public float? Price { get; set; }

        public int? Likes { get; set; }

        public bool? Status { get; set; } 

        public static ProductDto FromModel(Product model)
        {
            return new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Price = model.Price,
                Likes = model.Likes,
                Status = model.Status,
            };
        }

        public Product ToModel()
        {
            return new Product()
            {
                Id = Id,
                Description = Description,
                Price = Price,
                Likes = Likes,
                Status = Status,
            };
        }
    }
}
