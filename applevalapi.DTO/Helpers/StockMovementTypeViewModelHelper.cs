using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class StockMovementTypeViewModelHelper
    {
        public static StockMovementTypeViewModel ConvertToViewModel(StockMovementType dbModel)
        {
            var viewModel = new StockMovementTypeViewModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
            };

            return viewModel;   
        }

        public static List<StockMovementTypeViewModel> ConvertToViewModels(List<StockMovementType> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<StockMovementTypeBaseViewModel> ConvertToBaseViewModels(List<StockMovementType> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static StockMovementTypeBaseViewModel ConvertToBaseViewModel(StockMovementType dbModel)
        {
            var viewModel = new StockMovementTypeBaseViewModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
