using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class RoleDto
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public bool Status { get; set; }

        public static RoleDto FromModel(Role model)
        {
            return new RoleDto()
            {
                Id = model.Id,
                RoleName = model.RoleName,
                Status = model.Status,
            };
        }

        public Role ToModel()
        {
            return new Role()
            {
                Id = Id,
                RoleName = RoleName,
                Status = Status,
            };
        }
    }
}
