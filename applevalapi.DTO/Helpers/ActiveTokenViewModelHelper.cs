using System;
using System.Collections.Generic;
using System.Linq;
using applevalApi.DTO.ViewModels;
using applevalApi.Entities;

namespace applevalApi.DTO.Helpers
{
    public static class ActiveTokenViewModelHelper
    {
        public static ActiveTokenViewModel ConvertToViewModel(ActiveToken dbModel)
        {
            var viewModel = new ActiveTokenViewModel
            {
                Id = dbModel.Id,
                UserName = dbModel.UserName,
                CurrentToken = dbModel.CurrentToken,
                TokenDate = dbModel.TokenDate,
                TokenExpireDate = dbModel.TokenExpireDate,
                SourceIdentifier = dbModel.SourceIdentifier,
                Status = dbModel.Status
            };

            return viewModel;
        }

        public static List<ActiveTokenViewModel> ConvertToViewModels(List<ActiveToken> dbModel)
        {
            return dbModel.Select(ConvertToViewModel).ToList();
        }

        public static List<ActiveTokenBaseViewModel> ConvertToBaseViewModels(List<ActiveToken> dbModel)
        {
            return dbModel.Select(ConvertToBaseViewModel).ToList();
        }

       
        private static ActiveTokenBaseViewModel ConvertToBaseViewModel(ActiveToken dbModel)
        {
            var viewModel = new ActiveTokenBaseViewModel
            {
                Id = dbModel.Id,
                UserName = dbModel.UserName,
                CurrentToken = dbModel.CurrentToken,
                TokenDate = dbModel.TokenDate,
                TokenExpireDate = dbModel.TokenExpireDate,
                SourceIdentifier = dbModel.SourceIdentifier,
                Status = dbModel.Status
            };


            return viewModel;
        }

       

    } // End Class
        
}// End Namespace
        
