using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.BL.Interfaces
{
    public interface IInvoiceService
    {
        Task<Models.Invoice> GetAsync(int id);
        Task<List<Models.Invoice>> GetAllActiveAsync();
        Task<List<Models.Invoice>> GetAllActivePendingAsync();
        Task<bool> AddAsync(Models.Invoice invoice);
        Task<bool> UpdateAsync(Models.Invoice invoice);
        Task<bool> DeleteAsync(int id);
    }
}
