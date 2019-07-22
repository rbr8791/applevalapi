using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;

namespace applevalApi.DTO.Helpers
{
    public static class AuditLogViewModelHelper
    {
        public static AuditLogViewModel ConvertToViewModel(AuditLog dbModel)
        {
            var viewModel = new AuditLogViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                RawParams = dbModel.RawParams,
                EventType = dbModel.EventType,
                CreationDate = dbModel.CreationDate,
                UserAudited = dbModel.UserAudited
            };

            return viewModel;
        }

        public static List<AuditLogViewModel> ConvertToViewModels(List<AuditLog> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<AuditLogBaseViewModel> ConvertToBaseViewModels(List<AuditLog> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static AuditLogBaseViewModel ConvertToBaseViewModel(AuditLog dbModel)
        {
            var viewModel = new AuditLogBaseViewModel
            {
                Id = dbModel.Id,
                Description = dbModel.Description,
                RawParams = dbModel.RawParams,
                EventType = dbModel.EventType,
                CreationDate = dbModel.CreationDate,
                UserAudited = dbModel.UserAudited
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
