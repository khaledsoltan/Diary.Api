using Repository.Diary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDiaryRepository Diary { get; }
        IDiaryEventRepository DiaryEvent { get; }
        IDiaryEntryRepository DiaryEntry { get; }

        IContactRepository Contact { get; }
        Task CompleteAsync();

    }
}
