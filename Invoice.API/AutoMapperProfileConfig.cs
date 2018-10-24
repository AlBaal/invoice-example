using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig() : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfig(string profileName) : base(profileName)
        {
            CreateMap<BL.Models.Invoice, BL.Models.Invoice>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.InvoiceItems, opt => opt.Ignore())
                .ForMember(x => x.Partner, opt => opt.Ignore());
        }
    }
}
