using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Diary.Exceptions.Folder
{
    public sealed class DiaryNotFoundException : NotFoundException
    {
        public DiaryNotFoundException(Guid Diary)
           : base($"The Diary with id: {Diary} doesn't exist in the database.")
        {
        }
    }

   

}
