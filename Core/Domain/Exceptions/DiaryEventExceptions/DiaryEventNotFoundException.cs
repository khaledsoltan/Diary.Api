using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Diary.Exceptions.DiaryEventExceptions
{
   
    public sealed class DiaryEventNotFoundException : NotFoundException
    {
        public DiaryEventNotFoundException(Guid diaryEvent)
           : base($"The DiaryEvent with id: {diaryEvent} doesn't exist in the database.")
        {
        }
    }

}
