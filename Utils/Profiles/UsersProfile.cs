using AutoMapper;
using Books.Models;
using Books.Models.Dtos;

namespace Books.Utils.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserDto, User>().ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AdminUserDto, User>().ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
