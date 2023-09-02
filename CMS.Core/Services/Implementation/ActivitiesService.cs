using AutoMapper;
using CMS.Core.Services.Interfaces;
using CMS.Data.Dto;
using CMS.Data.Entities;
using CMS.Data.Repository.Implementation;
using CMS.Data.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services.Implementation
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivitiesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<bool>> DeleteActivityAsync(string id)
        {
            try
            {
                var activity = _unitOfWork.ActivitiesRepo.Get(b => b.Id == id);
                if (activity == null)
                {
                    var notFoundResponse = new ResponseDTO<bool>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = $"Activity with id = {id} not found.",
                        Result = false
                    };

                    return notFoundResponse;
                }

                _unitOfWork.ActivitiesRepo.Remove(activity);
                _unitOfWork.Save();

                var response = new ResponseDTO<bool>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Activity deleted successfully.",
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



        public async Task<ResponseDTO<IEnumerable<ActivitiesDto>>> GetAllActivitiesAsync(int? pageNumber, int? pageSize)
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
                var activities = await _unitOfWork.ActivitiesRepo.GetPageAsync(pageNumber.Value, pageSize.Value);
                var activitiesdto = _mapper.Map<IEnumerable<Activities>, IEnumerable<ActivitiesDto>>(activities);

                var response = new ResponseDTO<IEnumerable<ActivitiesDto>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Activities retrieved successfully.",
                    Result = activitiesdto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDTO<IEnumerable<ActivitiesDto>>
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
