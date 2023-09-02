using CMS.Data.Context;
using CMS.Data.Entities;
using CMS.Data.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository.Implementation
{
    public class ActivitiesRepo : RepositoryBase<Activities>, IActivitiesRepo
    {
        private AppDbContext _DbContext;

        public ActivitiesRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Activities activity)
        {
            _DbContext.Entry(activity).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }

    }
}
