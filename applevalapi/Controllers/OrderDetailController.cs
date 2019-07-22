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
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;
        private IMapper _mapper;
      

        public OrderDetailController(IOrderDetailService orderDetailService,
            IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.OrderDetailViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var orderDetail = _orderDetailService.FindByOrdinal(id);
            if (orderDetail == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(OrderDetailViewModelHelper.ConvertToViewModel(orderDetail));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetails = _orderDetailService.GetAll();
            var orderDetailsDTO = _mapper.Map<IList<OrderDetailDto>>(orderDetails);
            return Ok(orderDetailsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orderDetail = _orderDetailService.GetById(id);
            var orderDetailDTO = _mapper.Map<OrderDetailDto>(orderDetail);
            return Ok(orderDetailDTO);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderDetailService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace