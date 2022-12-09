using Api.Host.Domain.Entites;
using Shared.PagedList;
using Shared.RequestParameters.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Contracts
{
    public interface IDiaryEntryRepository
    {
        void CreateDiaryEntry(DiaryEntry diaryEntry);
        void DeleteDiaryEntry(DiaryEntry diaryEntry);
        Task<DiaryEntry> GetDiaryEntriesById(Guid diaryId,Guid diaryentryId, bool trackchange);
        Task<PagedList<DiaryEntry>> GetDaysInMonthWithEntries(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, int Month, int Year, bool trackchange);
        Task<PagedList<DiaryEntry>> GetDiaryEntriesByDate(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, DateTime FromDate, DateTime ToDate, bool trackchange);
        Task<PagedList<DiaryEntry>> GetDiaryEntriesRecentlyChanged(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, bool trackchange);
    }
}
