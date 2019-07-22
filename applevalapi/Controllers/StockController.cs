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
    public class StockController : BaseController
    {
        private readonly IStockService _stockService;
        private IMapper _mapper;
      

        public StockController(IStockService stockService,
            IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the stock in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.StockViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var stock = _stockService.FindByOrdinal(id);
            if (stock == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(StockViewModelHelper.ConvertToViewModel(stock));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _stockService.GetAll();
            var stocksDTO = _mapper.Map<IList<StockDto>>(stocks);
            return Ok(stocksDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _stockService.GetById(id);
            var stockDTO = _mapper.Map<StockDto>(stock);
            return Ok(stockDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _stockService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace