using CMS.Data.Entities;

namespace CMS.MVC.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
