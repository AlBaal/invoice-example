using Invoice.BL.Interfaces;
using Invoice.BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.ViewModels
{
    public class InvoicesViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Bank account")]
        public string BankAccount { get; set; }

        [Display(Name = "Due date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14).Date;
        public int? PartnerId { get; set; }

        [Display(Name = "Client")]
        public Partner Partner { get; set; }

        [Display(Name = "Payment type")]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Payment status")]
        public PaymentStatus PaymentStatus { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Active;

        public List<InvoiceItemsViewModel> InvoiceItems { get; set; } = new List<InvoiceItemsViewModel>();

        [Display(Name = "Invoiced amount")]
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InvoicedAmount
        {
            get
            {
                return this.InvoiceItems == null ? 0 : this.InvoiceItems.Where(a => a.Status == InvoiceItemStatus.Active).Sum(a => a.TotalAmount);
            }
        }


        #region control binding collections

        public List<Partner> Partners { get; set; }

        #endregion
    }
}
