using Domain.Responses;
using Shared.DTOS.DiaryDto;
using Shared.RequestParameters;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Services.Contracts
{
    public interface IDiaryService
    {
        Task<DiaryDto> CreateDiary(DiaryDtoForCreate diaryForCreationDto);

        Task<DiaryDto> DeleteDiaryForUser(Guid DiaryId, bool trackChanges);

        Task<(IEnumerable<DiaryDto> diaries, MetaData metaData)> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges);

        Task<DiaryDto> UpdateDiary(Guid DiaryId,  UpdateDiaryDto UpdateDiaryDto, bool trackChanges);

        Task<DiaryDto> GetDiaryById(Guid DiaryId,  bool trackChanges);

        
    }
}
