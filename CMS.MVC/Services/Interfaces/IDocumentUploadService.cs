using CMS.MVC.Models;
using Microsoft.AspNetCore.Http;

namespace CMS.MVC.Services.Interfaces
{
    public interface IDocumentUploadService
    {
        Task<DocumentUploadResult> UploadFileAsync(string documentContent);
        Task<DocumentUploadResult> UploadImageAsync(IFormFile image);
    }
}
