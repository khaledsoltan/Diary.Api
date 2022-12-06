using Business.ServiceLocator.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.Controllers.Base;
using Shared.DTOS.DiaryDto;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{


   
    /// <summary>
    /// 
    /// </summary>
    /// 

    [Authorize]
    [Route("api/Diaries")]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryController : ControllerBase
    {

        public const string GetAllDiaries = nameof(GetAllDiaries);
        public const string CreateDiaryForUser = nameof(CreateDiaryForUser);
        public const string DeleteDiary = nameof(DeleteDiary);
        public const string UpdateDiaryForUser = nameof(UpdateDiaryForUser);


        
        private readonly IServiceLocator _service;

        /// <summary>
        /// 
        /// </summary>
        public DiaryController(IServiceLocator service) => _service = service;

        /// <summary>
        /// GetDiaries 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = GetAllDiaries)]
        public async Task<IActionResult> GetDiaries([FromQuery] DiaryRequestParameter diaryRequestParameter)
        {
            var pagedResult = await  _service.DiaryService.GetAllDiariesByUserId(diaryRequestParameter , trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.diaries);
        }

        /// <summary>
        /// Create Diary 
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = CreateDiaryForUser)]
        public async Task<IActionResult> CreateDiary(DiaryDto diaryDto)
        {
            var baseResult =  _service.DiaryService.CreateDiary(diaryDto);

            return Ok(baseResult);
        }
        [HttpDelete(Name = DeleteDiary)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult DeleteDiaryForUser(Guid DiaryId)
        {
            _service.DiaryService.DeleteDiaryForUser(DiaryId, User.FindFirstValue(ClaimTypes.NameIdentifier), trackChanges: false);
            return Content("Delete Sucsseful");
        }

        [HttpPut("UpdateDiary",Name = UpdateDiaryForUser)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdateDiary(Guid DiaryId, [FromBody] UpdateDiaryDto UpdateDiaryDto)
        {

            _service.DiaryService.UpdateDiary(DiaryId, User.FindFirstValue(ClaimTypes.NameIdentifier), UpdateDiaryDto, true);

            return Content("Updated Succesfully");
        }

    }
}
