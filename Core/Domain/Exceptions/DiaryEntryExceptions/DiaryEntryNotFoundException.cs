using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Diary.Exceptions.DiaryEntryExceptions
{
 
    public sealed class DiaryEntryNotFoundException : NotFoundException
    {
        public DiaryEntryNotFoundException(Guid diaryEntryId)
           : base($"The diaryEntry with id: {diaryEntryId} doesn't exist in the database.")
        {
        }
    }
}
