using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoice.BL.Interfaces;
using Invoice.WebApp.Mappers;
using Invoice.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.WebApp.Controllers
{
    public class InvoiceItemsController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceItemService _invoiceItemService;
        private readonly InvoiceItemsViewModelMapper _invoiceItemsViewModelMapper;

        public InvoiceItemsController(
            IInvoiceService invoiceService, IInvoiceItemService invoiceItemService, InvoiceItemsViewModelMapper invoiceItemsViewModelMapper)
        {
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _invoiceItemsViewModelMapper = invoiceItemsViewModelMapper;
        }
        
        public ActionResult Create(int invoiceId)
        {
            return View(new InvoiceItemsViewModel(invoiceId));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoiceItemsViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(vm);

                if (vm.InvoiceId == null)
                    return RedirectToAction(nameof(InvoicesController.Index), "Invoices");

                var invoice = await _invoiceService.GetAsync((int)vm.InvoiceId);

                if (invoice == null)
                    return RedirectToAction(nameof(InvoicesController.Edit), "Invoices", new { id = (int)vm.InvoiceId });

                var invoiceItem = _invoiceItemsViewModelMapper.GetDomainFromViewModel(vm, invoice);

                await _invoiceItemService.AddAsync(invoiceItem);

                return RedirectToAction(nameof(InvoicesController.Edit), "Invoices", new { id = (int)vm.InvoiceId });
            }
            catch
            {
                return View(vm);
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int invoiceId)
        {
            try
            {
                await _invoiceItemService.DeleteAsync(id);

                return RedirectToAction(nameof(InvoicesController.Edit), "Invoices", new { id = invoiceId });
            }
            catch
            {
                return RedirectToAction(nameof(InvoicesController.Edit), "Invoices", new { id = invoiceId });
            }
        }
    }
}