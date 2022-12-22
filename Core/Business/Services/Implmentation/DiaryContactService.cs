using Api.Host.Domain.Entites;
using AutoMapper;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.Services.Contracts;
using Domain.Auth;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.UnitOfWork;
using Shared.DTOS.DiaryContact;
using Shared.DTOS.DiaryEntry;
using Shared.LoggerService;
using Shared.RequestParameters;
using Shared.RequestParameters.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Services.Implmentation
{
    internal sealed class DiaryContactService : IDiaryContactService
    {
        private readonly IUnitOfWork _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidateIFexists _GetAndcheckIfExists;
        public DiaryContactService(IUnitOfWork repository, ILoggerManager logger, IMapper mapper, IValidateIFexists validateIFexists)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _GetAndcheckIfExists = validateIFexists;
        }

        public async Task<DiaryContactDto> CreateContact(Guid DiaryId, DiaryContactForCreate DiaryContactForCreate, bool changetrack)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(DiaryId, false);           
            var entityContact = _mapper.Map<Contact>(DiaryContactForCreate);
            _repository.Contact.CreateContact(DiaryId, entityContact);
            await _repository.CompleteAsync();
            return _mapper.Map<DiaryContactDto>(entityContact);
        }

       

        public async Task DeleteContact(Guid DiaryId, Guid contactId, bool changetrack)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(DiaryId, false);
            var contactEntity = await _GetAndcheckIfExists.GetContactAndCheckIfExists(DiaryId, contactId, changetrack);
            _repository.Contact.DeleteContact(contactEntity);
            await _repository.CompleteAsync();
        }

        public async Task<(IEnumerable<DiaryContactDto> contacts, MetaData metaData)> GetAllcontactsByDiaryId(Guid DiaryId, ContactRequestParameters contactsParameters, bool trackchange)
        {
            var contactsWithMetaData = await _repository.Contact.GetAllContactByDiaryId(DiaryId, contactsParameters, trackchange);
            var contactDto = _mapper.Map<IEnumerable<DiaryContactDto>>(contactsWithMetaData);
            return   (contactDto, contactsWithMetaData.MetaData);
        }

        public async Task<DiaryContactDto> GetContactByContactId(Guid diaryId, Guid contactId, bool trackCahnge)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(diaryId, false);
            var contactEntity = await _GetAndcheckIfExists.GetContactAndCheckIfExists(diaryId, contactId, trackCahnge);
            return _mapper.Map<DiaryContactDto>(contactEntity);

        }

        public async Task<DiaryContactDto> UpdateContact(Guid DiaryId, Guid contactId, DiaryContactForUpdate diaryContactForUpdate, bool changetrack)
        {
            await _GetAndcheckIfExists.GetDiaryAndCheckIfExists(DiaryId , false);
            var contactEntity =  await _GetAndcheckIfExists.GetContactAndCheckIfExists(DiaryId, contactId, changetrack);
            _mapper.Map(diaryContactForUpdate, contactEntity);
            await _repository.CompleteAsync();
            return _mapper.Map<DiaryContactDto>(contactEntity);
        }
    }
}
