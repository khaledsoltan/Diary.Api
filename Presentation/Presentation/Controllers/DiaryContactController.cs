using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DTOS.DiaryContact;
using Shared.RequestParameters.Contact;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Diary.Controllers
{
    [Route("api/DiaryContact")]
    [ApiController]
    [Authorize]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryContactController : ControllerBase
    {

        private readonly IServiceLocator _service;

        public DiaryContactController(IServiceLocator service) => _service = service;


        [HttpGet("GetContacts")]
        public async Task<IActionResult> GetContacts(Guid diaryId ,[FromQuery] ContactRequestParameters cntactRequestParameters)
        {
            var pagedResult = await _service.DiaryContactService.GetAllcontactsByDiaryId(diaryId, cntactRequestParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.contacts);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateContact(Guid diaryId, [FromBody] DiaryContactForCreate diaryContactDto)
        {
            var result = await _service.DiaryContactService.CreateContact(diaryId , diaryContactDto,false );
            return Ok(result);
        }

        [HttpPut("UpdateContact")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult>  UpdateContact(Guid DiaryId, Guid contactId, [FromBody] DiaryContactForUpdate diaryContact)
        {
            var result = await _service.DiaryContactService.UpdateContact(DiaryId, contactId, diaryContact, true);
            return Ok(result);
        }

        [HttpDelete("DeleteContact")]
        public async Task<OkApiResponse<string>> DeleteContact(Guid DiaryId, Guid contactId)
        {
            await _service.DiaryContactService.DeleteContact(DiaryId,  contactId, false);
            return new OkApiResponse<string>("Contat Deleted Succesfuly");
        }

        [HttpGet("GetContactsByID")]
        public async Task<IActionResult>  GetContactsByID(Guid diaryId,Guid contactId)
        {
            var Getcontact =await _service.DiaryContactService.GetContactByContactId(diaryId,contactId, false);
            return Ok(Getcontact);
        }
    }
}
