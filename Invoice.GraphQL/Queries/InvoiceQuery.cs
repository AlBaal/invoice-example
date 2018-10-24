using GraphQL.Types;
using Invoice.BL.Interfaces;
using Invoice.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Queries
{
    public class InvoiceQuery : ObjectGraphType<object>
    {
        public InvoiceQuery(IInvoiceService invoiceService)
        {
            Name = "Query";

            Field<InvoiceType>(
                 "invoice",
                 arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of invoice" }),
                 resolve: context => invoiceService.GetAsync(context.GetArgument<int>("id"))
            );
            Field<ListGraphType<InvoiceType>>(
                 "invoices",
                 resolve: context => invoiceService.GetAllActiveAsync()
            );
        }
    }
}
