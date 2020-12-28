namespace CodeBase.Data.Models
{
    using CodeBase.Data.Common.Models;

    public class CourseTag : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
