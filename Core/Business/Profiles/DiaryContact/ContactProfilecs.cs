using Api.Host.Domain.Entites;
using AutoMapper;
using Shared.DTOS.DiaryContact;
using Shared.DTOS.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Profiles.DiaryContact
{
    public class ContactProfilecs : Profile
    {
        public ContactProfilecs()
        {
            CreateMap<DiaryContactForCreate, Contact>().ReverseMap();
            CreateMap<DiaryContactForUpdate, Contact>().ReverseMap();
            CreateMap<DiaryContactDto, Contact>().ReverseMap();

        }
    }
}
