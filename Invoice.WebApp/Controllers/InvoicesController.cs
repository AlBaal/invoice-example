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
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IInvoiceItemService _invoiceItemService;
        private readonly IPartnerService _partnerService;
        private readonly InvoicesViewModelMapper _invoicesViewModelMapper;
        private readonly InvoiceItemsViewModelMapper _invoiceItemsViewModelMapper;

        public InvoicesController(
            IInvoiceService invoiceService, IInvoiceItemService invoiceItemService, IPartnerService partnerService, 
            InvoicesViewModelMapper invoicesViewModelMapper, InvoiceItemsViewModelMapper invoiceItemsViewModelMapper)
        {
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _partnerService = partnerService;
            _invoicesViewModelMapper = invoicesViewModelMapper;
            _invoiceItemsViewModelMapper = invoiceItemsViewModelMapper;
        }

        public async Task<ActionResult> Index()
        {
            var invoices = await _invoiceService.GetAllActiveAsync();

            var vm = new InvoicesIndexViewModel();
            vm.Invoices = invoices.Select(a => _invoicesViewModelMapper.GetViewModelFromDomain(a)).ToList();

            return View(vm);
        }
        
        public async Task<ActionResult> Create()
        {
            ViewBag.Partners = await _partnerService.GetAllAsync() ?? new List<BL.Models.Partner>();
            return View(new InvoicesViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoicesViewModel vm)
        {
            try
            {
                if (vm.PartnerId != null)
                    vm.Partner = await _partnerService.GetAsync((int)vm.PartnerId);

                if (!ModelState.IsValid)
                {
                    ViewBag.Partners = await _partnerService.GetAllAsync() ?? new List<BL.Models.Partner>();
                    return View(vm);
                }

                await _invoiceService.AddAsync(_invoicesViewModelMapper.GetDomainFromViewModel(vm));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vm);
            }
        }
        
        public async Task<ActionResult> Edit(int id)
        {
            var invoice = await _invoiceService.GetAsync(id);

            if (invoice == null)
                return RedirectToAction(nameof(Index));

            var vm = _invoicesViewModelMapper.GetViewModelFromDomain(invoice);

            ViewBag.Partners = await _partnerService.GetAllAsync() ?? new List<BL.Models.Partner>();
                
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InvoicesViewModel vm)
        {
            try
            {
                if (vm.PartnerId != null)
                    vm.Partner = await _partnerService.GetAsync((int)vm.PartnerId);

                if (!ModelState.IsValid)
                {
                    ViewBag.Partners = await _partnerService.GetAllAsync() ?? new List<BL.Models.Partner>();
                    return View(vm);
                }
                    
                var isUpdated = await _invoiceService.UpdateAsync(_invoicesViewModelMapper.GetDomainFromViewModel(vm));

                if (!isUpdated)
                    return View(vm);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _invoiceService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}