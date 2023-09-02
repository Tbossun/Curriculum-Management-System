using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository.RepositoryInterface
{
    public interface ILessonRepo : IRepositoryBase<Lesson>
    {
        void Update(Lesson lesson);
    }
}
