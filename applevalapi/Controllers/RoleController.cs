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

namespace applevalApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private IMapper _mapper;
      

        public RoleController(IRoleService roleService,
            IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the role in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.RoleViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var role = _roleService.FindByOrdinal(id);
            if (role == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(RoleViewModelHelper.ConvertToViewModel(role));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            var rolesDTO = _mapper.Map<IList<RoleDto>>(roles);
            return Ok(rolesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _roleService.GetById(id);
            var roleDTO = _mapper.Map<RoleDto>(role);
            return Ok(roleDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace