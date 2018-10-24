using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.WebApp.Mappers
{
    public interface IMapper<TViewModel, TDomainObject>
    {
        TViewModel GetViewModelFromDomain(TDomainObject domainObject);
        TDomainObject GetDomainFromViewModel(TViewModel viewModel);
    }
}
