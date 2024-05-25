using AutoMapper;
using D2Soft.Application.DTOs;
using D2Soft.Domain.Entities;

namespace D2Soft.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}