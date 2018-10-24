using GraphQL.EntityFramework;

public class InvoiceItemGraph : EfObjectGraphType<Invoice.BL.Models.InvoiceItem>
{
    public InvoiceItemGraph(IEfGraphQLService graphQlService) : base(graphQlService)
    {
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.Quantity);
        Field(x => x.Amount);
        AddNavigationField<InvoiceGraph, Invoice.BL.Models.Invoice>(
            name: "invoice",
            resolve: context => context.Source.Invoice
        );
    }
}