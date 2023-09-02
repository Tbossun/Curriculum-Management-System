using CMS.Data.Dto;
using CMS.Data.Enum;
using CMS.MVC.Services.Implementation;

namespace CMS.MVC.Services.Interfaces
{
    public interface IUserService
    {

        public Task<ResponseDTO<bool>> DeleteUser(string userId);
        public Task<ResponseDTO<bool>> SetActiveStatus(string userId, bool status);

        public Task<bool> RequestPermission(string userId);
        public Task<bool> GrantPermission(string userId, Permissions claims);

        Task<ResponseDTO<bool>> DeleteFileAsync(string publicId, string email);
        Task<ResponseDTO<Dictionary<string, string>>> UploadFileAsync(IFormFile file, string email);
        Task<ResponseDTO<string>> GetUserRoles(string userId);
        Task<ResponseDTO<IEnumerable<GetAllUsersDto>>> GetAllUsers();
        Task<ResponseDTO<GetuserByIdDto>> GetByIDAsync(string Id);
    }
}

