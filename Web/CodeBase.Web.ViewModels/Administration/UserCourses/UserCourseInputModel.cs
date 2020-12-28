namespace CodeBase.Web.ViewModels.Administration.UserCourses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class UserCourseInputModel : IMapFrom<UserCourse>
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
