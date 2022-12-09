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
        void CreateContact(Contact contact);
        Task<Contact> GetContactByContactId(Guid contactId, bool trackCahnge);
        void DeleteContact(Contact contact);
        Task<Contact> GetContactByAndContactId(Guid contactId, bool trackCahnge);
        Task<PagedList<Contact>> GetDaysInMonthWithEntries(Guid DiaryId, ContactRequestParameters contactRequestParameters, int Month, int Year, bool trackchange);
    }
}
