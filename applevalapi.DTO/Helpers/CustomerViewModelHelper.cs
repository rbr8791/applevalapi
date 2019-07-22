using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class CustomerViewModelHelper
    {
        public static CustomerViewModel ConvertToViewModel(Customer dbModel)
        {
            var viewModel = new CustomerViewModel
            {
                Id = dbModel.Id,
                user = UserDto.FromModel(dbModel.user),
                country = CountryDto.FromModel(dbModel.country),
                Address1 = dbModel.Address1,
                Address2 = dbModel.Address2,
                PaymentMethod = dbModel.PaymentMethod,
                Enabled = dbModel.Enabled,
            };

            return viewModel;   
        }

        public static List<CustomerViewModel> ConvertToViewModels(List<Customer> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<CustomerBaseViewModel> ConvertToBaseViewModels(List<Customer> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static CustomerBaseViewModel ConvertToBaseViewModel(Customer dbModel)
        {
            var viewModel = new CustomerBaseViewModel
            {
                Id = dbModel.Id,
                user = UserDto.FromModel(dbModel.user),
                country = CountryDto.FromModel(dbModel.country),
                Address1 = dbModel.Address1,
                Address2 = dbModel.Address2,
                PaymentMethod = dbModel.PaymentMethod,
                Enabled = dbModel.Enabled,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
