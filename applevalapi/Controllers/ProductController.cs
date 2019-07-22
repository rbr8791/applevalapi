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
using System.Security.Principal;
using applevalApi.Helpers;
using Newtonsoft.Json;

namespace applevalApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private IMapper _mapper;
     
        public ProductController(IProductService productService,
            IMapper mapper, IUserService uService)
        {
            _productService = productService;
            _mapper = mapper;
            _userService = uService;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the product in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.ProductViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var product = _productService.FindByOrdinal(id);
            if (product == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(ProductViewModelHelper.ConvertToViewModel(product));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            var productsDTO = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDTO);
        }

        [HttpGet("Get/FindProductByName")]
        public IActionResult FindProductByName(string ProductName)
        {
            var products = _productService.FindByName(ProductName);
            var productsDTO = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDTO);
        }

        [HttpGet("Get/GetAllSortedByNameAndLikes")]
        public IActionResult GetAllSortedByNameAndLikes()
        {
            var products = _productService.GetAllSortedByNameAndLikes();
            var productsDTO = _mapper.Map<IList<ProductDto>>(products);
            return Ok(productsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            var productDTO = _mapper.Map<ProductDto>(product);
            return Ok(productDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Used to create a new product
        /// </summary>
        /// <param name="productDto">The Data transfromation object parameter</param>
        /// <returns>
        /// is returned, which contains a <see cref="applevalApi.DTO.MapperObjects.ProductDto"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpPost("Create")]
        public IActionResult Create([FromBody]ProductDto productDto)
        {
            
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            //var userId = int.Parse(context.Principal.Identity.Name);
            var user = _userService.GetById(int.Parse(userId));

           
            try
            {
                Product prod = productDto.ToModel();

                // Audit fields
                prod.createdBy = user.Username;
                prod.Created = DateTime.Now;
                
                Product returnedProduct = _productService.Create(prod, user);

                ProductDto r = ProductDto.FromModel(returnedProduct);
                return Ok(r);
               
            }
            catch (AppException ex)
            {
                //var Message = ex.Message;
                return ErrorResponse (ex.Message);
                //return BadRequest(new  { message = ex.Message });
            }
        }

        /// <summary>
        /// Used to update a new product
        /// </summary>
        /// <param name="productDto">The Data transfromation object parameter</param>
        /// <returns>
        /// is returned, which contains a <see cref="applevalApi.DTO.MapperObjects.ProductDto"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpPost("Update")]
        public IActionResult Update([FromBody]ProductDto productDto)
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var user = _userService.GetById(int.Parse(userId));


            try
            {
                var productParams = _mapper.Map<Product>(productDto);
                Product returnedProduct = _productService.Update(productParams, user);
                ProductDto r = ProductDto.FromModel(returnedProduct);

                if (r.Id == -1)
                {
                    throw new AppException(r.Description);
                }
                return Ok(r);

            }
            catch (AppException ex)
            {
                //var Message = ex.Message;
                return ErrorResponse(ex.Message);
                //return BadRequest(new  { message = ex.Message });
            }


           
           

            //try
            //{
              
            //}
            //catch (AppException ex)
            //{
            //    return BadRequest(new { message = ex.Message });
            //}


        }


        [HttpPost("UpdatePriceById")]
        public IActionResult UpdatePriceById([FromBody]ProductDto productDto)
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var user = _userService.GetById(int.Parse(userId));


            try
            {
                var productParams = _mapper.Map<Product>(productDto);
                Product returnedProduct = _productService.Update(productParams, user);
                ProductDto r = ProductDto.FromModel(returnedProduct);

                if (r.Id == -1)
                {
                    throw new AppException(r.Description);
                }
                return Ok(r);

            }
            catch (AppException ex)
            {
                //var Message = ex.Message;
                return ErrorResponse(ex.Message);
                //return BadRequest(new  { message = ex.Message });
            }


           
           

            //try
            //{
              
            //}
            //catch (AppException ex)
            //{
            //    return BadRequest(new { message = ex.Message });
            //}


        }

        /// <summary>
        /// Used to update a new product
        /// </summary>
        /// <param name="productDto">The Data transfromation object parameter</param>
        /// <returns>
        /// is returned, which contains a <see cref="applevalApi.DTO.MapperObjects.ProductDto"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpPost("LikeProduct")]
        public IActionResult LikeProductById([FromBody]ProductDto productDto)
        {

            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var user = _userService.GetById(int.Parse(userId));


            try
            {
                var productParams = _mapper.Map<Product>(productDto);
                Product returnedProduct = _productService.LikeProductById(productParams, user);
                ProductDto r = ProductDto.FromModel(returnedProduct);

                if (r.Id == -1)
                {
                    throw new AppException(r.Description);
                }
                return Ok(r);

            }
            catch (AppException ex)
            {
                //var Message = ex.Message;
                return ErrorResponse(ex.Message);
                //return BadRequest(new  { message = ex.Message });
            }





            //try
            //{

            //}
            //catch (AppException ex)
            //{
            //    return BadRequest(new { message = ex.Message });
            //}


        }





    } // End Class
} // End Namespace