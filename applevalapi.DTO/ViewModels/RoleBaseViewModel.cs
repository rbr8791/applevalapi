using System;
using applevalApi.DTO.MapperObjects;

namespace applevalApi.DTO.ViewModels
{
    public class RoleBaseViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public bool Status { get; set; }
    }
}