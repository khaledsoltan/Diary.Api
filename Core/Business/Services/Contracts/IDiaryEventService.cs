using Shared.DTOS.DiaryEvent;
using Shared.RequestParameters;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Services.Contracts
{
    public interface IDiaryEventService
    {
        Task<DiaryEventDto> CreateDiaryEvent(Guid diaryId, DiaryEventDto diaryEventDto, bool treackChange);
        Task<DiaryEventDto> UpdateDiaryEvent(Guid DiaryId, Guid DiaryEventId, DiaryEventDto diaryEventDto, bool treackChangeDiary, bool treackChangeEvent);
        Task DeleteDiaryEvent(Guid diaryId, Guid DiaryEventId, bool trackChanges);
        Task<(IEnumerable<DiaryEventDto> events, MetaData metaData)> GetDiaryEventsRecentlyChanged(Guid diaryId, DiaryEventsParameters diaryEventsParameters, bool trackchange);
        Task<(MetaData metaData, IEnumerable<DiaryEventDto> diaries)> GetDiaryEventsByDate(Guid diaryId, DiaryEventsParameters diaryEventsParameters, DateTime FromDate, DateTime ToDate, bool trackchange);
        Task<(MetaData metaData, IEnumerable<DiaryEventDto>? events)> GetDaysInMonthWithEvents(Guid diaryId,  DiaryEventsParameters diaryEventsParameters, int Month, int Year, bool trackchange);

    }
}
