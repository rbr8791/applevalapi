using System;
using System.Collections.Generic;
using System.Text;
using applevalApi.DTO.Helpers.Infrastructure;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.Linq.Expressions;

namespace applevalApi.DTO.MapperObjects
{
    public class ActiveTokenDto
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CurrentToken { get; set; }
        public DateTime TokenDate { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string SourceIdentifier { get; set; }
        public bool Status { get; set; }
    }

    public class ActiveTokenMapper : MapperBase<ActiveToken, ActiveTokenDto>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<ActiveToken, ActiveTokenDto>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<ActiveToken, ActiveTokenDto>>)(p => new ActiveTokenDto()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    Id = p.Id,
                    UserName = p.UserName,
                    CurrentToken = p.CurrentToken,
                    TokenDate = p.TokenDate,
                    Status = p.Status
                }));
            }
        }

        public override void MapToModel(ActiveTokenDto dto, ActiveToken model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.Id = dto.Id;
            model.UserName = dto.UserName;
            model.CurrentToken = dto.CurrentToken;
            model.TokenDate = dto.TokenDate;
            model.Status = dto.Status;

        }
    }
}
