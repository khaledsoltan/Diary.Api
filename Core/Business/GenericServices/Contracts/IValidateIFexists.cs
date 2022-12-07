using Api.Host.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.GenericServices.Contracts
{
    public interface IValidateIFexists
    {
        Task<DiarY> GetDiaryAndCheckIfExists(Guid diaryId, bool trackChange);
        Task<DiaryEvent> GetDiaryEventAndCheckIfExists(Guid diaryId, Guid diaryEventId, bool trackChange);

    }
}
