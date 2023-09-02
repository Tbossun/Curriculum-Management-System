using CMS.Data.Context;
using CMS.Data.Entities;
using CMS.Data.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository.Implementation
{
    public class StackRepo : RepositoryBase<Stack>, IStackRepo
    {
        private AppDbContext _DbContext;

        public StackRepo(AppDbContext db) : base(db)
        {
            _DbContext = db;
        }

        public void Update(Stack stack)
        {
            _DbContext.Entry(stack).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}
