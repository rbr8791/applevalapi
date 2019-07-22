using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class AuditLogDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string RawParams { get; set; }

        public string EventType { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserAudited { get; set; }

        public static AuditLogDto FromModel(AuditLog model)
        {
            return new AuditLogDto()
            {
                Id = model.Id,
                Description = model.Description,
                RawParams = model.RawParams,
                EventType = model.EventType,
                CreationDate = model.CreationDate,
                UserAudited = model.UserAudited,
            };
        }

        public AuditLog ToModel()
        {
            return new AuditLog()
            {
                Id = Id,
                Description = Description,
                EventType = EventType,
                CreationDate = CreationDate,
                UserAudited = UserAudited,
            };
        }
    }
}
