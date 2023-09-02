using AutoMapper;
using CMS.Core.Services.Implementation;
using CMS.Core.Services.Interfaces;
using CMS.Data.Dto;
using CMS.Data.Entities;

namespace CMS.API.MappingProfiles
{
    public class Mappingprofiles  : Profile
    {
        public Mappingprofiles() 
        {
            CreateMap<Activities, ActivitiesDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, AddCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();
        }     
    }
}
