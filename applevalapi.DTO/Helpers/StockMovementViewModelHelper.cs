using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class StockMovementViewModelHelper
    {
        public static StockMovementViewModel ConvertToViewModel(StockMovement dbModel)
        {
            var viewModel = new StockMovementViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                DocumentID = dbModel.DocumentID,
                Quantity = dbModel.Quantity,
                StockMovementType = StockMovementTypeDto.FromModel(dbModel.StockMovementType),
                createdBy = dbModel.createdBy,
            };

            return viewModel;   
        }

        public static List<StockMovementViewModel> ConvertToViewModels(List<StockMovement> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<StockMovementBaseViewModel> ConvertToBaseViewModels(List<StockMovement> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static StockMovementBaseViewModel ConvertToBaseViewModel(StockMovement dbModel)
        {
            var viewModel = new StockMovementBaseViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                DocumentID = dbModel.DocumentID,
                Quantity = dbModel.Quantity,
                StockMovementType = StockMovementTypeDto.FromModel(dbModel.StockMovementType),
                createdBy = dbModel.createdBy,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
