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
    public class StockMovementTypeController : BaseController
    {
        private readonly IStockMovementTypeService _stockMovementTypeService;
        private IMapper _mapper;
      

        public StockMovementTypeController(IStockMovementTypeService stockMovementTypeService,
            IMapper mapper)
        {
            _stockMovementTypeService = stockMovementTypeService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the stockMovementType in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.StockMovementTypeViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var stockMovementType = _stockMovementTypeService.FindByOrdinal(id);
            if (stockMovementType == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(StockMovementTypeViewModelHelper.ConvertToViewModel(stockMovementType));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var stockMovementTypes = _stockMovementTypeService.GetAll();
            var stockMovementTypesDTO = _mapper.Map<IList<StockMovementTypeDto>>(stockMovementTypes);
            return Ok(stockMovementTypesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stockMovementType = _stockMovementTypeService.GetById(id);
            var stockMovementTypeDTO = _mapper.Map<StockMovementTypeDto>(stockMovementType);
            return Ok(stockMovementTypeDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _stockMovementTypeService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace