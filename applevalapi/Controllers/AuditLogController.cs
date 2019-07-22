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
    public class AuditLogController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private IMapper _mapper;
      

        public AuditLogController(IAuditLogService auditLogService,
            IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.AuditLogViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var auditLog = _auditLogService.FindByOrdinal(id);
            if (auditLog == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(AuditLogViewModelHelper.ConvertToViewModel(auditLog));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var auditLogs = _auditLogService.GetAll();
            var auditLogsDTO = _mapper.Map<IList<AuditLogDto>>(auditLogs);
            return Ok(auditLogsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var activeToken = _auditLogService.GetById(id);
            var activeTokenDTO = _mapper.Map<AuditLogDto>(activeToken);
            return Ok(activeTokenDTO);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _auditLogService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace