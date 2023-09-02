using CMS.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Dto
{
    public class AddLessonDTO
    {
        public string CourseId { get; set; }
        public string AddedById { get; set; }
        public Modules Module { get; set; }
        public ModuleWeeks Weeks { get; set; }

        [MaxLength(150)]
        public string Topic { get; set; }

        public string Text { get; set; }
        public string VideoUrl { get; set; }
        public string PublicId { get; set; }
        public bool CompletionStatus { get; set; }
        public bool IsDeleted { get; set; }

    }
}