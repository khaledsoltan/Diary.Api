using Configure.Hateoas.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOS.DiaryEntry;

namespace Api.Diary.Utility.HatetosLinks.DiaryEntry
{
    public static class DiaryEntryLinks
    {
        public static void AddDiaryEntriesLinks(this IServiceCollection services) =>
          services
            .AddMvc()
            .AddLink(policy =>
            {
                policy
                  .AddPolicy<GetDiaryEntryDto>(model =>
                  {
                      model
                        .AddCustomPath(m => $"/api/DiaryEntry/CreatEntryDiary/{m.DiaryId}", "CreatEntryDiary", method: HttpMethods.Post, message: "@desc (Saves a fully populated  DiaryEntry object. If it’s a  new entry, Save() calls InsertNewDiaryEntry sub and the details are inserted in to the database. The new DiaryEntryId is returned from the database and entered in to mDiaryEntryId.) @param (DiaryId , diaryEntrydtoForCreate)  @return (return object inserted ito database and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/UpdateDiaryEntry/{m.DiaryId}/{m.Id}", "UpdateDiaryEntry", method: HttpMethods.Put, message: "@desc (calls UpdateDiaryEntry,which updates the database values with those in the DiaryEntry object.) @param (diaryId ,entryId,diaryEntrydto)  @return (return updated object from database and links description  all behaviors related diary.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/DeleteDiaryEntry/{m.DiaryId}/{m.Id}", "DeleteDiaryEntry", method: HttpMethods.Delete, message: "@desc (Removes the DiaryEntry object from the collection at the specified index.) @param (diaryId,diaryEntryId)  @return (return Message Successfully deleted.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/GetDaysInMonthWithEntries/{m.DiaryId}", "GetDaysInMonthWithEntries", method: HttpMethods.Get, message: "@desc (array detailing which days have a diary entry associated with them.The array index matches with the day of the month(1 is the first of the month, 2 the second, and so on).) @param (diaryId,Month,Year,diaryEntryParameters)  @return ( return array of  diaryEntries from database and all links describe all behaviors related Contact.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/GetDiaryEntriesRecentlyChanged/{m.DiaryId}", "GetDiaryEntriesRecentlyChanged", method: HttpMethods.Get, message: "@desc (Returns a array containing a DataSet of diary entriesrecently created.) @param (diaryId, diaryEntryParameters)  @return (return array of  diaryEntries from database and all links describe all behaviors related Contact.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/GetDiaryEntriesByDate/{m.DiaryId}", "GetDiaryEntriesByDate", method: HttpMethods.Get, message: "@desc (returns a  populated with rows from  the database detailing diary entries between the  FromDate and ToDate  arguments.) @param (diaryId,FromDate,ToDate,diaryEntryParameters )  @return (return array of  diaryEntries from database and all links describe all behaviors related Contact.)")
                        .AddCustomPath(m => $"/api/DiaryEntry/GetDiaryEntriesById/{m.DiaryId}/{m.Id}", "GetDiaryEntriesById", method: HttpMethods.Get, message: "@desc ( Get Diary Entry By diarEntryId . ) @param (diaryId,diarEntry)  @return ( Return Object Diary Entry and all links describe all behaviors related Contact.)");
                  });
            });
    }
}
