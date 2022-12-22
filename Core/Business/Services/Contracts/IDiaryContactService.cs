using Shared.DTOS.DiaryContact;
using Shared.DTOS.DiaryEntry;
using Shared.RequestParameters.DiaryEntry;
using Shared.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.RequestParameters.Contact;

namespace Business.Diary.Services.Contracts
{
    public interface IDiaryContactService
    {
        Task<DiaryContactDto> CreateContact(Guid DiaryId, DiaryContactForCreate DiaryContactForCreate, bool changetrack);
        Task<DiaryContactDto> UpdateContact(Guid DiaryId,  Guid contactId, DiaryContactForUpdate diaryContactForUpdate, bool changetrack);
        Task DeleteContact(Guid DiaryId, Guid contactId, bool changetrack);
        Task<(IEnumerable<DiaryContactDto> contacts, MetaData metaData)> GetAllcontactsByDiaryId(Guid DiaryId, ContactRequestParameters contactsParameters, bool trackchange);
        Task<DiaryContactDto> GetContactByContactId(Guid diaryId, Guid contactId, bool trackCahnge);

    }
}
