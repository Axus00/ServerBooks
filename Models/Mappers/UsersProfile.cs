using AutoMapper;
using Books.Models;
using Books.Models.DTOs;

namespace Books.Utils.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserDTO, User>().ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AdminUserDTO, User>().ForAllMembers(options => options.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
