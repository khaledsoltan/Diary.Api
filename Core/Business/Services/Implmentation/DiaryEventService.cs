using Api.Host.Domain.Entites;
using AutoMapper;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.Services.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryDto;
using Shared.DTOS.DiaryEvent;
using Shared.LoggerService;
using Shared.RequestParameters;
using Shared.RequestParameters.Diary;
using Shared.RequestParameters.DiaryEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Diary.Services.Implmentation
{
    internal sealed class DiaryEventService : IDiaryEventService
    {
        private readonly IUnitOfWork _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidateIFexists _checkIfExists;

        public DiaryEventService(IUnitOfWork repository, ILoggerManager logger, IMapper mapper, IValidateIFexists validateIFexists)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _checkIfExists = validateIFexists;
        }

        public async Task<DiaryEventDto> CreateDiaryEvent(Guid diaryId, DiaryEventForCreate diaryEventDto, bool treackChange)
        {
            await  _checkIfExists.GetDiaryAndCheckIfExists(diaryId, treackChange);
            var diaryEvent = _mapper.Map<DiaryEvent>(diaryEventDto);
            _repository.DiaryEvent.CreateNewDiaryEvent(diaryEvent, diaryId);
            await _repository.CompleteAsync();
            var diaryEventForReturn = _mapper.Map<DiaryEventDto>(diaryEvent);
            return diaryEventForReturn;
        }

        public async Task DeleteDiaryEvent(Guid diaryId, Guid DiaryEventId, bool trackChanges)
        {
           var diaryEvent = await  _checkIfExists.GetDiaryEventAndCheckIfExists(diaryId, DiaryEventId, trackChanges);
            _repository.DiaryEvent.DeleteDiaryEvent(diaryEvent);
            await _repository.CompleteAsync();
        }

      

        public async Task<(MetaData metaData ,IEnumerable<DiaryEventDto>? events)> GetDaysInMonthWithEvents(Guid diaryId,  DiaryEventsParameters diaryEventsParameters, int Month, int Year, bool trackchange)
        {
            var eventsWithMetaData = await _repository.DiaryEvent.GetDaysInMonthWithEntries(diaryId, diaryEventsParameters, Month, Year, trackchange);

            var eventsDto = _mapper.Map<IEnumerable<DiaryEventDto>>(eventsWithMetaData);

            return (eventsWithMetaData.MetaData , eventsDto);
        }

       
        public async Task<(MetaData metaData , IEnumerable<DiaryEventDto> diaries )> GetDiaryEventsByDate(Guid diaryId,  DiaryEventsParameters diaryEventsParameters, DateTime FromDate, DateTime ToDate, bool trackchange)
        {
            var eventsWithMetaData = await  _repository.DiaryEvent.GetDiaryEntriesByDate(diaryId, diaryEventsParameters, FromDate, ToDate, trackchange);

            var eventsDto = _mapper.Map<IEnumerable<DiaryEventDto>>(eventsWithMetaData);

            return (eventsWithMetaData.MetaData, eventsDto);
        }

        public async Task<(IEnumerable<DiaryEventDto> events, MetaData metaData)> GetDiaryEventsRecentlyChanged(Guid diaryId, DiaryEventsParameters diaryEventsParameters, bool trackchange)
        {
            var eventsWithMetaData = await _repository.DiaryEvent.GetDiaryEntriesRecentlyChanged(diaryId, diaryEventsParameters,  trackchange);

            var eventsDto = _mapper.Map<IEnumerable<DiaryEventDto>>(eventsWithMetaData);

            return (eventsDto, eventsWithMetaData.MetaData);
        }

        public async Task<DiaryEventDto> UpdateDiaryEvent(Guid diaryId, Guid diaryEventId, DiaryEventForUpdate diaryEventForUpdate, bool treackChangeDiary, bool treackChangeEvent)
        {
            await _checkIfExists.GetDiaryAndCheckIfExists(diaryId, treackChangeDiary);
            var eventEntity = await _checkIfExists.GetDiaryEventAndCheckIfExists(diaryId, diaryEventId, treackChangeDiary);
            _mapper.Map(eventEntity, diaryEventForUpdate);
            _repository?.CompleteAsync();
            var eventDto = _mapper.Map<DiaryEventDto>(eventEntity);
            return eventDto;
        }
        public async Task<DiaryEventDto> GetDiaryEventById(Guid DiaryId, Guid DiaryEventId, bool trackChange)
        {
            var diaryEventEntity = await _checkIfExists.GetDiaryEventAndCheckIfExists(DiaryId , DiaryEventId, trackChange);
            var diaryEventDto = _mapper.Map<DiaryEventDto>(diaryEventEntity);
            return diaryEventDto;

        }

    }
}
