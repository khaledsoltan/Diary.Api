using AutoMapper;
using Domain.Auth;
using Shared.DTOS.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<UserForRegistrationDto, User>();

        }

    }
}
