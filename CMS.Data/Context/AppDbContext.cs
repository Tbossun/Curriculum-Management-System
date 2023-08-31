using CMS.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Invite> Invites { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserQuizTaken> UserQuizTaken { get; set; }
        public DbSet<UserStack> UserStack { get; set; }
        public DbSet<QuizReviewRequest> QuizReviews { get; set; }
        public DbSet<UserLesson> UserLessons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
