namespace CodeBase.Web.ViewModels.Administration.Lectures
{
    using System;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Administration.Courses;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public Difficulty Difficulty { get; set; }

        public TimeSpan ReadTime { get; set; }

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public CourseViewModel Course { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
