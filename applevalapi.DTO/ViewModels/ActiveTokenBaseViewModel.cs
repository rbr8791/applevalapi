using System;

namespace applevalApi.DTO.ViewModels
{
    public class ActiveTokenBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CurrentToken { get; set; }
        public DateTime TokenDate { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string SourceIdentifier { get; set; }
        public bool Status { get; set; }
    }
}