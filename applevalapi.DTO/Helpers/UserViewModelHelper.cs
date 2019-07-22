using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class UserViewModelHelper
    {
        public static UserViewModel ConvertToViewModel(User dbModel)
        {
            var viewModel = new UserViewModel
            {
                Id = dbModel.Id,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                Username = dbModel.Username,
                Password = dbModel.Password,
                PasswordHash = dbModel.PasswordHash.ToArray(),
                PasswordSalt = dbModel.PasswordSalt.ToArray(),
                Role = RoleDto.FromModel(dbModel.Role),
                Enabled = dbModel.Enabled,
            };

            return viewModel;   
        }

        public static List<UserViewModel> ConvertToViewModels(List<User> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<UserBaseViewModel> ConvertToBaseViewModels(List<User> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static UserBaseViewModel ConvertToBaseViewModel(User dbModel)
        {
            var viewModel = new UserBaseViewModel
            {
                Id = dbModel.Id,
                FirstName = dbModel.FirstName,
                LastName = dbModel.LastName,
                Username = dbModel.Username,
                Password = dbModel.Password,
                PasswordHash = dbModel.PasswordHash.ToArray(),
                PasswordSalt = dbModel.PasswordSalt.ToArray(),
                Role = RoleDto.FromModel(dbModel.Role),
                Enabled = dbModel.Enabled,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
