using Invoice.BL.Models;
using Invoice.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Mappers
{
    public class InvoiceItemsViewModelMapper : IMapper<InvoiceItemsViewModel, InvoiceItem>
    {
        public InvoiceItem GetDomainFromViewModel(InvoiceItemsViewModel viewModel)
        {
            var result = GetDomainFromViewModel(viewModel, null);

            return result;
        }

        public InvoiceItem GetDomainFromViewModel(InvoiceItemsViewModel viewModel, BL.Models.Invoice invoice)
        {
            var result = new InvoiceItem()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Quantity = viewModel.Quantity,
                Amount = (decimal)viewModel.Amount,
                Status = viewModel.Status,
                Unit = viewModel.Unit
            };

            if (invoice != null)
                result.Invoice = invoice;

            return result;
        }

        public InvoiceItemsViewModel GetViewModelFromDomain(InvoiceItem domainObject)
        {
            var result = new InvoiceItemsViewModel(domainObject.Invoice.Id)
            {
                Id = domainObject.Id,
                Name = domainObject.Name,
                Quantity = domainObject.Quantity,
                Amount = domainObject.Amount,
                Status = domainObject.Status,
                Unit = domainObject.Unit
            };

            return result;
        }
    }
}
