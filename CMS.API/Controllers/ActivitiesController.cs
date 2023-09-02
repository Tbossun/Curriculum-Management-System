using CMS.Core.Services.Implementation;
using CMS.Core.Services.Interfaces;
using CMS.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using System.Diagnostics;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesService _activitiesService;

        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        [HttpGet("All-Activities")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ActivitiesDto>>>> GetAllActivities(int pageNumber, int pageSize)
        {
            var response = await _activitiesService.GetAllActivitiesAsync(pageNumber, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<bool>>> DeleteActivityAsync(string id)
        {
            var response = await _activitiesService.DeleteActivityAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
