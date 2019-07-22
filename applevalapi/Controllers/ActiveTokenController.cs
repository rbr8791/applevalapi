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
using applevalApi.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace applevalApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController]
    public class ActiveTokenController : BaseController
    {
        private readonly IActiveTokenService _activeTokenService;
        private IMapper _mapper;
      

        public ActiveTokenController(IActiveTokenService activeTokenService,
            IMapper mapper)
        {
            _activeTokenService = activeTokenService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.ActiveTokenViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var activeToken = _activeTokenService.FindByOrdinal(id);
            if (activeToken == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(ActiveTokenViewModelHelper.ConvertToViewModel(activeToken));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var activeTokens = _activeTokenService.GetAll();
            var activeTokensDTO = _mapper.Map<IList<ActiveTokenDto>>(activeTokens);
            return Ok(activeTokensDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var activeToken = _activeTokenService.GetById(id);
            var activeTokenDTO = _mapper.Map<ActiveTokenDto>(activeToken);
            return Ok(activeTokenDTO);
        }

        [HttpGet("GetTopOneActiveTokenBySourceIdentifier/{sourceIdentifier}")]
        public IActionResult GetTopOneActiveTokenBySourceIdentifier(string sourceIdentifier)
        {
            var activeTokens = _activeTokenService.GetTopOneActiveTokenBySourceIdentifier(sourceIdentifier);
            var activeTokensDTO = _mapper.Map<IList<ActiveTokenDto>>(activeTokens);
            return Ok(activeTokensDTO);
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody]ActiveTokenDto activeTokenDto)
        {
            var activeToken = _mapper.Map<ActiveToken>(activeTokenDto);
            try
            {
                var newActiveToken = _activeTokenService.Create(activeToken, activeTokenDto.UserName,
                     activeTokenDto.CurrentToken, activeTokenDto.SourceIdentifier);
                return Ok(new
                {
                    message = "Ok",
                    newActiveTokenId = newActiveToken.Id,
                    bearerToken = newActiveToken.CurrentToken
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ActiveTokenDto activeTokenDto)
        {
            var activeToken = _mapper.Map<ActiveToken>(activeTokenDto);
            activeToken.Id = id;

            try
            {
                _activeTokenService.Update(activeToken, activeTokenDto.CurrentToken, activeTokenDto.SourceIdentifier);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _activeTokenService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace