using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;

namespace applevalApi.DTO.Helpers
{
    public static class AuditLogPurchaseViewModelHelper
    {
        public static AuditLogPurchaseViewModel ConvertToViewModel(AuditLogPurchase dbModel)
        {
            var viewModel = new AuditLogPurchaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                RawParams = dbModel.RawParams,
                EventType = dbModel.EventType,
                Customer = dbModel.Customer,
                PurchaseDate = dbModel.PurchaseDate,
                Quantity = dbModel.Quantity,
                product = dbModel.product,
                UserAudited = dbModel.UserAudited
            };

            return viewModel;
        }

        public static List<AuditLogPurchaseViewModel> ConvertToViewModels(List<AuditLogPurchase> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<AuditLogPurchaseBaseViewModel> ConvertToBaseViewModels(List<AuditLogPurchase> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static AuditLogPurchaseBaseViewModel ConvertToBaseViewModel(AuditLogPurchase dbModel)
        {
            var viewModel = new AuditLogPurchaseBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                RawParams = dbModel.RawParams,
                EventType = dbModel.EventType,
                Customer = dbModel.Customer,
                PurchaseDate = dbModel.PurchaseDate,
                Quantity = dbModel.Quantity,
                product = dbModel.product,
                UserAudited = dbModel.UserAudited
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
