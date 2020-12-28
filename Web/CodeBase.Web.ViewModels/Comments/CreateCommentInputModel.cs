namespace CodeBase.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        public int LectureId { get; set; }

        public int ParentId { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The comment cannot be more that {1} symbols.")]
        public string Content { get; set; }
    }
}
