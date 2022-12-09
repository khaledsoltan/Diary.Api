using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DTOS.DiaryEvent;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryEventController : ControllerBase
    {
        private readonly IServiceLocator _service;
        public const string Create_NewEvent = nameof(Create_NewEvent);
        public const string Delete_DiaryEvent = nameof(Delete_DiaryEvent);
        public const string Update_DiaryEvent = nameof(Update_DiaryEvent);
        public const string GetDays_InMonthWithEvents = nameof(GetDays_InMonthWithEvents);
        public const string GetDiary_EventsByDate = nameof(GetDiary_EventsByDate);
        public const string GetDiary_EventsRecentlyChanged = nameof(GetDiary_EventsRecentlyChanged);

        
        public DiaryEventController(IServiceLocator service) => _service = service;

        [HttpPost("CreateNewEvent" , Name = Create_NewEvent)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateNewEvent(Guid diaryId, [FromBody] DiaryEventDto diaryEventDto)
        {
           var result = _service.DiaryEventService.CreateDiaryEvent(diaryId, diaryEventDto, false);
            return Ok(result);

        }

        [HttpPut("UpdateDiaryEvent" , Name = Update_DiaryEvent)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDiaryEvent(Guid diaryId, Guid eventId, [FromBody] DiaryEventDto diaryEventDto)
        {
           
            var result =await _service.DiaryEventService.UpdateDiaryEvent(diaryId, eventId, diaryEventDto, false, true);
            return Ok(result);
        }

        [HttpDelete("DeleteDiaryEvent", Name = Delete_DiaryEvent)]
        public async Task<OkApiResponse<string>> DeleteDiaryEvent(Guid diaryId,Guid diaryEventId)
        {
            await _service.DiaryEventService.DeleteDiaryEvent(diaryId , diaryEventId, trackChanges: false);
            return  new OkApiResponse<string>("DiaryEvent has been deleted successfully");
        }
        [HttpGet("GetDaysInMonthWithEvents" , Name = GetDays_InMonthWithEvents)]
        public async Task<IActionResult> GetDaysInMonthWithEvents(Guid diaryID,DiaryEventsParameters diaryEventsParameters, int Month, int Year)
        {
            var pageResult = await _service.DiaryEventService.GetDaysInMonthWithEvents(diaryID, diaryEventsParameters, Month, Year, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageResult.metaData));
            return Ok(pageResult.events);
        }


        [HttpGet("GetDiaryEventsByDate" , Name = GetDiary_EventsByDate)]
        public async Task<IActionResult> GetDiaryEventsByDate(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, DateTime FromDate, DateTime ToDate)
        {
            var diaryEntries =await _service.DiaryEventService.GetDiaryEventsByDate(DiaryId, diaryEventsParameters, FromDate, ToDate, false);
            return Ok(diaryEntries);
        }

        [HttpGet("GetDiaryEventsRecentlyChanged/{DiaryId:guid}", Name = GetDiary_EventsRecentlyChanged)]

        public async Task<IActionResult> GetDiaryEventsRecentlyChanged(Guid DiaryId, DiaryEventsParameters diaryEventsParameters)
        {
            var diaryEntries = await _service.DiaryEventService.GetDiaryEventsRecentlyChanged(DiaryId, diaryEventsParameters,false);
            return Ok(diaryEntries);
        }


    }
}
