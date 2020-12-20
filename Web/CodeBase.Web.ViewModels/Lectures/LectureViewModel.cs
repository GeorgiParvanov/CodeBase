namespace CodeBase.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using Ganss.XSS;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        // TODO: move Difficulty enum to CodeBase.Common and uncomment Difficulty prop here
        // public Difficulty Difficulty { get; set; }
        public TimeSpan ReadTime { get; set; }

        public virtual IEnumerable<LectureCommentViewModel> Comments { get; set; }
    }
}
