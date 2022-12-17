using AutoMapper;
using OIDC.Entities;
using OIDC.Entities.ViewModels;

namespace OIDC;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserRegistrationModel, User>()
			.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
	}
}
