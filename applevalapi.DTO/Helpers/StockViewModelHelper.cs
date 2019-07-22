using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class StockViewModelHelper
    {
        public static StockViewModel ConvertToViewModel(Stock dbModel)
        {
            var viewModel = new StockViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                product = ProductDto.FromModel(dbModel.product),
                Quantity = dbModel.Quantity,
                Status = dbModel.Status,
            };

            return viewModel;   
        }

        public static List<StockViewModel> ConvertToViewModels(List<Stock> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<StockBaseViewModel> ConvertToBaseViewModels(List<Stock> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static StockBaseViewModel ConvertToBaseViewModel(Stock dbModel)
        {
            var viewModel = new StockBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                product = ProductDto.FromModel(dbModel.product),
                Quantity = dbModel.Quantity,
                Status = dbModel.Status,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
