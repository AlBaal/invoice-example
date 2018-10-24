using GraphQL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Graphs
{
    public class InvoiceGraph : EfObjectGraphType<BL.Models.Invoice>
    {
        public InvoiceGraph(IEfGraphQLService graphQlService) : base(graphQlService)
        {
            Field(x => x.Id);
            Field(x => x.Description);
            AddNavigationField<InvoiceItemGraph, BL.Models.InvoiceItem>(
                name: "items",
                resolve: context => context.Source.InvoiceItems
            );
        }
    }
}
