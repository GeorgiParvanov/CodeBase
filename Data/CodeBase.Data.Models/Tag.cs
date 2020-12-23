namespace CodeBase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CodeBase.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            this.Courses = new HashSet<CourseTag>();
        }

        public string Name { get; set; }

        public virtual ICollection<CourseTag> Courses { get; set; }

        // TODO: add image?
    }
}
