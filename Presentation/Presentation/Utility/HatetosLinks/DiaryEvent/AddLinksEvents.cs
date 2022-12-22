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
                        .AddCustomPath(m => $"/api/Diary/CreateNewEvent/{m.DiaryId}", "CreateNewEvent", method: HttpMethods.Post, message: "@desc (CreateNewEvent for Create EventDiary .) @param (DiaryEventDto  DiaryEventDto : object  for Create DiaryEventDto.)  @return (return DiaryEventDto created and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/Diary/UpdateDiaryEvent/{m.DiaryId}/{m.Id}", "UpdateDiaryEvent", method: HttpMethods.Put, message: "@desc (DiaryEventDto for Update UpdateDiaryEvent .) @param (DiaryEventDto  DiaryEventDto : object  for update DiaryEntryDto.)  @return (return DiaryEventDto updated and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/Diary/DeleteDiaryEvent/{m.DiaryId}/{m.Id}", "DeleteDiaryEvent", method: HttpMethods.Delete, message: "@desc (DeleteDiaryEvent for Delete event .) @param (diaryId : for get eventEntity. , diaryEntryId : for get diaryEntry)  @return (return messege succesed .)")
                        .AddCustomPath(m => $"/api/Diary/GetDaysInMonthWithEntries/{m.DiaryId}", "GetDaysInMonthWithEvents", method: HttpMethods.Get, message: "@desc (GetDaysInMonthWithEvents for get List of DiaryEventBasedCodition .) @param (DiaryEvents)  @return (return list of  diaryEventDto .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEventsByDate/{m.DiaryId}", "GetDiaryEventsByDate", method: HttpMethods.Get, message: "@desc (GetDiaryEventsByDate for get List of DiaryEventBasedCodition .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEventsRecentlyChanged/{m.DiaryId}", "GetDiaryEventsRecentlyChanged", method: HttpMethods.Get, message: "@desc (GetDiaryEventsRecentlyChanged for get List of DiaryEntries .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEventById/{m.DiaryId}/{m.Id}", "GetDiaryEventById", method: HttpMethods.Get, message: "@desc (GetDiaryEventsRecentlyChanged for get List of DiaryEntries .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)");

                      

                  });
            });
    }

}
