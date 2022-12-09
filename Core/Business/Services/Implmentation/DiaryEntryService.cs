using Api.Host.Domain.Entites;
using AutoMapper;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.Services.Contracts;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryEvent;
using Shared.LoggerService;
using Shared.RequestParameters.DiaryEvents;
using Shared.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.DiaryEntry;
using Domain.Responses;
using Shared.RequestParameters.DiaryEntry;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Diary.Services.Implmentation
{
    internal sealed class DiaryEntryService : IDiaryEntryService
    {
        private readonly IUnitOfWork _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidateIFexists _GetAndcheckIfExists;

        public DiaryEntryService(IUnitOfWork repository, ILoggerManager logger, IMapper mapper, IValidateIFexists validateIFexists)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _GetAndcheckIfExists = validateIFexists;
        }
        public async Task<GetDiaryEntryDto> GetDiaryEntriesById(Guid diaryId, Guid diaryEntryId, bool trackChanges)
        {
            var diaryEntry = await _GetAndcheckIfExists.GetDiarEntryAndCheckIfExists(diaryId, diaryEntryId, trackChanges);
            var diaryEntryForReturn = _mapper.Map<GetDiaryEntryDto>(diaryEntry);
            return diaryEntryForReturn;
        }

        public async Task<GetDiaryEntryDto> CreateDiaryEntry(Guid diaryId, DiaryEntryDtoForCreate diaryEntrydto, bool treackChange)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(diaryId , false);
            var diaryEntry = _mapper.Map<DiaryEntry>(diaryEntrydto);
            diaryEntry.DiaryId = diaryId;
            _repository.DiaryEntry.CreateDiaryEntry(diaryEntry);
            await _repository.CompleteAsync();
            var diaryEntryCreatedForReturn = _mapper.Map<GetDiaryEntryDto>(diaryEntry);
            return diaryEntryCreatedForReturn;

        }

        public async Task DeleteDiaryEntry(Guid diaryId, Guid diaryEntryId, bool trackChanges)
        {
           await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(diaryId, false);
           var diaryEntryentity = await _GetAndcheckIfExists.GetDiarEntryAndCheckIfExists(diaryId , diaryEntryId, trackChanges);
           _repository.DiaryEntry.DeleteDiaryEntry(diaryEntryentity);
           await _repository.CompleteAsync();
        }

      

        public async Task<(MetaData metaData, IEnumerable<GetDiaryEntryDto>? diaryEntries)> GetDaysInMonthWithEntries(Guid diaryId,  DiaryEntryParameters diaryEntryParameters, int Month, int Year, bool trackchange)
        {
            var diariessWithMetaData = await _repository.DiaryEntry.GetDaysInMonthWithEntries(diaryId, diaryEntryParameters, Month, Year, trackchange);

            var diaryentryDto = _mapper.Map<IEnumerable<GetDiaryEntryDto>>(diariessWithMetaData);

            return (diariessWithMetaData.MetaData, diaryentryDto);
        }

        public async Task<(IEnumerable<GetDiaryEntryDto> diariyentries, MetaData metaData)> GetDiaryEntriesByDate(Guid diaryId, DiaryEntryParameters diaryEntryParameters, DateTime FromDate, DateTime ToDate, bool trackchange)
        {
            var diariessWithMetaData = await _repository.DiaryEntry.GetDiaryEntriesByDate(diaryId, diaryEntryParameters, FromDate, ToDate, trackchange);

            var diaryentryDto = _mapper.Map<IEnumerable<GetDiaryEntryDto>>(diariessWithMetaData);

            return (diaryentryDto, diariessWithMetaData.MetaData);
        }

        public async Task<(IEnumerable<GetDiaryEntryDto> diariyentries, MetaData metaData)> GetDiaryEntriesRecentlyChanged(Guid diaryId, DiaryEntryParameters diaryEntryParameters, bool trackchange)
        {
            var diariessWithMetaData = await _repository.DiaryEntry.GetDiaryEntriesRecentlyChanged(diaryId, diaryEntryParameters, trackchange);

            var diaryentryDto = _mapper.Map<IEnumerable<GetDiaryEntryDto>>(diariessWithMetaData);

            return (diaryentryDto, diariessWithMetaData.MetaData);
        }

        public async Task<GetDiaryEntryDto> UpdateDiaryEntry(Guid DiaryId, Guid DiaryEntryId, DiaryEntryDtoForUpdate diaryEntrydtoforUpdate, bool treackChangeDiary, bool treackChangeEntry)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(DiaryId, treackChangeDiary);
            var diarEntryEntity =await  _GetAndcheckIfExists.GetDiarEntryAndCheckIfExists(DiaryId , DiaryEntryId, treackChangeEntry);
            _mapper.Map(diaryEntrydtoforUpdate, diarEntryEntity);
            await _repository.CompleteAsync();
            var diaryEntryReturn = _mapper.Map<GetDiaryEntryDto>(diarEntryEntity);
            return diaryEntryReturn;
        }
    }
}
