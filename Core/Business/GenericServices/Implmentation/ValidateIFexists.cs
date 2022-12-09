using Api.Host.Domain.Entites;
using Business.Diary.GenericServices.Contracts;
using Domain.Diary.Exceptions.DiaryEntryExceptions;
using Domain.Diary.Exceptions.DiaryEventExceptions;
using Domain.Diary.Exceptions.Folder;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.GenericServices.Implmentation
{
    public class ValidateIFexists : IValidateIFexists
    {
        private readonly IUnitOfWork _repository;
        public ValidateIFexists(IUnitOfWork unitOfWork) {
            _repository = unitOfWork;
        }
        public async Task<DiarY> GetDiaryAndCheckIfExists(Guid diaryId, bool trackChange)
        {
            var diary = await _repository.Diary.GetDiaryByIdAndUserId(diaryId, trackChange);
            _ = diary ?? throw new DiaryNotFoundException(diaryId);
            return diary;
        }
        public async Task<DiaryEvent> GetDiaryEventAndCheckIfExists(Guid diaryId, Guid diaryEventId, bool trackChange)
        {
            var diaryEvent = await _repository.DiaryEvent.GetDiaryEventByEventId(diaryId, diaryEventId, trackChange);
            _ = diaryEvent ?? throw new DiaryEventNotFoundException(diaryId);
            return diaryEvent;
        }
        public async Task<DiaryEntry> GetDiarEntryAndCheckIfExists(Guid diaryId, Guid diaryEntryId, bool trackChange)
        {
            var diaryEntry = await _repository.DiaryEntry.GetDiaryEntriesById(diaryId, diaryEntryId, trackChange);
            _ = diaryEntry ?? throw new DiaryEntryNotFoundException(diaryId);
            return diaryEntry;
        }


    }
}
