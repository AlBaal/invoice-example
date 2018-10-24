using GraphQL.Types;
using Invoice.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Types
{
    public class InvoiceType : ObjectGraphType<BL.Models.Invoice>
    {
        public InvoiceType(IInvoiceService invoiceService)
        {
            Name = "Invoice";

            Field(i => i.Id).Description("The id of the invoice");
            Field(i => i.Description, nullable: true).Description("The description of the invoice");
        }
    }
}
