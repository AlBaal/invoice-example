using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.BL.Models
{
    public class Invoice : BaseDomain
    {
        public InvoiceStatus Status { get; set; }
        public string Description { get; set; }
        public string BankAccount { get; set; }
        public DateTime DueDate { get; set; }
        public Partner Partner { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
