using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Dto
{
    public class QuizReviewRequestDTO
    {
        [Required]

        public string CourseId { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }

        [Required]
        public bool IsSatisfied { get; set; }

    }
}
