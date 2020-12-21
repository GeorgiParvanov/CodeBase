namespace CodeBase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
