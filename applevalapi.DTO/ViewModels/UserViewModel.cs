using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class UserViewModel : BaseViewModel
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


    }
}