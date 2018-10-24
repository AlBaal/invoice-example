using GraphQL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Graphs
{
    public class InvoiceItemGraph : EfObjectGraphType<BL.Models.InvoiceItem>
    {
        public InvoiceItemGraph(IEfGraphQLService graphQlService) : base(graphQlService)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Quantity);
            Field(x => x.Amount);
            AddNavigationField<InvoiceGraph, BL.Models.Invoice>(
                name: "invoice",
                resolve: context => context.Source.Invoice
            );
        }
    }
}
