using AutoMapper;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Models.DTOs;

namespace NUnit.Test.Application.Mapping
{
    public class AuthMapper : Profile
    {
        public AuthMapper() : base("AuthMapper")
        {
            CreateMap<UserSIgnUpDTO, User>().ReverseMap()
              .ForMember(c => c.Password, option => option.Ignore())
              .IgnoreAllPropertiesWithAnInaccessibleSetter()
              .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserSignInDTO, User>().ReverseMap();

        }
    }
}
