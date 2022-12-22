using Api.Host.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Diary.Repositories.Contracts;
using Repository.GenericRepository;
using Shared.PagedList;
using Shared.RequestParameters.Contact;
using Shared.RequestParameters.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Diary.Repositories.Implmentation
{
    internal sealed class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public void CreateContact(Guid diaryId, Contact contact)
        {
            contact.DiaryId = diaryId;
            Create(contact);
        }

        public async Task<Contact> GetContactByContactId(Guid diaryId,Guid contactId, bool trackCahnge) 
            => await FindByCondition(x => x.DiaryId.Equals(diaryId) && x.Id.Equals(contactId), trackCahnge).SingleOrDefaultAsync();
       
        public void DeleteContact(Contact contact) => Delete(contact);
        public async Task<PagedList<Contact>> GetAllContactByDiaryId(Guid DiaryId, ContactRequestParameters contactRequestParameters, bool trackchange)
        {

            var diaryEntries = await FindByCondition(x => x.DiaryId == DiaryId, trackchange).ToListAsync();

            return PagedList<Contact>.ToPagedList(diaryEntries, contactRequestParameters.PageNumber, contactRequestParameters.PageSize);

        }


    }
}
