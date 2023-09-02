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
    public class LessonRepo : RepositoryBase<Lesson>, ILessonRepo
    {
        private AppDbContext _DbContext;

        public LessonRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Lesson lesson)
        {
            _DbContext.Entry(lesson).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}
