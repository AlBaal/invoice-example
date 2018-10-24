using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invoice.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "ApiKeyAuthScheme")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoicesController(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("unpaid")]
        public async Task<IActionResult> GetUnpaid()
        {
            try
            {
                var result = await _invoiceService.GetAllActivePendingAsync();
                return Ok(result);
            }
            catch
            {
                throw new Exception("Getting a invoice failed.");
            }
        }

        [HttpPost]
        [Route("set-paid/{id}")]
        public async Task<ActionResult> SetPaid(int id)
        {
            if (id == 0)
                return BadRequest();

            var invoice = await _invoiceService.GetAsync(id);

            if (invoice == null)
                return NotFound();

            invoice.PaymentStatus = BL.Models.PaymentStatus.Paid;

            if (!await _invoiceService.UpdateAsync(invoice))
                throw new Exception("Updating a invoice payment status failed on save.");

            return Ok(invoice);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody]BL.Models.Invoice patch)
        {
            if (patch == null)
                return BadRequest();

            var existingInvoice = await _invoiceService.GetAsync(id);

            if (existingInvoice == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _mapper.Map(patch, existingInvoice);
            }
            catch
            {
                throw new Exception("Updating a invoice failed while mapping.");
            }

            if (!await _invoiceService.UpdateAsync(existingInvoice))
                throw new Exception("Updating a invoice failed on save.");

            return Ok(existingInvoice);
        }
    }
}