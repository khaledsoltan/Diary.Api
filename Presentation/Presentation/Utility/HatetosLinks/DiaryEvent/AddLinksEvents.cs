using Configure.Hateoas.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOS.DiaryEvent;

namespace Api.Diary.Utility.HatetosLinks.DiaryEvent
{

    public static class AddLinksEvents
    {
        public static void AddDiaryEvents(this IServiceCollection services) =>
          services
            .AddMvc()
            .AddLink(policy =>
            {
                policy
                  .AddPolicy<DiaryEventDto>(model =>
                  {
                      model
                        .AddCustomPath(m => $"/api/DiaryEvent/CreateNewEvent/{m.DiaryId}", "CreateNewEvent", method: HttpMethods.Post, message: "@desc ( Creates a new DiaryEvent object with all properties set to their default values.) @param (diaryId,diaryEventForCreate)  @return (return object inserted ito database and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/DiaryEvent/UpdateDiaryEvent/{m.DiaryId}/{m.Id}", "UpdateDiaryEvent", method: HttpMethods.Put, message: "@desc (calls UpdateDiaryEvent, which updates the database values with those in the DiaryEvent object.) @param (diaryId,eventId,diaryEventForUpdate)  @return (return updated object from database and all links describe all behaviors related Contact.)")
                        .AddCustomPath(m => $"/api/DiaryEvent/DeleteDiaryEvent/{m.DiaryId}/{m.Id}", "DeleteDiaryEvent", method: HttpMethods.Delete, message: "@desc (deletes the event from the database with an EventId value equal to the EventId argument of the method.) @param (diaryId, diaryEventId)  @return (return DiaryEvent has been deleted successfully!)")
                        .AddCustomPath(m => $"/api/DiaryEvent/GetDaysInMonthWithEntries/{m.DiaryId}", "GetDaysInMonthWithEvents", method: HttpMethods.Get, message: "@desc (returns array have events associated with them.The array index matches with the day of the month(1 is the first of the month, 2 the second, and so on).) @param (diaryID,Month,Year,diaryEventsParameters)  @return (* List Of Events.  * Metadata for information pagination {pagsize,currenpage,totalcount, totalpages, HasPrevious, HasNext}  * Return all links describe all behaviors related Diary.)")
                        .AddCustomPath(m => $"/api/DiaryEvent/GetDiaryEventsByDate/{m.DiaryId}", "GetDiaryEventsByDate", method: HttpMethods.Get, message: "@desc (returns a  objects populated with rows from the database detailing diary events between the FromDate and ToDate arguments.) @param (DiaryId,FromDate,ToDate,diaryEventsParameters)  @return (return list of Events and metadata.)")
                        .AddCustomPath(m => $"/api/DiaryEvent/GetDiaryEventsRecentlyChanged/{m.DiaryId}", "GetDiaryEventsRecentlyChanged", method: HttpMethods.Get, message: "@desc ( returns a  objects populated with rows from the database detailing diary events RecentlyChanged.) @param (DiaryId,diaryEventsParameters)  @return (return list of  Events and metadata .)")
                        .AddCustomPath(m => $"/api/DiaryEvent/GetDiaryEventById/{m.DiaryId}/{m.Id}", "GetDiaryEventById", method: HttpMethods.Get, message: "@desc ( Get Diary Event By Id.) @param (DiaryId,EventId)  @return (return  object from database and all links describe all behaviors related Contact.)");

                      

                  });
            });
    }

}
