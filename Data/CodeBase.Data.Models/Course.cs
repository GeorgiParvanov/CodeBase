﻿namespace CodeBase.Data.Models
{
    using System.Collections.Generic;

    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Users = new HashSet<UserCourse>();
            this.Lectures = new HashSet<Lecture>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Difficulty Difficulty { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual Cheatsheet Cheatsheet { get; set; }

        public virtual ICollection<UserCourse> Users { get; set; }
    }
}