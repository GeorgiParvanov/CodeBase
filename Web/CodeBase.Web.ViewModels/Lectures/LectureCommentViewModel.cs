namespace CodeBase.Web.ViewModels.Lectures
{
    using System;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using Ganss.XSS;

    public class LectureCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }
    }
}
