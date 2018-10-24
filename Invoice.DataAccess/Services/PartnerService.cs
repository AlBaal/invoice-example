using Invoice.BL.Interfaces;
using Invoice.BL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.DataAccess.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly InvoiceDbContext _context;

        public PartnerService(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Partner>> GetAllAsync()
        {
            return await _context.Partner.ToListAsync();
        }

        public async Task<Partner> GetAsync(int id)
        {
            return await _context.Partner.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
