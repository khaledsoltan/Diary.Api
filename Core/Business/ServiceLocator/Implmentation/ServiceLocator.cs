using AutoMapper;
using Business.AuthService.Contracts;
using Business.AuthService.Implmentation;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.Services.Contracts;
using Business.Diary.Services.Implmentation;
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
        private readonly Lazy<IDiaryService> _diaryService;
        private readonly Lazy<IDiaryEventService> _diaryeventService;
        private readonly Lazy<IDiaryEntryService> _diaryEntryService;

        public ServiceLocator(IUnitOfWork repository, ILoggerManager logger , IMapper mapper, UserManager<User> userManager, IOptions<JwtConfiguration> configuration, IValidateIFexists ValidateIFexists)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _diaryService = new Lazy<IDiaryService>(() => new DiaryService(repository, logger, mapper, ValidateIFexists));
            _diaryeventService = new Lazy<IDiaryEventService>(() => new DiaryEventService(repository, logger, mapper, ValidateIFexists));
            _diaryEntryService = new Lazy<IDiaryEntryService>(() => new DiaryEntryService(repository, logger, mapper, ValidateIFexists));
        }

        public IDiaryService DiaryService => _diaryService.Value;
        public IDiaryEventService DiaryEventService => _diaryeventService.Value;

        public IDiaryEntryService DiaryEntryService => _diaryEntryService.Value;
        
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
