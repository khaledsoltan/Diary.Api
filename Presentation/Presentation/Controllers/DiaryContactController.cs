using Api.Host.Domain.Entites;
using Business.ServiceLocator.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presentation.Diary.Controllers
{

    /// <summary>
    /// The Contact class objectifies a single contact — a person or thing for which you want to store contact
    /// information.It encapsulates everything to do with contacts, including the storing and retrieving of contact information in the database.
    /// </summary>
    /// 
    [Authorize]
    [Route("api/DiaryContact")]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class DiaryContactController : ControllerBase
    {

        private readonly IServiceLocator _service;

        public DiaryContactController(IServiceLocator service) => _service = service;

        /// <summary>
        /// Get All Contacts by diaryId 
        /// </summary>
        /// <param name="diaryId"></param>
        /// pass value to repository layer for get specific contacts based on diaryId.
        /// <param name="cntactRequestParameters"></param>
        /// * PageSize : For get length of pages
        /// * OrderBy : For sorting entities by one field or more . 
        ///   example : orderBy=name,age desc. 
        ///   @note : (Please leave a space between fields and orderBy type)               
        /// * Fields : For get specific fields from entity
        /// * SearchByContactName : For search by diaryName
        /// <returns>
        /// * List Of Contacts.
        /// * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext}
        /// * Return all links describe all behaviors related Diary
        /// </returns>
        [HttpGet("GetContacts")]
        public async Task<IActionResult> GetContacts(Guid diaryId ,[FromQuery] ContactRequestParameters cntactRequestParameters)
        {
            var pagedResult = await _service.DiaryContactService.GetAllcontactsByDiaryId(diaryId, cntactRequestParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.contacts);
        }


        /// <summary>
        /// Saves a fully populated Contact object. If it’s a new contact, Save()
        /// calls InsertNewContact sub, and the details are inserted into the database.
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="diaryContactDto"></param>
        /// <returns>
        /// * return object inserted ito database.
        /// * return all links describe all behaviors related Contact
        /// </returns>

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateContact(Guid diaryId, [FromBody] DiaryContactForCreate diaryContactDto)
        {
            var result = await _service.DiaryContactService.CreateContact(diaryId , diaryContactDto,false );
            return Ok(result);
        }

        /// <summary>
        /// calls UpdateContact, which
        /// updates the database values with
        /// those in the Contact object.
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="contactId"></param>
        /// <param name="diaryContact"></param>
        /// <returns>
        /// return updated object from database.
        /// return all links describe all behaviors related Contact
        /// </returns>

        [HttpPut("UpdateContact")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult>  UpdateContact(Guid DiaryId, Guid contactId, [FromBody] DiaryContactForUpdate diaryContact)
        {
            var result = await _service.DiaryContactService.UpdateContact(DiaryId, contactId, diaryContact, true);
            return Ok(result);
        }

        /// <summary>
        ///  deletes the Contact object from the database
        /// with a ContactId value equal to the ContactId argument of the method
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="contactId"></param>
        /// <returns>Message Successfully deleted.</returns>

        [HttpDelete("DeleteContact")]
        public async Task<OkApiResponse<string>> DeleteContact(Guid DiaryId, Guid contactId)
        {
            await _service.DiaryContactService.DeleteContact(DiaryId,  contactId, false);
            return new OkApiResponse<string>("Contact Successfully Deleted!");
        }

        /// <summary>
        /// GetContacts object By contactID  and diaryId
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="contactId"></param>
        /// <returns>
        /// * Return Contact Object  
        /// </returns>
        [HttpGet("GetContactsByID")]
        public async Task<IActionResult>  GetContactsByID(Guid diaryId,Guid contactId)
        {
            var Getcontact =await _service.DiaryContactService.GetContactByContactId(diaryId,contactId, false);
            return Ok(Getcontact);
        }
    }
}
