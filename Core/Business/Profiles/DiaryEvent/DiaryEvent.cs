using Api.Host.Domain.Entites;
using AutoMapper;
using Shared.DTOS.DiaryDto;
using Shared.DTOS.DiaryEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Profiles.NewFolder
{
    
    public class DiaryEventProfile : Profile
    {
        public DiaryEventProfile()
        {
            CreateMap<DiaryEventDto, DiaryEvent>().ReverseMap();
            CreateMap<DiaryEventForCreate, DiaryEvent>().ReverseMap();
            CreateMap<DiaryEventForUpdate, DiaryEvent>().ReverseMap();
        }
    }
}
