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
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private IMapper _mapper;
      

        public OrderController(IOrderService orderService, IUserService userService,
            IMapper mapper)
        {
            _orderService = orderService;
            _userService = userService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.OrderViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var order = _orderService.FindByOrdinal(id);
            if (order == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(OrderViewModelHelper.ConvertToViewModel(order));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAll();
            var ordersDTO = _mapper.Map<IList<CustomerDto>>(orders);
            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetById(id);
            var orderDTO = _mapper.Map<CustomerDto>(order);
            return Ok(orderDTO);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody]OrderDto orderDto)
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            Guid gORderNumber = Guid.NewGuid();

            string OrderNumber = gORderNumber.ToString();

            //var userId = int.Parse(context.Principal.Identity.Name);
            var user = _userService.GetById(int.Parse(userId));


            try
            {
                Order order = orderDto.ToModel();

                // Audit fields
                order.createdBy = user.Username;
                order.Created = DateTime.Now;
                order.OrderNumber = OrderNumber;

                Order returnedOrder = _orderService.Create(order, user);

                OrderDto r = OrderDto.FromModel(returnedOrder);
                return Ok(r);

            }
            catch (AppException ex)
            {
                //var Message = ex.Message;
                return ErrorResponse(ex.Message);
                //return BadRequest(new  { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace