using GraphQL.EntityFramework;
using GraphQL.Types;
using Invoice.DataAccess;
using System.Linq;

public class Query : EfObjectGraphType
{
    public Query(IEfGraphQLService efGraphQlService) : base(efGraphQlService)
    {
        Name = "Query";

        AddQueryField<InvoiceGraph, Invoice.BL.Models.Invoice>(
            name: "invoice",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "id of invoice" }),
            resolve: context =>
            {
                var dataContext = context.UserContext as InvoiceDbContext;
                var id = context.GetArgument<int>("id");
                return dataContext.Invoice.Where(i => i.Id == id);
            }
        );

        AddQueryField<InvoiceGraph, Invoice.BL.Models.Invoice>(
            name: "invoices",
            resolve: context =>
            {
                var dataContext = context.UserContext as InvoiceDbContext;
                return dataContext.Invoice;
            }
        );

        AddQueryConnectionField<InvoiceGraph, Invoice.BL.Models.Invoice>(
            name: "invoicesConnection",
            resolve: context =>
            {
                var dataContext = context.UserContext as InvoiceDbContext;
                return dataContext.Invoice;
            }
        );

        AddQueryField<InvoiceItemGraph, Invoice.BL.Models.InvoiceItem>(
            name: "items",
            resolve: (ResolveFieldContext<object> context) =>
            {
                var dataContext = context.UserContext as InvoiceDbContext;
                return dataContext.InvoiceItem;
            }
        );

        AddQueryConnectionField<InvoiceItemGraph, Invoice.BL.Models.InvoiceItem>(
            name: "itemsConnection",
            resolve: context =>
            {
                var dataContext = context.UserContext as InvoiceDbContext;
                return dataContext.InvoiceItem;
            }
        );
    }
}