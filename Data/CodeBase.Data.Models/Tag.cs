namespace CodeBase.Data.Models
{
    using System.Collections.Generic;

    using CodeBase.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            this.Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        // TODO: add image?
    }
}
