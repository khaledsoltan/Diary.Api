using AutoMapper;
using Business.AuthService.Contracts;
using Business.AuthService.Implmentation;
using Business.GenericServices.Contracts;
using Business.ServiceLocator.Contracts;
using Domain.Auth;
using Domain.ConfigurationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository.UnitOfWork;
using Shared.LoggerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceLocator.Implmentation
{
    public class ServiceLocator : IServiceLocator
    {
    
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceLocator(IUnitOfWork repositoryManager, ILoggerManager logger , IMapper mapper, UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));

        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
