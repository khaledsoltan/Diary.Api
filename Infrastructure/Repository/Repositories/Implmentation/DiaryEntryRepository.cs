using Api.Host.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Diary.Repositories.Contracts;
using Repository.GenericRepository;
using Shared.PagedList;
using Shared.RequestParameters.DiaryEntry;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Implmentation
{
   
    internal sealed class DiaryEntryRepository : GenericRepository<DiaryEntry>, IDiaryEntryRepository
    {
        public DiaryEntryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.CreatedDate = DateTime.Now;
            Create(diaryEntry);
        }

       public async Task<DiaryEntry> GetDiaryEntriesById(Guid diaryId ,Guid DiaryentryId, bool trackchange) 
            =>await FindByCondition(x => x.Id.Equals(DiaryentryId) && x.DiaryId.Equals(diaryId) , trackchange).SingleOrDefaultAsync();

       public void DeleteDiaryEntry(DiaryEntry diaryEntry) => Delete(diaryEntry);

        public async Task<PagedList<DiaryEntry>> GetDaysInMonthWithEntries(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, int Month, int Year, bool trackchange)
        {

            var diaryEntries = await FindByCondition(x => x.DiaryId.Equals(DiaryId) && x.EntryDate.Month.Equals(Month) && x.EntryDate.Year.Equals(Year), trackchange)
                .ToListAsync();

            return PagedList<DiaryEntry>.ToPagedList(diaryEntries, diaryEntryParameters.PageNumber, diaryEntryParameters.PageSize);

        }

        public async Task<PagedList<DiaryEntry>> GetDiaryEntriesByDate(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, DateTime FromDate, DateTime ToDate, bool trackchange)
        {

            var diaryEntries = await FindByCondition(x => x.DiaryId.Equals(DiaryId) && x.EntryDate >= FromDate && x.EntryDate <= ToDate, trackchange).ToListAsync();


            return PagedList<DiaryEntry>.ToPagedList(diaryEntries, diaryEntryParameters.PageNumber, diaryEntryParameters.PageSize);

        }

        public async Task<PagedList<DiaryEntry>> GetDiaryEntriesRecentlyChanged(Guid DiaryId, DiaryEntryParameters diaryEntryParameters, bool trackchange)
        {
            var diaryEntries = await FindByCondition(x => x.DiaryId.Equals(DiaryId), trackchange).OrderByDescending(x => x.CreatedDate).ToListAsync();

            return PagedList<DiaryEntry>.ToPagedList(diaryEntries, diaryEntryParameters.PageNumber, diaryEntryParameters.PageSize);

        }

      


    }
}

