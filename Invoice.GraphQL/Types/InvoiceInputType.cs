using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Types
{
    public class InvoiceInputType : InputObjectGraphType<BL.Models.Invoice>
    {
        public InvoiceInputType()
        {
            Name = "InvoiceInput";
            Field(x => x.Description);
        }
    }
}
