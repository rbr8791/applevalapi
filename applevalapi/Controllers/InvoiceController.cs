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
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private IMapper _mapper;
      

        public InvoiceController(IInvoiceService invoiceService,
            IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.InvoiceViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var invoice = _invoiceService.FindByOrdinal(id);
            if (invoice == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(InvoiceViewModelHelper.ConvertToViewModel(invoice));
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var invoices = _invoiceService.GetAll();
            var invoicesDTO = _mapper.Map<IList<CustomerDto>>(invoices);
            return Ok(invoicesDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var invoice = _invoiceService.GetById(id);
            var invoiceDTO = _mapper.Map<CustomerDto>(invoice);
            return Ok(invoiceDTO);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _invoiceService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace