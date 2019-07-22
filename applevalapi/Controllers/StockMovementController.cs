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
    public class StockMovementController : BaseController
    {
        private readonly IStockMovementService _stockMovementService;
        private IMapper _mapper;
      

        public StockMovementController(IStockMovementService stockMovementService,
            IMapper mapper)
        {
            _stockMovementService = stockMovementService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the stockMovement in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.StockMovementViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var stockMovement = _stockMovementService.FindByOrdinal(id);
            if (stockMovement == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(StockMovementViewModelHelper.ConvertToViewModel(stockMovement));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var stockMovements = _stockMovementService.GetAll();
            var stockMovementsDTO = _mapper.Map<IList<CustomerDto>>(stockMovements);
            return Ok(stockMovementsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stockMovement = _stockMovementService.GetById(id);
            var stockMovementDTO = _mapper.Map<CustomerDto>(stockMovement);
            return Ok(stockMovementDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _stockMovementService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace