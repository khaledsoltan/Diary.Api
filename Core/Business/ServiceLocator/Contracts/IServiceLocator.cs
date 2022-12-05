using Business.AuthService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceLocator.Contracts
{
    public interface IServiceLocator
    {
        
        IAuthenticationService AuthenticationService { get; }

    }
}
