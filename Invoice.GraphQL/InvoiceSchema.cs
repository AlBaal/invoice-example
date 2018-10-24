using GraphQL;
using GraphQL.Types;
using Invoice.GraphQL.Mutations;
using Invoice.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL
{
    public class InvoiceSchema : Schema
    {
        public InvoiceSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<InvoiceQueryEF>();
            Mutation = resolver.Resolve<InvoiceMutation>();
        }
    }
}
