using CMS.Data.Enum;

namespace CMS.Data.Dto
{
    public class QuizSummaryDto
    {
        public List<LessonQuizesSummaryDto>  QuizesSummary { get; set; }

        public double Percentage { get; set; }

        public int CorrectCount { get; set; }

        public int IncorrectCount { get; set; }
        public int SkippedCount { get; set; }

        public List<ModuleWeeks> Weeks { get; set; }


    }
}
