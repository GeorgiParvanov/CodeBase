namespace CodeBase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
