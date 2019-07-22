using Microsoft.AspNetCore.Mvc;
using System.Linq;
using applevalApi.DAL;
using applevalApi.DTO.Helpers;
using applevalApi.DTO;
using applevalApi.DAL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;
using applevalApi.Helpers;

namespace applevalApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController]
    public class AuditLogPurchaseController : BaseController
    {
        private readonly IAuditLogPurchaseService _auditLogPurchaseService;
        private IMapper _mapper;
      

        public AuditLogPurchaseController(IAuditLogPurchaseService auditLogPurchaseService,
            IMapper mapper)
        {
            _auditLogPurchaseService = auditLogPurchaseService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.AuditLogPurchaseViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var auditLogPurchase = _auditLogPurchaseService.FindByOrdinal(id);
            if (auditLogPurchase == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(AuditLogPurchaseViewModelHelper.ConvertToViewModel(auditLogPurchase));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var auditLogPurchases = _auditLogPurchaseService.GetAll();
            var auditLogPurchasesDTO = _mapper.Map<IList<AuditLogPurchaseDto>>(auditLogPurchases);
            return Ok(auditLogPurchasesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var auditLogPurchase = _auditLogPurchaseService.GetById(id);
            var auditLogPurchaseDto = _mapper.Map<AuditLogPurchaseDto>(auditLogPurchase);
            return Ok(auditLogPurchaseDto);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _auditLogPurchaseService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace