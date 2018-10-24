using GraphQL.EntityFramework;
using GraphQL.Types;
using Invoice.DataAccess;
using Invoice.GraphQL.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Queries
{
    public class InvoiceQueryEF : EfObjectGraphType
    {
        public InvoiceQueryEF(IEfGraphQLService efGraphQlService, InvoiceDbContext dbContext) : base(efGraphQlService)
        {
            Name = "Query";

            AddQueryField<InvoiceGraph, BL.Models.Invoice>(
                name: "invoice",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of invoice" }),
                resolve: context =>
                {
                    var dataContext = context.UserContext;
                    var id = context.GetArgument<int>("id");
                    return dbContext.Invoice.Where(i => i.Id == id);
                }
            );

            AddQueryField<InvoiceGraph, BL.Models.Invoice>(
                name: "invoices",
                resolve: context =>
                {
                    return dbContext.Invoice;
                }
            );

            AddQueryField<InvoiceItemGraph, BL.Models.InvoiceItem>(
                name: "items",
                resolve: context =>
                {
                    return dbContext.InvoiceItem;
                }
            );
        }
    }
}
