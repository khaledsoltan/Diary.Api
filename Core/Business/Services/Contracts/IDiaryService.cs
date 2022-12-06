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
        DiaryDto CreateDiary(DiaryDto diaryForCreationDto);

        void DeleteDiaryForUser(Guid DiaryId, string Userid, bool trackChanges);


        Task<(IEnumerable<DiaryDto> diaries, MetaData metaData)> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges);
        Task<DiaryDto> UpdateDiary(Guid DiaryId, string Userid, UpdateDiaryDto UpdateDiaryDto, bool trackChanges);
    }
}
