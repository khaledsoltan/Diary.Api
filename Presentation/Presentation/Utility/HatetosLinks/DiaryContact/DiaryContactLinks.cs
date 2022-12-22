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
                        .AddCustomPath(m => $"/api/Diary/CreatEentryDiary/{m.DiaryId}", "Creat_EntryDiary", method: HttpMethods.Post, message: "@desc (CreatEentryDiary for Create EntryDiary .) @param (DiaryEntryDtoForCreate  diaryEntryDtoForCreate : object  for Create diaryEntity.)  @return (return diaryEntryEntity created and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/Diary/UpdateDiaryForDiaryEntry/{m.DiaryId}/{m.Id}", "Update_DiaryEntry", method: HttpMethods.Post, message: "@desc (UpdateDiaryForDiaryEntry for Update EntryDiary .) @param (DiaryEntryDtoForUpdate  diaryEntryDtoForUpdate : object  for update DiaryEntryDto.)  @return (return diaryEntryEntity updated and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/Diary/DeleteDiaryEntry/{m.DiaryId}/{m.Id}", "Delete_DiaryEntry", method: HttpMethods.Post, message: "@desc (DeleteDiaryEntry for Delete EntryDiary .) @param (diaryId : for get diaryEntity. , diaryEntryId : for get diaryEntry)  @return (return messege succesed .)")
                        .AddCustomPath(m => $"/api/Diary/GetDaysInMonthWithEntries/{m.DiaryId}", "GetDays_InMonthWithEntries", method: HttpMethods.Post, message: "@desc (GetDaysInMonthWithEntries for get List of DiaryEntry .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEntriesRecentlyChanged/{m.DiaryId}", "GetDiary_EntriesRecentlyChanged", method: HttpMethods.Post, message: "@desc (GetDiaryEntriesRecentlyChanged for get List of DiaryEntry .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEntriesByDate/{m.DiaryId}", "GetDiary_EntriesByDate", method: HttpMethods.Post, message: "@desc (GetDiaryEntriesByDate for get List of DiaryEntries .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)")
                        .AddCustomPath(m => $"/api/Diary/GetDiaryEntriesById/{m.DiaryId}/{m.Id}", "GetDiary_EntriesById", method: HttpMethods.Post, message: "@desc (GetDiaryEntriesByDate for get List of DiaryEntries .) @param (DiaryEntryParameters)  @return (return list of  diaryentries .)");

                  });
            });
    }
}
