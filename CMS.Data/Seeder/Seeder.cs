using CMS.Data.Context;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CMS.Data.Seeder
{
    public class Seeder
    {
        public static async void SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            var baseDir = Directory.GetCurrentDirectory();           

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Users.Any())
            {
                await context.Database.EnsureCreatedAsync();
                //Creating list of roles

                List<string> roles = new() { "Facilitator", "Admin", "Student" };

                //Creating roles
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var users = new List<ApplicationUser> {
                //Instantiating i User and it properties
                new ApplicationUser
                {
                    Id = "36e318aa-6d02-46d9-8048-3e2a8182a6c3",
                    FirstName = "Sodiq",
                    LastName = "Facilitator",
                    UserName = "Tboss",
                    Email = "boyalabi@gmail.com",
                    PhoneNumber = "08103685498",
                    SquadNumber = "001",
                    PhoneNumberConfirmed = true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    EmailConfirmed = true
                },

                 new ApplicationUser
                {
                    Id = "4ce8e96f-b3f9-4510-8e8c-02d099e1f3bd",
                    FirstName = "Ola",
                    LastName = "Tboss",
                    UserName = "Ola",
                    Email = "alabisdq@gmail.com",
                    PhoneNumber = "08076570048",
                    SquadNumber = "002",
                    PhoneNumberConfirmed = true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    EmailConfirmed = true
                },

                 new ApplicationUser
                 {
                    Id = "311c0cf1-7c88-e221-c4ff-38ce6418a005",
                    FirstName = "Alabi",
                    LastName = "boy",
                    UserName = "boyalabi",
                    Email = "valexsaint@yahoo.com",
                    PhoneNumber = "09033941851",
                    SquadNumber = "003",
                    PhoneNumberConfirmed = true,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    EmailConfirmed = true
                 }

                };

                for (int i = 0; i < users.Count; i++)
                {
                    await userManager.CreateAsync(users[i], "Password@123");
                    await userManager.AddToRoleAsync(users[i], roles[i]);
                }
            }

            if (!context.Courses.Any())
            {
                var coursePath = File.ReadAllText(FilePath(baseDir, "JsonFiles/Course.json"));
                var cmsCourse = JsonConvert.DeserializeObject<List<Course>>(coursePath);
                await context.Courses.AddRangeAsync(cmsCourse);

                var modules = context.Lessons.Where(x => x.CourseId == "").Select(x => x.Module).ToList();
            }

            if (!context.Lessons.Any())
            {
                var lessonPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/Lesson.json"));
                var cmsLesson = JsonConvert.DeserializeObject<List<Lesson>>(lessonPath);
                await context.Lessons.AddRangeAsync(cmsLesson);
            }


            if (!context.Quizs.Any())
            {
                var quizPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/Quiz.json"));
                var cmsQuiz = JsonConvert.DeserializeObject<List<Quiz>>(quizPath);
                await context.Quizs.AddRangeAsync(cmsQuiz);
            }


            //if (!dbContext.QuizOptions.Any())
            //{
            //    var quizOptionPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/QuizOption.json"));
            //    var cmsQuizOption = JsonConvert.DeserializeObject<List<QuizOption>>(quizOptionPath);
            //    await dbContext.QuizOptions.AddRangeAsync(cmsQuizOption);
            //}

            if (!context.Stacks.Any())
            {
                var stackPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/Stack.json"));
                var cmsStack = JsonConvert.DeserializeObject<List<Stack>>(stackPath);
                await context.Stacks.AddRangeAsync(cmsStack);
            }

            if (!context.QuizReviews.Any())
            {
                var stackPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/QuizReviews.json"));
                var cmsStack = JsonConvert.DeserializeObject<List<QuizReviewRequest>>(stackPath);
                await context.QuizReviews.AddRangeAsync(cmsStack);
            }

            //if (!dbContext.UserCourses.Any())
            //{
            //    var userCoursePath = File.ReadAllText(FilePath(baseDir, "JsonFiles/UserCourse.json"));
            //    var cmsUserCourse = JsonConvert.DeserializeObject<List<UserCourse>>(userCoursePath);
            //    await dbContext.UserCourses.AddRangeAsync(cmsUserCourse);
            //}

            if (!context.UserQuizTaken.Any())
            {
                var userQuizTakenPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/QuizTaken.json"));
                var cmsUserQuizTaken = JsonConvert.DeserializeObject<List<UserQuizTaken>>(userQuizTakenPath);
                await context.UserQuizTaken.AddRangeAsync(cmsUserQuizTaken);
            }

            if(!context.UserStack.Any())
            {
                var userStackPath = File.ReadAllText(FilePath(baseDir, "JsonFiles/UserStack.json"));
                var cmsUserStack = JsonConvert.DeserializeObject<List<UserStack>>(userStackPath);
                await context.UserStack.AddRangeAsync(cmsUserStack);
            }
            //Saving everything into the database
            await context.SaveChangesAsync();
        }

        //Defining method to get file paths
        private static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}