namespace CodeBase.Web.ViewModels.Administration.Courses
{
    using System;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CourseInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool IsDeleted { get; set; }
    }
}
