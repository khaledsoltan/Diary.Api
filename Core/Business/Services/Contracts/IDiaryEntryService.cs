using Domain.Responses;
using Shared.DTOS.DiaryDto;
using Shared.DTOS.DiaryEntry;
using Shared.RequestParameters.Diary;
using Shared.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestParameters.DiaryEntry;

namespace Business.Diary.Services.Contracts
{
    public interface IDiaryEntryService
    {
        Task<GetDiaryEntryDto> CreateDiaryEntry(Guid diaryId,  DiaryEntryDtoForCreate diaryEntrydto, bool treackChange);

        Task<GetDiaryEntryDto> UpdateDiaryEntry(Guid DiaryId,  Guid DiaryEntryId, DiaryEntryDtoForUpdate diaryEntrydtoforUpdate, bool treackChangeDiary, bool treackChangeEntry);

        Task DeleteDiaryEntry(Guid diaryId, Guid diaryEntryId, bool trackChanges);

        Task<(MetaData metaData, IEnumerable<GetDiaryEntryDto>? diaryEntries)> GetDaysInMonthWithEntries(Guid diaryId, DiaryEntryParameters diaryEntryParameters, int Month, int Year, bool trackchange);

        Task<(IEnumerable<GetDiaryEntryDto> diariyentries, MetaData metaData)> GetDiaryEntriesByDate(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, DateTime FromDate, DateTime ToDate, bool trackchange);

        Task<(IEnumerable<GetDiaryEntryDto> diariyentries, MetaData metaData)> GetDiaryEntriesRecentlyChanged(Guid DiaryId, DiaryEntryParameters diaryEntryParameters,  bool trackchange);
    }
}
