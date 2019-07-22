using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        

        public bool Status { get; set; }

        public static InvoiceDto FromModel(Invoice model)
        {
            return new InvoiceDto()
            {
                Id = model.Id,
                Description = model.Description,
                
                Status = model.Status,
            };
        }

        public Invoice ToModel()
        {
            return new Invoice()
            {
                Id = Id,
                Description = Description,
                
                Status = Status,
            };
        }
    }
}
