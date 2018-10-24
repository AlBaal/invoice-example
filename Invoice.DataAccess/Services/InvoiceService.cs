using Invoice.BL.Interfaces;
using Invoice.BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceDbContext _context;

        public InvoiceService(InvoiceDbContext context)
        {
            _context = context;
        }

        private IQueryable<BL.Models.Invoice> GetAllQuery()
        {
            return _context.Invoice
                .Include(a => a.InvoiceItems)
                .Include(a => a.Partner);
        }

        private IQueryable<BL.Models.Invoice> GetAllActiveQuery()
        {
            return this.GetAllQuery().Where(a => a.Status == InvoiceStatus.Active);
        }

        public async Task<List<BL.Models.Invoice>> GetAllActiveAsync()
        {
            return await this.GetAllActiveQuery().ToListAsync();
        }

        public async Task<List<BL.Models.Invoice>> GetAllActivePendingAsync()
        {
            return await this.GetAllActiveQuery().Where(a => a.PaymentStatus == PaymentStatus.Pending).ToListAsync();
        }

        public async Task<bool> AddAsync(BL.Models.Invoice invoice)
        {
            try
            {
                await _context.AddAsync(invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var invoice = await GetAsync(id);

                if (invoice == null)
                    return false;

                invoice.Status = InvoiceStatus.Deleted;

                _context.Update(invoice);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BL.Models.Invoice> GetAsync(int id)
        {
            return await GetAllActiveQuery().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateAsync(BL.Models.Invoice invoice)
        {
            try
            {
                if (invoice == null)
                    return false;

                _context.Update(invoice);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
