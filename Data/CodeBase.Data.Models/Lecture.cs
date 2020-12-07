namespace CodeBase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public Lecture()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public Difficulty Difficulty { get; set; }

        public TimeSpan ReadTime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        // TODO: rating?
    }
}
