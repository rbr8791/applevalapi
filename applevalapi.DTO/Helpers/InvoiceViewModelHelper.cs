using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.Helpers
{
    public static class InvoiceViewModelHelper
    {
        public static InvoiceViewModel ConvertToViewModel(Invoice dbModel)
        {
            var viewModel = new InvoiceViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                createdBy = dbModel.createdBy,
                Status = dbModel.Status,
            };

            return viewModel;   
        }

        public static List<InvoiceViewModel> ConvertToViewModels(List<Invoice> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<InvoiceBaseViewModel> ConvertToBaseViewModels(List<Invoice> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static InvoiceBaseViewModel ConvertToBaseViewModel(Invoice dbModel)
        {
            var viewModel = new InvoiceBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                createdBy = dbModel.createdBy,
                Status = dbModel.Status,
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
