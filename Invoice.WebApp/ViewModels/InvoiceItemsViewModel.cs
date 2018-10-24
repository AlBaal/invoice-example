using Invoice.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.ViewModels
{
    public class InvoiceItemsViewModel
    {
        public InvoiceItemsViewModel()
        {
        }

        public InvoiceItemsViewModel(int invoiceId)
        {
            this.InvoiceId = invoiceId;
        }

        public int Id { get; set; }
        public int? InvoiceId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Status")]
        public InvoiceItemStatus Status { get; set; } = InvoiceItemStatus.Active;

        [Display(Name = "Price per unit")]
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Amount { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; } = 1;

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Total amount")]
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalAmount => (this.Amount ?? 0) * this.Quantity;
    }
}
