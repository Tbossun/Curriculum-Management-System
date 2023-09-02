using CMS.Core.Services.Interfaces;
using CMS.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("All-Courses")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<CourseDto>>>> GetCoursesAsync(int pageNumber, int pageSize)
        {
            var response = await _courseService.GetAllCoursesAsync(pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Course-By{id}")]
        public async Task<ActionResult<ResponseDTO<CourseDto>>> GetACourseAsync(string id)
        {
            var response = await _courseService.GetACourseAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Set-As-Completed")]
        public IActionResult SetCourseAsCompleted(string courseId)
        {
             _courseService.SetCourseAsCompleted(courseId);
             return Ok();
        }

        [HttpPost("Add-A-Course")]
        public async Task<ActionResult<ResponseDTO<CourseDto>>> CreateBookAsync([FromForm] AddCourseDto addCourseDto)
        {
            var response = await _courseService.AddCourseAsync(addCourseDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update-Course{id}")]
        public async Task<ActionResult<ResponseDTO<CourseDto>>> UpdateBookAsync(string id, [FromForm] UpdateCourseDTO courseDto)
        {
            var response = await _courseService.UpdateACourseAsync(id, courseDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<bool>>> DeleteBookAsync(string id)
        {
            var response = await _courseService.DeleteACourseAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
