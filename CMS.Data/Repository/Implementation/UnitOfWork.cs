using CMS.Data.Context;
using CMS.Data.Repository.RepositoryInterface;

namespace CMS.Data.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private AppDbContext _appDbContext;

        public IActivitiesRepo ActivitiesRepo { get; private set; }

        public IStackRepo StackRepo { get; private set; }

        public ILessonRepo LessonsRepo { get; private set; }

        public ICourseRepo CoursesRepo { get; private set; }

        public IQuizRepo QuizRepo { get; private set; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            ActivitiesRepo = new ActivitiesRepo(_appDbContext);
            StackRepo = new StackRepo(_appDbContext);
            LessonsRepo = new LessonRepo(_appDbContext);
            CoursesRepo = new CourseRepo(_appDbContext);
            QuizRepo = new QuizRepo(_appDbContext);

        }    

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
