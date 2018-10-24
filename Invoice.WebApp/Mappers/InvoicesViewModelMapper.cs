using Invoice.BL.Models;
using Invoice.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Mappers
{
    public class InvoicesViewModelMapper : IMapper<InvoicesViewModel, BL.Models.Invoice>
    {
        private readonly InvoiceItemsViewModelMapper _invoiceItemsViewModelMapper;

        public InvoicesViewModelMapper(InvoiceItemsViewModelMapper invoiceItemsViewModelMapper)
        {
            _invoiceItemsViewModelMapper = invoiceItemsViewModelMapper;
        }

        public BL.Models.Invoice GetDomainFromViewModel(InvoicesViewModel viewModel)
        {
            var result = new BL.Models.Invoice()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                BankAccount = viewModel.BankAccount,
                DueDate = viewModel.DueDate,
                Partner = viewModel.Partner,
                PaymentStatus = viewModel.PaymentStatus,
                PaymentType = viewModel.PaymentType,
                Status = viewModel.Status,
                InvoiceItems = viewModel.InvoiceItems.Select(a => _invoiceItemsViewModelMapper.GetDomainFromViewModel(a)).ToList()
            };

            return result;
        }

        public InvoicesViewModel GetViewModelFromDomain(BL.Models.Invoice domainObject)
        {
            var result = new InvoicesViewModel()
            {
                Id = domainObject.Id,
                Description = domainObject.Description,
                BankAccount = domainObject.BankAccount,
                DueDate = domainObject.DueDate,
                Partner = domainObject.Partner,
                PartnerId = domainObject.Partner.Id,
                PaymentStatus = domainObject.PaymentStatus,
                PaymentType = domainObject.PaymentType,
                Status = domainObject.Status,
                InvoiceItems = domainObject.InvoiceItems.Select(a => _invoiceItemsViewModelMapper.GetViewModelFromDomain(a)).ToList()
            };

            return result;
        }
    }
}
