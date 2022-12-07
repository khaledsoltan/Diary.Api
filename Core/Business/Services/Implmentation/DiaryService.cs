using Api.Host.Domain.Entites;
using AutoMapper;
using Business.Diary.Services.Contracts;
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

        public DiaryService(IUnitOfWork repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }

        public DiaryDto CreateDiary(DiaryDto diaryForCreationDto)
        {
            var diaryEntity = _mapper.Map<DiarY>(diaryForCreationDto);
            _repository.Diary.CreateDiary(diaryEntity);
            _repository?.CompleteAsync();
            var diaryToReturn = _mapper.Map<DiaryDto>(diaryEntity);
            return  diaryToReturn;
        }
      
        public async Task<(IEnumerable<DiaryDto> diaries, MetaData metaData)> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges)
        {
            //if (!diaryRequestParameter.ValidDateRange)
            //    throw new Exception();

            var diariesWithMetaData = await _repository.Diary.GetAllDiariesByUserId(diaryRequestParameter, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<DiaryDto>>(diariesWithMetaData);

            return (employees: employeesDto, metaData: diariesWithMetaData.MetaData);
        }
        public async void DeleteDiaryForUser(Guid DiaryId, string Userid, bool trackChanges)
        {
            var diaryForDelete = await _repository.Diary.GetDiaryByIdAndUserId(DiaryId, trackChanges);
            if (diaryForDelete is null)
                throw new Exception();
            _repository.Diary.DeleteDiary(diaryForDelete);
            _repository?.CompleteAsync();
        }

        public async Task<DiaryDto> UpdateDiary(Guid DiaryId, string Userid, UpdateDiaryDto UpdateDiaryDto, bool trackChanges)
        {
            var diaryrntity = await _repository.Diary.GetDiaryByIdAndUserId(DiaryId, trackChanges);
            _mapper.Map(UpdateDiaryDto, diaryrntity);
            _repository?.CompleteAsync();
            var diaryDtoDto = _mapper.Map<DiaryDto>(diaryrntity);
            return diaryDtoDto;

        }

      
    }
}
