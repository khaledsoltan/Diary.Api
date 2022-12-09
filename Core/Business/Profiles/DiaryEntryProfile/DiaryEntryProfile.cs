using Api.Host.Domain.Entites;
using AutoMapper;
using Shared.DTOS.DiaryDto;
using Shared.DTOS.DiaryEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Profiles.DiaryEntryProfile
{
    public class DiaryEntryProfile : Profile
    {
        public DiaryEntryProfile()
        {
            CreateMap<DiaryEntryDtoForCreate, DiaryEntry>().ReverseMap();
            CreateMap<DiaryEntryDtoForUpdate, DiaryEntry>().ReverseMap();
            CreateMap<GetDiaryEntryDto, DiaryEntry>().ReverseMap();
        }
    }
}
