using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Dto
{
    public class EmailDto
    {
        [EmailAddress]
        public List<string>  To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
