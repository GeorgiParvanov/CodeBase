namespace CodeBase.Data.Models
{
    using CodeBase.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
