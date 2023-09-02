using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Data.Repository.RepositoryInterface
{
    public interface IUnitOfWork
    {
        IActivitiesRepo ActivitiesRepo { get; }
        IStackRepo StackRepo { get; }
        ILessonRepo LessonsRepo { get; }
        ICourseRepo CoursesRepo { get; }
        IQuizRepo QuizRepo { get; }
        
        void Save();
    }
}
