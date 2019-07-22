using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class InvoiceDetailViewModelHelper
    {
        public static InvoiceDetailViewModel ConvertToViewModel(InvoiceDetail dbModel)
        {
            var viewModel = new InvoiceDetailViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                invoice = InvoiceDto.FromModel(dbModel.invoice),
                Quantity = dbModel.Quantity,
                Price = dbModel.Price
            };

            return viewModel;   
        }

        public static List<InvoiceDetailViewModel> ConvertToViewModels(List<InvoiceDetail> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<InvoiceDetailBaseViewModel> ConvertToBaseViewModels(List<InvoiceDetail> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static InvoiceDetailBaseViewModel ConvertToBaseViewModel(InvoiceDetail dbModel)
        {
            var viewModel = new InvoiceDetailBaseViewModel
            {
                Id = dbModel.Id,
                product = ProductDto.FromModel(dbModel.product),
                invoice = InvoiceDto.FromModel(dbModel.invoice),
                Quantity = dbModel.Quantity,
                Price = dbModel.Price
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
