using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Diary.Exceptions.ContactExceptions
{
    public sealed class ContactyNotFoundException : NotFoundException
    {
        public ContactyNotFoundException(Guid contactId)
           : base($"The Contact with id: {contactId} doesn't exist in the database.")
        {
        }
    }
}
