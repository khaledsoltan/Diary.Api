using Api.Host.Domain.Entites;
using Shared.PagedList;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Contracts
{
    public interface IDiaryEventRepository
    {
        void CreateNewDiaryEvent(DiaryEvent diaryEvent, Guid diaryId);
        Task<DiaryEvent> GetDiaryEventByEventId(Guid diaryId, Guid diaryEventId, bool trackchange);
        void DeleteDiaryEvent(DiaryEvent diaryEvent);
        Task<PagedList<DiaryEvent>> GetDaysInMonthWithEntries(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, int Month, int Year, bool trackchange);
        Task<PagedList<DiaryEvent>>  GetDiaryEntriesByDate(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, DateTime FromDate, DateTime ToDate, bool trackchange);
        Task<PagedList<DiaryEvent>> GetDiaryEntriesRecentlyChanged(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, bool trackchange);
    }
}
