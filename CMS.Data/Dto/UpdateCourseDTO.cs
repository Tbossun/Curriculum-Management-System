using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Dto
{
    public class UpdateCourseDTO
    {
        [MaxLength(150)]
        public string? Name { get; set; }
       
    }
}
