using Api.Host.Domain.Entites;
using AutoMapper;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.Services.Contracts;
using Domain.Responses;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryDto;
using Shared.LoggerService;
using Shared.RequestParameters;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Services.Implmentation
{
    internal   sealed  class DiaryService : IDiaryService
    {
        private readonly IUnitOfWork _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidateIFexists _getEntityAndCheckIfExists;


        public DiaryService(IUnitOfWork repository, ILoggerManager logger, IMapper mapper, IValidateIFexists validateIFexists)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _getEntityAndCheckIfExists = validateIFexists;
        }

        public async Task<DiaryDto> CreateDiary(DiaryDtoForCreate diaryForCreationDto)
        {
            var diaryEntity = _mapper.Map<DiarY>(diaryForCreationDto);
            _repository.Diary.CreateDiary(diaryEntity);
            await _repository.CompleteAsync();
            var diaryToReturn = _mapper.Map<DiaryDto>(diaryEntity);
            return diaryToReturn;
        }

        
        public async Task<(IEnumerable<DiaryDto> diaries, MetaData metaData)> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges)
        {
     
            var diariesWithMetaData = await _repository.Diary.GetAllDiariesByUserId(diaryRequestParameter, trackChanges);

            var diariesDto = _mapper.Map<IEnumerable<DiaryDto>>(diariesWithMetaData);

            return (diaries: diariesDto, metaData: diariesWithMetaData.MetaData);
        }
        public async Task<DiaryDto> DeleteDiaryForUser(Guid DiaryId, bool trackChanges)
        {
            var diaryForDelete = await _getEntityAndCheckIfExists.GetDiaryAndCheckIfExists(DiaryId, trackChanges);
            _repository.Diary.DeleteDiary(diaryForDelete);
            _repository?.CompleteAsync();
            var diaryToReturn = _mapper.Map<DiaryDto>(diaryForDelete);
            return diaryToReturn;

        }

        public async Task<DiaryDto> UpdateDiary(Guid DiaryId, UpdateDiaryDto UpdateDiaryDto, bool trackChanges)
        {
            var diaryrntity = await _getEntityAndCheckIfExists.GetDiaryAndCheckIfExists(DiaryId, trackChanges);
            diaryrntity.UpdatedDate = DateTime.UtcNow;
            _mapper.Map(UpdateDiaryDto, diaryrntity);
            _repository?.CompleteAsync();
            var diaryDtoDto = _mapper.Map<DiaryDto>(diaryrntity);
            return diaryDtoDto;

        }
        public async Task<DiaryDto> GetDiaryById(Guid DiaryId, bool trackChanges)
        {
            var diaryrntity = await _getEntityAndCheckIfExists.GetDiaryAndCheckIfExists(DiaryId, trackChanges);
            var diaryToReturn = _mapper.Map<DiaryDto>(diaryrntity);
            return diaryToReturn;
        }


    }
}
