using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class AuditLogPurchaseDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string RawParams { get; set; }

        public string EventType { get; set; }

        public User Customer { get; set; }

        public DateTime? PurchaseDate { get; set; } = null;

        public int? Quantity { get; set; } = null;

        public Product product { get; set; } 

        public string UserAudited { get; set; }

        public static AuditLogPurchaseDto FromModel(AuditLogPurchase model)
        {
            return new AuditLogPurchaseDto()
            {
                Id = model.Id,
                Description = model.Description,
                RawParams = model.RawParams,
                EventType = model.EventType,
                Customer = model.Customer,
                PurchaseDate = model.PurchaseDate,
                Quantity = model.Quantity,
                product = model.product,
                UserAudited = model.UserAudited,
            };
        }

        public AuditLogPurchase ToModel()
        {
            return new AuditLogPurchase()
            {
                Id = Id,
                Description = Description,
                RawParams = RawParams,
                EventType = EventType,
                Customer = Customer,
                PurchaseDate = PurchaseDate,
                Quantity = Quantity,
                product = product,
                UserAudited = UserAudited,
            };
        }
    }
}
