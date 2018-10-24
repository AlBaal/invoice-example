using GraphQL.Types;
using Invoice.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.Interfaces
{
    public class InvoiceInterface : InterfaceGraphType<BL.Models.Invoice>
    {
        public InvoiceInterface()
        {
            Name = "Invoice";

            Field(d => d.Id).Description("The id of the interface.");
            Field(d => d.Description, nullable: true).Description("The description of the interface.");
        }
    }
}
