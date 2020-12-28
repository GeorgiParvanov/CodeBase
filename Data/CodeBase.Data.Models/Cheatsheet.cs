namespace CodeBase.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Common.Models;

    public class Cheatsheet : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
