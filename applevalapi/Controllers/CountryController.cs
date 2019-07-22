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
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        private IMapper _mapper;
      

        public CountryController(ICountryService countryService,
            IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.CountryViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var country = _countryService.FindByOrdinal(id);
            if (country == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(CountryViewModelHelper.ConvertToViewModel(country));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var countrys = _countryService.GetAll();
            var countrysDTO = _mapper.Map<IList<CountryDto>>(countrys);
            return Ok(countrysDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var country = _countryService.GetById(id);
            var countryDTO = _mapper.Map<CountryDto>(country);
            return Ok(countryDTO);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _countryService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace