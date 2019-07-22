using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class RoleViewModelHelper
    {
        public static RoleViewModel ConvertToViewModel(Role dbModel)
        {
            var viewModel = new RoleViewModel
            {
                Id = dbModel.Id,
                RoleName = dbModel.RoleName,
                Status = dbModel.Status,
            };

            return viewModel;   
        }

        public static List<RoleViewModel> ConvertToViewModels(List<Role> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<RoleBaseViewModel> ConvertToBaseViewModels(List<Role> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static RoleBaseViewModel ConvertToBaseViewModel(Role dbModel)
        {
            var viewModel = new RoleBaseViewModel
            {
                Id = dbModel.Id,
                RoleName = dbModel.RoleName,
                Status = dbModel.Status,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
