using CMS.Core.Services.Implementation;
using CMS.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ResponseDTO<IEnumerable<CourseDto>>> GetAllCoursesAsync(int? pageNumber, int? pageSize);
        Task<ResponseDTO<bool>> DeleteACourseAsync(string id);
        Task<ResponseDTO<CourseDto>> GetACourseAsync(string id);
        Task<ResponseDTO<AddCourseDto>> AddCourseAsync(AddCourseDto course);
        Task<ResponseDTO<CourseDto>> UpdateACourseAsync(string id, UpdateCourseDTO course);
        void SetCourseAsCompleted(string courseId);
    }
}
