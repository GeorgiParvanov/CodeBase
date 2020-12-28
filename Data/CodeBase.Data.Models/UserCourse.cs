namespace CodeBase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Common.Models;

    public class UserCourse : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
