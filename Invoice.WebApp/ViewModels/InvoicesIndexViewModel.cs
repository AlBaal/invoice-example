using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.ViewModels
{
    public class InvoicesIndexViewModel
    {
        public List<InvoicesViewModel> Invoices { get; set; } = new List<InvoicesViewModel>();

        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalAmount => Invoices.Sum(a => a.InvoicedAmount);
    }
}
