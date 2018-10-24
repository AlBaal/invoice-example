using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.BL.Models
{
    public class InvoiceItem : BaseDomain
    {
        public string Name { get; set; }
        public InvoiceItemStatus Status { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public Models.Invoice Invoice { get; set; }
    }
}
