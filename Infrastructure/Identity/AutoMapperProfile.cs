
using AutoMapper;
using Data.Models;

namespace Infrastructure.Identity
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, UserUpdateRequest>().ReverseMap();
            CreateMap<ApplicationUser, RegisterModel>().ReverseMap(); 
            CreateMap<ApplicationUserProcModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserResponse>().ReverseMap();
            CreateMap<List<ApplicationUser>, List<ApplicationUserResponse>>().ReverseMap();
        }
    }
}
