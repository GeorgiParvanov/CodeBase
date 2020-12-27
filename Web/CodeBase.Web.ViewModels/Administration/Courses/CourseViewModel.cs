namespace CodeBase.Web.ViewModels.Administration.Courses
{
    using System;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
