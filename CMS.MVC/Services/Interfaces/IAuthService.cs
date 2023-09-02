using CMS.Data.Dto;

namespace CMS.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDTO<ResetPassword>> ResetPasswords(ResetPassword resetPassword);
        Task<ResponseDTO<ConfirmEmailDto>> ConfirmEmail(string userId, string token);
        Task<ResponseDTO<string>> Logout();
        Task<ResponseDTO<string>> ForgotPassword(string email);
        Task<ResponseDTO<string>> ExternalLogin(string email, string firstName, string surname);
        Task<ResponseDTO<LoginResponseDto>> Login(LoginDto login);
    }
}