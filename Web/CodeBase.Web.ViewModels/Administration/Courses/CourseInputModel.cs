namespace CodeBase.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CourseInputModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} cannot be more than {1} symbols.")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage ="{0} cannot be more than {1} symbols.")]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
