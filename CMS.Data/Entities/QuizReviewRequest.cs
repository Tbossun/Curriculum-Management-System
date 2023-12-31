﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Entities
{
    public class QuizReviewRequest : BaseEntity
    {

        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }

        public bool IsSatisfied { get; set; }

    }
}
