using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;

namespace applevalApi.DTO.Helpers
{
    public static class CountryViewModelHelper
    {
        public static CountryViewModel ConvertToViewModel(Country dbModel)
        {
            var viewModel = new CountryViewModel
            {
                Id = dbModel.Id,
                ISOCountryCode = dbModel.ISOCountryCode,
                Name = dbModel.Name,
                Enabled = dbModel.Enabled,
            };

            return viewModel;
        }

        public static List<CountryViewModel> ConvertToViewModels(List<Country> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<CountryBaseViewModel> ConvertToBaseViewModels(List<Country> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static CountryBaseViewModel ConvertToBaseViewModel(Country dbModel)
        {
            var viewModel = new CountryBaseViewModel
            {
                Id = dbModel.Id,
                ISOCountryCode = dbModel.ISOCountryCode,
                Name = dbModel.Name,
                Enabled = dbModel.Enabled
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
