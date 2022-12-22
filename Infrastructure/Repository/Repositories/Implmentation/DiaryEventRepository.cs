using Api.Host.Domain.Entites;
using Domain.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Diary.Extensions.RepositoryExtensions;
using Repository.Diary.Repositories.Contracts;
using Repository.GenericRepository;
using Shared.PagedList;
using Shared.RequestParameters.Diary;
using Shared.RequestParameters.DiaryEvents;

namespace Repository.Diary.Repositories.Implmentation
{
  internal sealed class DiaryEventRepository : GenericRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateNewDiaryEvent(DiaryEvent diaryEvent, Guid diaryId) {
            diaryEvent.DiaryId = diaryId;
            Create(diaryEvent);
        } 

         public async Task<DiaryEvent>  GetDiaryEventByEventId(Guid diaryId,Guid diaryEventId, bool trackchange)
                => await FindByCondition(x => x.Id.Equals(diaryEventId) && x.DiaryId.Equals(diaryId), trackchange).SingleOrDefaultAsync();


        public void DeleteDiaryEvent(DiaryEvent diaryEvent) => Delete(diaryEvent);


        public async Task<PagedList<DiaryEvent>> GetDaysInMonthWithEntries(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, int Month, int Year, bool trackchange)
        {

            var diaryEvents = await FindByCondition(x => x.DiaryId.Equals(DiaryId) && x.EventDate.Month.Equals(Month) && x.EventDate.Year.Equals(Year), trackchange)
                     .ToListAsync();

            return PagedList<DiaryEvent>.ToPagedList(diaryEvents, diaryEventsParameters.PageNumber, diaryEventsParameters.PageSize);

        }

        public async Task<PagedList<DiaryEvent>> GetDiaryEntriesByDate(Guid DiaryId, DiaryEventsParameters diaryEventsParameters, DateTime FromDate, DateTime ToDate, bool trackchange)
        {

            var diaryEvents = await FindByCondition(x => x.DiaryId.Equals(DiaryId) && x.EventDate >= FromDate && x.EventDate <= ToDate, trackchange).ToListAsync();

            return PagedList<DiaryEvent>.ToPagedList(diaryEvents, diaryEventsParameters.PageNumber, diaryEventsParameters.PageSize);

        }

        public async Task<PagedList<DiaryEvent>> GetDiaryEntriesRecentlyChanged(Guid DiaryId, DiaryEventsParameters diaryEventsParameters,bool trackchange)
        {

            var diaryEvents = await FindByCondition(x => x.DiaryId.Equals(DiaryId), trackchange).OrderByDescending(x => x.EventDate).ToListAsync();

            return PagedList<DiaryEvent>
                .ToPagedList(diaryEvents, diaryEventsParameters.PageNumber, diaryEventsParameters.PageSize);
        }

       
    }
}
