using Business.ServiceLocator.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryContactController : ControllerBase
    {

        private readonly IServiceLocator _service;

        public DiaryContactController(IServiceLocator service) => _service = service;

    }
}
