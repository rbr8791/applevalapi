using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class OrderViewModelHelper
    {
        public static OrderViewModel ConvertToViewModel(Order dbModel)
        {
            var viewModel = new OrderViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                OrderNumber = dbModel.OrderNumber,
                createdBy = dbModel.createdBy,
                Status = dbModel.Status,
            };

            return viewModel;   
        }

        public static List<OrderViewModel> ConvertToViewModels(List<Order> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<OrderBaseViewModel> ConvertToBaseViewModels(List<Order> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static OrderBaseViewModel ConvertToBaseViewModel(Order dbModel)
        {
            var viewModel = new OrderBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                OrderNumber = dbModel.OrderNumber,
                createdBy = dbModel.createdBy,
                Status = dbModel.Status,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
