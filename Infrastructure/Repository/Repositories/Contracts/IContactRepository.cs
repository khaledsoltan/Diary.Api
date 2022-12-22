using Api.Host.Domain.Entites;
using Shared.PagedList;
using Shared.RequestParameters.Contact;
using Shared.RequestParameters.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Contracts
{
    public interface IContactRepository
    {
        void CreateContact(Guid DiaryId,Contact contact);
        void DeleteContact(Contact contact);
        Task<Contact> GetContactByContactId(Guid diaryId, Guid contactId, bool trackCahnge);
        Task<PagedList<Contact>> GetAllContactByDiaryId(Guid DiaryId, ContactRequestParameters contactRequestParameters,  bool trackchange);

    }
}
