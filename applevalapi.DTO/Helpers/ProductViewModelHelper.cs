using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class ProductViewModelHelper
    {
        public static ProductViewModel ConvertToViewModel(Product dbModel)
        {
            var viewModel = new ProductViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                Price = dbModel.Price,
                Likes = dbModel.Likes,
                Status = dbModel.Status,
            };

            return viewModel;   
        }

        public static List<ProductViewModel> ConvertToViewModels(List<Product> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<ProductBaseViewModel> ConvertToBaseViewModels(List<Product> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static ProductBaseViewModel ConvertToBaseViewModel(Product dbModel)
        {
            var viewModel = new ProductBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                Price = dbModel.Price,
                Likes = dbModel.Likes,
                Status = dbModel.Status,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
