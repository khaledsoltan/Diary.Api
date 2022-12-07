using Api.Host.Domain.Entites;
using Shared.PagedList;
using Shared.RequestParameters.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Contracts
{
    public interface IDiaryRepository
    {
         Task<PagedList<DiarY>> GetAllDiariesByUserId(DiaryRequestParameter diaryRequestParameter, bool trackChanges);

         Task<DiarY> GetDiaryByIdAndUserId(Guid DiaryId,  bool trackChanges);

         void CreateDiary(DiarY diary);

         void DeleteDiary(DiarY diary);


    }
}
