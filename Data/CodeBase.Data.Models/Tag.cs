namespace CodeBase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            this.Courses = new HashSet<CourseTag>();
        }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public virtual ICollection<CourseTag> Courses { get; set; }
    }
}
