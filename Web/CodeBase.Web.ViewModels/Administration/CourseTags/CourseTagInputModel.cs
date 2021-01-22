namespace CodeBase.Web.ViewModels.Administration.CourseTags
{
    using System;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CourseTagInputModel : IMapFrom<CourseTag>
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
