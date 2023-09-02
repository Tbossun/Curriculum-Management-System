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
    public class QuizRepo : RepositoryBase<Quiz>, IQuizRepo
    {
        private AppDbContext _DbContext;

        public QuizRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Quiz quiz)
        {
            _DbContext.Entry(quiz).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}
