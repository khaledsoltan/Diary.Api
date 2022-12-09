using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryEntry;
using Shared.RequestParameters.Diary;
using Shared.RequestParameters.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryEntryController :  ControllerBase
    {

        public const string CreatEentry_Diary = nameof(CreatEentry_Diary);
        public const string Update_DiaryEntry = nameof(Update_DiaryEntry);
        public const string Delete_DiaryEntry = nameof(Delete_DiaryEntry);
        public const string GetDays_InMonthWithEntries = nameof(GetDays_InMonthWithEntries);
        public const string GetDiaryEntries_RecentlyChanged = nameof(GetDiaryEntries_RecentlyChanged);
        public const string GetDiary_EntriesByDate = nameof(GetDiary_EntriesByDate);
        public const string GetDiary_EntriesById = nameof(GetDiary_EntriesById);

        

        private readonly IServiceLocator _service;

        public DiaryEntryController(IServiceLocator service) => _service = service;


        [HttpPost("CreatEentryDiary", Name = CreatEentry_Diary)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreatEentryDiary(Guid DiaryId, [FromBody] DiaryEntryDtoForCreate diaryEntrydtoForCreate)
        {
           
           var result =  _service.DiaryEntryService.CreateDiaryEntry(DiaryId,  diaryEntrydtoForCreate, false);
           return Ok(result);

        }

        [HttpPut("UpdateDiaryForDiaryEntry", Name = Update_DiaryEntry)]
        public IActionResult UpdateDiaryForDiaryEntry(Guid diaryId, Guid entryId, [FromBody] DiaryEntryDtoForUpdate diaryEntrydto)
        {
       
            var result = _service.DiaryEntryService.UpdateDiaryEntry(diaryId, entryId, diaryEntrydto,  false, true);

            return Ok(result);
        }

        [HttpDelete("DeleteDiaryEntry", Name = Delete_DiaryEntry)]
        public OkApiResponse<string> DeleteDiaryEntry(Guid diaryId,Guid diaryEntryId)
        {
            _service.DiaryEntryService.DeleteDiaryEntry(diaryId , diaryEntryId, trackChanges: false);
            return new OkApiResponse<string>("Successfully deleted !");
        }

 

        [HttpGet("GetDaysInMonthWithEntries", Name = GetDays_InMonthWithEntries)]
        public async Task<IActionResult> GetDaysInMonthWithEntries(Guid diaryId, [FromQuery] DiaryEntryParameters diaryEntryParameters, int Month, int Year)
        {
            var pagedResult = await _service.DiaryEntryService.GetDaysInMonthWithEntries(diaryId, diaryEntryParameters, Month, Year ,  false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diaryEntries);
        }
        [HttpGet("GetDiaryEntriesRecentlyChanged", Name = GetDiaryEntries_RecentlyChanged)]

        public async Task<IActionResult> GetDiaryEntriesRecentlyChanged(Guid diaryId, [FromQuery] DiaryEntryParameters diaryEntryParameters,  DateTime FromDate, DateTime ToDate)
        {
            var pagedResult = await _service.DiaryEntryService.GetDiaryEntriesRecentlyChanged(diaryId, diaryEntryParameters,   false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diariyentries);
        }

        [HttpGet("GetDiaryEntriesByDate" , Name = GetDiary_EntriesByDate)]
        public async Task<IActionResult> GetDiaryEntriesByDate(Guid diaryId, [FromQuery] DiaryEntryParameters diaryEntryParametersDateTime, DateTime FromDate, DateTime ToDate)
        {
            var pagedResult = await _service.DiaryEntryService.GetDiaryEntriesByDate(diaryId, diaryEntryParametersDateTime , FromDate, ToDate, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diariyentries);
        }

        [HttpGet("GetDiaryEntriesById", Name = GetDiary_EntriesById)]
        public async Task<IActionResult> GetDiaryEntriesById(Guid diaryId, Guid diarEntry)
        {
            var result = await _service.DiaryEntryService.GetDiaryEntriesById(diaryId, diarEntry, false);
            return Ok(result);
        }
        

    }
}
