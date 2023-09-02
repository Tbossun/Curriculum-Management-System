using CMS.Data.Context;
using CMS.Data.Entities;
using CMS.Data.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository.Implementation
{
    public class CourseRepo : RepositoryBase<Course>, ICourseRepo
    {
        private AppDbContext _DbContext;

        public CourseRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void SetCourseAsCompleted(string courseId)
        {
           var course = _DbContext.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                course.IsCompleted = true;
                _DbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Course with ID {courseId} does not exist.");
            }
        }

        public void Update(Course course)
        {
            _DbContext.Entry(course).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}
