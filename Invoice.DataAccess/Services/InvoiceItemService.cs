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
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly InvoiceDbContext _context;

        public InvoiceItemService(InvoiceDbContext context)
        {
            _context = context;
        }

        private IQueryable<InvoiceItem> GetAllQuery()
        {
            return _context.InvoiceItem;
        }

        private IQueryable<InvoiceItem> GetAllActiveQuery()
        {
            return this.GetAllQuery().Where(a => a.Status == InvoiceItemStatus.Active);
        }

        public Task<List<InvoiceItem>> GetAllActiveAsync()
        {
            return this.GetAllActiveQuery().ToListAsync();
        }

        public Task<List<InvoiceItem>> GetAllActiveAsync(int invoiceId)
        {
            return this.GetAllActiveQuery().Where(a => a.Invoice.Id == invoiceId).ToListAsync();
        }

        public async Task<bool> AddAsync(InvoiceItem item)
        {
            try
            {
                await _context.AddAsync(item);
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
                var item = await GetAsync(id);

                if (item == null)
                    return false;

                item.Status = InvoiceItemStatus.Deleted;

                _context.Update(item);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InvoiceItem> GetAsync(int id)
        {
            return await GetAllActiveQuery().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> UpdateAsync(InvoiceItem item)
        {
            try
            {
                if (item == null)
                    return false;

                _context.Update(item);
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
