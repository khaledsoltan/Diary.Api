using Api.Host.Domain.Entites;
using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Presentation.ActionFilters;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryEntry;
using Shared.RequestParameters.Diary;
using Shared.RequestParameters.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{

    /// <summary>
    /// The DiaryEntry class objectifies a single entry in a diary. It encapsulates everything to do with diary
    /// entries, including creating, updating, and retrieving diary entry data.It handles all the database access
    /// for diary entries.
    /// </summary>
    [Authorize]
    [Route("api/DiaryEntry")]
    [ApiController]
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

        /// <summary>
        /// Saves a fully populated
        /// DiaryEntry object. If it’s a  new entry, Save() calls
        /// InsertNewDiaryEntry sub and the details are inserted in to the database.
        /// The new DiaryEntryId is returned from the database and entered in to mDiaryEntryId.
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="diaryEntrydtoForCreate"></param>
        /// <returns>
        /// * return object inserted ito database.
        /// * return all links describe all behaviors related DiaryEntry
        /// </returns>
        [HttpPost("CreatEntryDiary", Name = CreatEentry_Diary)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatEntryDiary(Guid DiaryId, [FromBody] DiaryEntryDtoForCreate diaryEntrydtoForCreate)
        {
           var result = await _service.DiaryEntryService.CreateDiaryEntry(DiaryId,  diaryEntrydtoForCreate, false);
           return Ok(result);
        }

        /// <summary>
        /// calls UpdateDiaryEntry,which updates the database values with those in the DiaryEntry object
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="entryId"></param>
        /// <param name="diaryEntrydto"></param>
        /// <returns>
        /// * return updated object from database.
        /// * return all links describe all behaviors related Contact.
        /// </returns>
        [HttpPut("UpdateDiaryEntry", Name = Update_DiaryEntry)]
        public async Task<IActionResult> UpdateDiaryEntry(Guid diaryId, Guid entryId, [FromBody] DiaryEntryDtoForUpdate diaryEntrydto)
        {
       
            var result =await _service.DiaryEntryService.UpdateDiaryEntry(diaryId, entryId, diaryEntrydto,  false, true);

            return Ok(result);
        }


        /// <summary>
        ///  Removes the DiaryEntry object from the collection at the specified index.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diaryEntryId"></param>
        /// <returns>Message Successfully deleted.</returns> 
        
        [HttpDelete("DeleteDiaryEntry", Name = Delete_DiaryEntry)]
        public async Task<OkApiResponse<string>> DeleteDiaryEntry(Guid diaryId,Guid diaryEntryId)
        {
            await _service.DiaryEntryService.DeleteDiaryEntry(diaryId , diaryEntryId, trackChanges: false);
            return new OkApiResponse<string>("Successfully deleted !");
        }

        /// <summary>
        /// array detailing which days have a diary entry associated with them.The array index matches with the day of the month(1 is the first of the month, 2 the second, and so on).
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="diaryEntryParameters"></param>
        /// <returns>
        /// * return array of  diaryEntries from database .
        /// * return all links describe all behaviors related Contact.
        /// </returns>

        [HttpGet("GetDaysInMonthWithEntries", Name = GetDays_InMonthWithEntries)]
        public async Task<IActionResult> GetDaysInMonthWithEntries(Guid diaryId, int Month, int Year, [FromQuery] DiaryEntryParameters diaryEntryParameters)
        {
            var pagedResult = await _service.DiaryEntryService.GetDaysInMonthWithEntries(diaryId, diaryEntryParameters, Month, Year ,  false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diaryEntries);
        }

        /// <summary>
        /// Returns a array containing a DataSet of diary entriesrecently created.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diaryEntryParameters"></param>
        /// <returns>
        /// * return array of  diaryEntries from database .
        /// * return all links describe all behaviors related Contact.
        /// </returns>

        [HttpGet("GetDiaryEntriesRecentlyChanged", Name = GetDiaryEntries_RecentlyChanged)]
        public async Task<IActionResult> GetDiaryEntriesRecentlyChanged(Guid diaryId, [FromQuery] DiaryEntryParameters diaryEntryParameters)
        {
            var pagedResult = await _service.DiaryEntryService.GetDiaryEntriesRecentlyChanged(diaryId, diaryEntryParameters,   false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diariyentries);
        }

        /// <summary>
        /// returns a
        /// FromDate As Date, ByVal ToDate object As Date) populated with rows from  the database detailing
        ///  diary entries between the  FromDate and ToDate  arguments.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="diaryEntryParameters"></param>
        /// <returns>
        /// * return array of  diaryEntries from database .
        /// * return all links describe all behaviors related Contact.
        /// </returns>

        [HttpGet("GetDiaryEntriesByDate" , Name = GetDiary_EntriesByDate)]
        public async Task<IActionResult> GetDiaryEntriesByDate(Guid diaryId, DateTime FromDate, DateTime ToDate,  [FromQuery] DiaryEntryParameters diaryEntryParameters)
        {
            var pagedResult = await _service.DiaryEntryService.GetDiaryEntriesByDate(diaryId, diaryEntryParameters, FromDate, ToDate, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.diariyentries);
        }
        /// <summary>
        /// Get Diary Entry By diarEntryId . 
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diarEntry"></param>
        /// <returns>
        /// * Return Object Diary Entry.
        /// * return all links describe all behaviors related Contact.
        /// </returns>
        [HttpGet("GetDiaryEntriesById", Name = GetDiary_EntriesById)]
        public async Task<IActionResult> GetDiaryEntriesById(Guid diaryId, Guid diarEntry)
        {
            var result = await _service.DiaryEntryService.GetDiaryEntriesById(diaryId, diarEntry, false);
            return Ok(result);
        }
        

    }
}
