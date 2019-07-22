using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class OrderDetailViewModelHelper
    {
        public static OrderDetailViewModel ConvertToViewModel(OrderDetail dbModel)
        {
            var viewModel = new OrderDetailViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                Quantity = dbModel.Quantity,
                
            };

            return viewModel;   
        }

        public static List<OrderDetailViewModel> ConvertToViewModels(List<OrderDetail> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<OrderDetailBaseViewModel> ConvertToBaseViewModels(List<OrderDetail> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static OrderDetailBaseViewModel ConvertToBaseViewModel(OrderDetail dbModel)
        {
            var viewModel = new OrderDetailBaseViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                Quantity = dbModel.Quantity,
                
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
