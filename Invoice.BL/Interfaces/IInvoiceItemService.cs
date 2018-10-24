using Invoice.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.BL.Interfaces
{
    public interface IInvoiceItemService
    {
        Task<InvoiceItem> GetAsync(int id);
        Task<List<InvoiceItem>> GetAllActiveAsync();
        Task<List<InvoiceItem>> GetAllActiveAsync(int invoiceId);
        Task<bool> AddAsync(InvoiceItem item);
        Task<bool> UpdateAsync(InvoiceItem item);
        Task<bool> DeleteAsync(int id);
    }
}
