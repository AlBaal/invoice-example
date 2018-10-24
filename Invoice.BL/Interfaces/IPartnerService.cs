using Invoice.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.BL.Interfaces
{
    public interface IPartnerService
    {
        Task<List<Partner>> GetAllAsync();
        Task<Partner> GetAsync(int id);
    }
}
