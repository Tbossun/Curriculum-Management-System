using CMS.Core.Services.Implementation;
using CMS.Data.Dto;
using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Interfaces
{
    public interface IActivitiesService
    {
        Task<ResponseDTO<IEnumerable<ActivitiesDto>>> GetAllActivitiesAsync(int? pageNumber, int? pageSize);
        Task<ResponseDTO<bool>> DeleteActivityAsync(string id);
    }
}
