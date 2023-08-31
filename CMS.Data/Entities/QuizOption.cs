using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Entities
{
    public class QuizOption : BaseEntity
    {
        public string QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [MaxLength(150)]
        public string Option { get; set; }
    }
}