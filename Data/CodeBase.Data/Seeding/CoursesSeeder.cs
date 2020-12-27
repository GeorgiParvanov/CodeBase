namespace CodeBase.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;

    internal class CoursesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Courses.Any())
            {
                return;
            }

            var seeShrarpTag = new Tag { Name = "C#" };

            await dbContext.Courses.AddRangeAsync(
                new Course
                {
                    Name = "C# beginners course",
                    Description = "This is a c# beginners course",
                    Price = 13.00m,
                    Difficulty = Difficulty.Beginner,
                    Tags = new List<CourseTag>()
                    {
                        new CourseTag
                        {
                            Tag = seeShrarpTag,
                        },
                    },
                    Cheatsheet = new Cheatsheet
                    {
                        Content = "This is the cheatsheet of the c# beginners course",
                    },
                    Lectures = new Lecture[]
                    {
                        new Lecture { Name = "Lecture00", Content = "Get Lectured Boi", Difficulty = Difficulty.Beginner, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture01", Content = "Get Lectured Boi", Difficulty = Difficulty.Beginner, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture02", Content = "Get Lectured Boi", Difficulty = Difficulty.Beginner, ReadTime = new TimeSpan(0, 2, 0) },
                    },
                },
                new Course
                {
                    Name = "C# intermidiate course",
                    Description = "This is a c# intermidiate course",
                    Price = 13.00m,
                    Difficulty = Difficulty.Intermediate,
                    Tags = new List<CourseTag>()
                    {
                        new CourseTag
                        {
                            Tag = seeShrarpTag,
                        },
                    },
                    Cheatsheet = new Cheatsheet
                    {
                        Content = "This is the cheatsheet of the c# intermidiate course",
                    },
                    Lectures = new Lecture[]
                    {
                        new Lecture { Name = "Lecture03", Content = "Get Lectured Boi", Difficulty = Difficulty.Intermediate, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture04", Content = "Get Lectured Boi", Difficulty = Difficulty.Intermediate, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture05", Content = "Get Lectured Boi", Difficulty = Difficulty.Intermediate, ReadTime = new TimeSpan(0, 2, 0) },
                    },
                },
                new Course
                {
                    Name = "C# advanced course",
                    Description = "This is a c# advanced course",
                    Price = 13.00m,
                    Difficulty = Difficulty.Advanced,
                    Tags = new List<CourseTag>()
                    {
                        new CourseTag
                        {
                            Tag = seeShrarpTag,
                        },
                    },
                    Cheatsheet = new Cheatsheet
                    {
                        Content = "This is the cheatsheet of the c# advanced course",
                    },
                    Lectures = new Lecture[]
                    {
                        new Lecture { Name = "Lecture06", Content = "Get Lectured Boi", Difficulty = Difficulty.Advanced, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture07", Content = "Get Lectured Boi", Difficulty = Difficulty.Advanced, ReadTime = new TimeSpan(0, 2, 0) },
                        new Lecture { Name = "Lecture08", Content = "Get Lectured Boi", Difficulty = Difficulty.Advanced, ReadTime = new TimeSpan(0, 2, 0) },
                    },
                });
        }
    }
}
