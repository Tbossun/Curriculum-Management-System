using AutoMapper;
using CMS.Core.Services.Interfaces;
using CMS.Data.Dto;
using CMS.Data.Entities;
using CMS.Data.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<AddCourseDto>> AddCourseAsync(AddCourseDto course)
        {
            try
            {
                var newCourse = _mapper.Map<Course>(course);
                _unitOfWork.CoursesRepo.Add(newCourse);
                _unitOfWork.Save();

                var createdCourseDto = _mapper.Map<AddCourseDto>(newCourse);

                var response = new ResponseDTO<AddCourseDto>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    DisplayMessage = "New Course Added successfully.",
                    Result = createdCourseDto
                };

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<AddCourseDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return await Task.FromResult(response);
            }
        }

        public async Task<ResponseDTO<bool>> DeleteACourseAsync(string id)
        {
            try
            {
                var existingCourse = _unitOfWork.CoursesRepo.Get(b => b.Id == id);
                if (existingCourse == null)
                {
                    var notFoundResponse = new ResponseDTO<bool>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Course not found.",
                        Result = false
                    };

                    return notFoundResponse;
                }

                _unitOfWork.CoursesRepo.Remove(existingCourse);
                _unitOfWork.Save();

                var response = new ResponseDTO<bool>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Course deleted successfully.",
                    Result = true
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<bool>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = false
                };

                return response;
            }
        }

        public async Task<ResponseDTO<CourseDto>> GetACourseAsync(string id)
        {
            try
            {
                var course = _unitOfWork.CoursesRepo.Get(b => b.Id == id);
                if (course == null)
                {
                    var notFoundResponse = new ResponseDTO<CourseDto>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = $"Course with id = {id} not found.",
                        Result = null
                    };

                    return notFoundResponse;
                }

                var courseDto = _mapper.Map<CourseDto>(course);

                var response = new ResponseDTO<CourseDto>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Course retrieved successfully.",
                    Result = courseDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<CourseDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }

        public async Task<ResponseDTO<IEnumerable<CourseDto>>> GetAllCoursesAsync(int? pageNumber, int? pageSize)
        {
            int defaultPageSize = 10;
            int defaultPageNumber = 1;

            if (!pageNumber.HasValue || pageNumber <= 0)
            {
                pageNumber = defaultPageNumber;
            }

            if (!pageSize.HasValue || pageSize <= 0)
            {
                pageSize = defaultPageSize;
            }

            try
            {
                var courses = await _unitOfWork.CoursesRepo.GetPageAsync(pageNumber.Value, pageSize.Value);
                var coursesDto = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses);

                var response = new ResponseDTO<IEnumerable<CourseDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "All Courses retrieved successfully.",
                    Result = coursesDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<IEnumerable<CourseDto>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }

        public void SetCourseAsCompleted(string courseId)
        {
            try
            {
                _unitOfWork.CoursesRepo.SetCourseAsCompleted(courseId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Course with ID {courseId} does not exist.");
            }
        }

        public async Task<ResponseDTO<CourseDto>> UpdateACourseAsync(string id, UpdateCourseDTO course)
        {
            try
            {
                var existingcourse = _unitOfWork.CoursesRepo.Get(b => b.Id == id);
                if (existingcourse == null)
                {
                    var notFoundResponse = new ResponseDTO<CourseDto>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Course not found.",
                        Result = null
                    };

                    return notFoundResponse;
                }

                _mapper.Map(course, existingcourse);
                _unitOfWork.CoursesRepo.Update(existingcourse);
                _unitOfWork.Save();

                var updatedcourseDto = _mapper.Map<CourseDto>(existingcourse);

                var response = new ResponseDTO<CourseDto>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Course updated successfully.",
                    Result = updatedcourseDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<CourseDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }

    }
}
