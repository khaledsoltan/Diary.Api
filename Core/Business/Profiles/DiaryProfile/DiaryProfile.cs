using Api.Host.Domain.Entites;
using AutoMapper;
using Domain.Auth;
using Shared.DTOS.Auth;
using Shared.DTOS.DiaryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Diary.Profiles.DiaryProfile
{
   
    public class DiaryProfile : Profile
    {
        public DiaryProfile()
        {
            CreateMap<DiaryDto, DiarY>().ReverseMap();

        }

    }
}
