namespace CodeBase.Data.Models
{
    using CodeBase.Data.Common.Models;

    public class UserCourse : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
