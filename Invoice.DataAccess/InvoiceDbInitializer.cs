using Invoice.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess
{
    public class InvoiceDbInitializer
    {
        private readonly InvoiceDbContext _context;

        public InvoiceDbInitializer(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Partner.Any())
            {
                var partners = new List<Partner>();
                partners.Add(new Partner { Name = "Prague Labs s.r.o." });
                partners.Add(new Partner { Name = "Nestlé Česko s.r.o." });
                partners.Add(new Partner { Name = "24net s.r.o." });
                partners.Add(new Partner { Name = "WebExpo s.r.o." });

                _context.AddRange(partners);

                await _context.SaveChangesAsync();
            }

            if (!_context.Invoice.Any())
            {
                var invoices = new List<BL.Models.Invoice>();
                invoices.Add(new BL.Models.Invoice
                {
                    Description = "Faktura za leden 2018",
                    BankAccount = "10101010/0100",
                    DueDate = DateTime.Parse("14.1.2018"),
                    PaymentStatus = PaymentStatus.Paid,
                    PaymentType = PaymentType.Transfer,
                    Status = InvoiceStatus.Active,
                    Partner = _context.Partner.FirstOrDefault(a => a.Id == 1),
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem { Name = "Práce na projektu 1", Amount = 2500M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 7 },
                        new InvoiceItem { Name = "Práce na projektu 2", Amount = 2500M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 2.5M },
                        new InvoiceItem { Name = "Práce na projektu 3", Amount = 2500M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 1.3M },
                    }
                });
                invoices.Add(new BL.Models.Invoice
                {
                    Description = "Faktura za únor 2018",
                    BankAccount = "10101010/0100",
                    DueDate = DateTime.Parse("14.2.2018"),
                    PaymentStatus = PaymentStatus.Paid,
                    PaymentType = PaymentType.Cash,
                    Status = InvoiceStatus.Active,
                    Partner = _context.Partner.FirstOrDefault(a => a.Id == 2),
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem { Name = "Práce na projektu 1", Amount = 2100M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 5 },
                        new InvoiceItem { Name = "Práce na projektu 2", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 2.5M },
                        new InvoiceItem { Name = "Práce na projektu 3", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 12.3M },
                    }
                });
                invoices.Add(new BL.Models.Invoice
                {
                    Description = "Faktura za březen 2018",
                    BankAccount = "10101010/0100",
                    DueDate = DateTime.Parse("14.3.2018"),
                    PaymentStatus = PaymentStatus.Paid,
                    PaymentType = PaymentType.Transfer,
                    Status = InvoiceStatus.Active,
                    Partner = _context.Partner.FirstOrDefault(a => a.Id == 3),
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem { Name = "Práce na projektu 1", Amount = 2100M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 5 },
                        new InvoiceItem { Name = "Práce na projektu 2", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 2.5M },
                        new InvoiceItem { Name = "Práce na projektu 3", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 12.3M },
                    }
                });
                invoices.Add(new BL.Models.Invoice
                {
                    Description = "Faktura za duben 2018",
                    BankAccount = "10101010/0100",
                    DueDate = DateTime.Parse("14.4.2018"),
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentType = PaymentType.Card,
                    Status = InvoiceStatus.Active,
                    Partner = _context.Partner.FirstOrDefault(a => a.Id == 3),
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem { Name = "Práce na projektu 1", Amount = 2100M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 20 },
                        new InvoiceItem { Name = "Práce na projektu 2", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 2.5M },
                        new InvoiceItem { Name = "Práce na projektu 3", Amount = 2200M, Status = InvoiceItemStatus.Active, Unit = "MD", Quantity = 12.3M },
                    }
                });

                _context.AddRange(invoices);

                await _context.SaveChangesAsync();
            }
        }
    }
}
