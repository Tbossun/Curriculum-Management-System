using AutoMapper;
using CMS.Data.Dto;
using CMS.Data.Entities;

namespace CMS.MVC.MvcMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, GetuserByIdDto>();
            CreateMap<ApplicationUser, GetAllUsersDto>();
            CreateMap<ApplicationUser, LoginResponseDto>();

        }
    }
}
