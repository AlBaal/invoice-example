using GraphQL.Types;
using Invoice.BL.Interfaces;
using Invoice.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Mutations
{
    public class InvoiceMutation : ObjectGraphType
    {
        public InvoiceMutation(IInvoiceService invoiceServise)
        {
            Name = "InvoiceMutation";

            Field<BooleanGraphType>(
                "createInvoice",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<InvoiceInputType>> { Name = "invoice" }
                ),
                resolve: context =>
                {
                    var invoice = context.GetArgument<BL.Models.Invoice>("invoice");
                    return invoiceServise.AddAsync(invoice);
                });
        }
    }
}
