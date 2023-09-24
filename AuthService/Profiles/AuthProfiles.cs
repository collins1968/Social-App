using AuthService.Models.RequestsDto;
using AuthService.Models.ResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegistrationDto, IdentityUser>();
            CreateMap<IdentityUser, UserDto>().ReverseMap();

        }
    }
}
