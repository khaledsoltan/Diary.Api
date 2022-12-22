using Configure.Hateoas.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOS.DiaryContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Diary.Utility.HatetosLinks.DiaryContact
{

    public static class DiaryContactLinks
    {
        public static void AddDiaryContactLinks(this IServiceCollection services) =>
          services
            .AddMvc()
            .AddLink(policy =>
            {
                policy
                  .AddPolicy<DiaryContactDto>(model =>
                  {
                      model
                        .AddCustomPath(m => $"/api/DiaryContact/GetContacts/{m.DiaryId}", "GetContacts", method: HttpMethods.Get, message: "@desc ( Get All Contacts by diaryId  .) @param (param name=diaryId => pass value to repository layer for get specific contacts based on diaryId. param name=contactRequestParameters =>   * PageSize : For get length of pages.   * OrderBy : For sorting entities by one field or more .  example : orderBy=name,age desc. @note : (Please leave a space between fields and orderBy type)  * SearchByContactName : For search by diaryName.  return * List Of Contacts. * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext} * Return all links describe all behaviors related Diary: object  for Create diaryEntity.)  @return (return diaryEntryEntity created and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/DiaryContact/CreateContact/{m.DiaryId}", "CreateContact", method: HttpMethods.Post, message: "@desc ( Saves a fully populated Contact object. If it’s a new contact, Save()   calls InsertNewContact sub, and the details are inserted into the database.) @param (diaryId  ,diaryContactDto : object  for update DiaryEntryDto.)  @return (* return object inserted ito database.  * return all links describe all behaviors related Contact)")
                        .AddCustomPath(m => $"/api/DiaryContact/UpdateContact/{m.DiaryId}/{m.Id}", "UpdateContact", method: HttpMethods.Put, message: "@desc ( calls UpdateContact, which updates the database values with those in the Contact object.) @param (DiaryId , contactId ,diaryContactForUpdate )  @return (return updated object from database. return all links describe all behaviors related Contact)")
                        .AddCustomPath(m => $"/api/DiaryContact/DeleteContact/{m.DiaryId}/{m.Id}", "DeleteContact", method: HttpMethods.Delete, message: "@desc (GetDaysInMonthWithEntries for get List of DiaryEntry .) @param (DiaryEntryParameters)  @return (return Contact Successfully Deleted! .)")
                        .AddCustomPath(m => $"/api/DiaryContact/GetContactsByID/{m.DiaryId}/{m.Id}", "GetContactsByID", method: HttpMethods.Get, message: "@desc (GetContacts object By contactID  and diaryId .) @param (diaryId, contactId)  @return (return Contact Object And return all links describe all behaviors related Contact )");
                  });
            });
    }
}
