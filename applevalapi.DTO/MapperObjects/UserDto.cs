using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;
using System.Linq;

namespace applevalApi.DTO.MapperObjects
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public RoleDto Role { get; set; }

        public bool Enabled { get; set; }

        public static UserDto FromModel(User model)
        {
            return new UserDto()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                PasswordHash = model.PasswordHash.ToArray(),
                PasswordSalt = model.PasswordSalt.ToArray(),
                Role = RoleDto.FromModel(model.Role),
                Enabled = model.Enabled,
            };
        }

        public User ToModel()
        {
            return new User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Password = Password,
                PasswordHash = PasswordHash.ToArray(),
                PasswordSalt = PasswordSalt.ToArray(),
                Role = Role.ToModel(),
                Enabled = Enabled,
            };
        }
    }
}
