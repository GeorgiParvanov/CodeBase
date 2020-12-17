namespace CodeBase.Web.ViewModels.Lectures
{
    using System;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        // public Difficulty Difficulty { get; set; }
        public TimeSpan ReadTime { get; set; }

        // public virtual ICollection<Comment> Comments { get; set; }
    }
}
