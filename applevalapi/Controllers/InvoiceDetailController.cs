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
    public class InvoiceDetailController : BaseController
    {
        private readonly IInvoiceDetailService _invoiceDetailService;
        private IMapper _mapper;
      

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService,
            IMapper mapper)
        {
            _invoiceDetailService = invoiceDetailService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.InvoiceDetailViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var invoiceDetail = _invoiceDetailService.FindByOrdinal(id);
            if (invoiceDetail == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(InvoiceDetailViewModelHelper.ConvertToViewModel(invoiceDetail));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var invoiceDetails = _invoiceDetailService.GetAll();
            var invoiceDetailsDTO = _mapper.Map<IList<InvoiceDetailDto>>(invoiceDetails);
            return Ok(invoiceDetailsDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var invoiceDetail = _invoiceDetailService.GetById(id);
            var invoiceDetailDTO = _mapper.Map<InvoiceDetailDto>(invoiceDetail);
            return Ok(invoiceDetailDTO);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _invoiceDetailService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace