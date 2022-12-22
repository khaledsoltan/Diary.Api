using Api.Host.Domain.Entites;
using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Presentation.ActionFilters;
using Shared.DTOS.DiaryEvent;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{

    /// <summary>
    /// The DiaryEvent class objectifies a single entry in a diary. It encapsulates everything to do with diary
    /// entries, including creating, updating, and retrieving diary events data.It handles all the database access for diary events.
    /// </summary>
    /// 
    [Authorize]
    [Route("api/DiaryEvent")]
    [ApiController]
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



        /// <summary>
        /// Creates a new DiaryEvent object with all properties set to their default values.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diaryEventForCreate"></param>
        /// <returns>
        /// * return object inserted ito database.
        /// * return all links describe all behaviors related Contact
        /// </returns>
        [HttpPost("CreateNewEvent" , Name = Create_NewEvent)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateNewEvent(Guid diaryId, [FromBody] DiaryEventForCreate diaryEventForCreate)
        {
           var result = await _service.DiaryEventService.CreateDiaryEvent(diaryId, diaryEventForCreate, false);
            return Ok(result);

        }


        /// <summary>
        /// calls UpdateDiaryEvent, which updates the database values with those in the DiaryEvent object.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="eventId"></param>
        /// <param name="diaryEventForUpdate"></param>
        /// <returns>
        ///  return updated object from database and all links describe all behaviors related Contact.
        /// </returns>

        [HttpPut("UpdateDiaryEvent" , Name = Update_DiaryEvent)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDiaryEvent([FromQuery] Guid diaryId, Guid eventId, [FromBody] DiaryEventForUpdate diaryEventForUpdate)
        {
            var result =await _service.DiaryEventService.UpdateDiaryEvent(diaryId, eventId, diaryEventForUpdate, false, true);
            return Ok(result);
        }



        /// <summary>
        /// deletes the event from the database with an EventId value equal to the EventId argument of the method.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diaryEventId"></param>
        /// <returns>
        /// return DiaryEvent has been deleted successfully!
        /// </returns>
        [HttpDelete("DeleteDiaryEvent", Name = Delete_DiaryEvent)]
        public async Task<OkApiResponse<string>> DeleteDiaryEvent([FromQuery] Guid diaryId,Guid diaryEventId)
        {
            await _service.DiaryEventService.DeleteDiaryEvent(diaryId , diaryEventId, trackChanges: false);
            return  new OkApiResponse<string>("DiaryEvent has been deleted successfully");
        }

        /// <summary>
        ///  returns array   have events associated with them.The array index matches with the day of the month(1 is the first of the month, 2 the second, and so on).
        /// </summary>
        /// <param name="diaryID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="diaryEventsParameters"></param>
        /// <returns>
        /// * List Of Events.
        /// * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext}
        /// * Return all links describe all behaviors related Diary
        /// </returns>
        [HttpGet("GetDaysInMonthWithEvents" , Name = GetDays_InMonthWithEvents)]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetDaysInMonthWithEvents( Guid diaryID, int Month, int Year, [FromQuery]  DiaryEventsParameters diaryEventsParameters)
        {

            var pageResult = await _service.DiaryEventService.GetDaysInMonthWithEvents(diaryID, diaryEventsParameters, Month, Year, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageResult.metaData));
            return Ok(pageResult.events);

        }

        /// <summary>
        /// returns a  objects populated with rows from the database detailing diary events between the FromDate and ToDate arguments
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="diaryEventsParameters"></param>
        /// <returns>
        /// * List Of Events.
        /// * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext}
        /// * Return all links describe all behaviors related Diary
        /// </returns>
        [HttpGet("GetDiaryEventsByDate" , Name = GetDiary_EventsByDate)]
        public async Task<IActionResult> GetDiaryEventsByDate(Guid DiaryId, DateTime FromDate, DateTime ToDate, [FromQuery] DiaryEventsParameters diaryEventsParameters)
        {
            var diaryEvents =await _service.DiaryEventService.GetDiaryEventsByDate(DiaryId, diaryEventsParameters, FromDate, ToDate, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(diaryEvents.metaData));
            return Ok(diaryEvents.diaries);
        }


        /// <summary>
        /// returns a  objects populated with rows from the database detailing diary events RecentlyChanged
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="diaryEventsParameters"></param>
        /// <returns>
        /// * List Of Events.
        /// * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext}
        /// * Return all links describe all behaviors related Diary
        /// </returns>
        [HttpGet("GetDiaryEventsRecentlyChanged/{DiaryId:guid}", Name = GetDiary_EventsRecentlyChanged)]
        public async Task<IActionResult> GetDiaryEventsRecentlyChanged(Guid DiaryId,[FromQuery] DiaryEventsParameters diaryEventsParameters)
        {
            var diaryEntries = await _service.DiaryEventService.GetDiaryEventsRecentlyChanged(DiaryId, diaryEventsParameters,false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(diaryEntries.metaData));
            return Ok(diaryEntries.events);
        }

        /// <summary>
        /// Get Diary Event By Id
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="EventId"></param>
        /// <returns>
        /// return  object from database.
        /// return all links describe all behaviors related Contact
        /// </returns>
        [HttpGet("GetDiaryEventById")]
        public async Task<IActionResult> GetDiaryEventById([FromQuery] Guid DiaryId,Guid EventId)
        {
            var diaryEntries = await _service.DiaryEventService.GetDiaryEventById(DiaryId, EventId, false);
            return Ok(diaryEntries);
        }

    }
}
