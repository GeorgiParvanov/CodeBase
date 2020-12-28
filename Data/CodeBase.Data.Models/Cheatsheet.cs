namespace CodeBase.Data.Models
{
    using CodeBase.Data.Common.Models;

    public class Cheatsheet : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
