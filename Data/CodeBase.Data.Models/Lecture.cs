namespace CodeBase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Common.Enums;
    using CodeBase.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public Lecture()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        public TimeSpan ReadTime { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
