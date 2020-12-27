namespace CodeBase.Web.ViewModels.Lectures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using Ganss.XSS;

    public class LectureViewModel : IMapFrom<Lecture>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public Difficulty Difficulty { get; set; }

        public TimeSpan ReadTime { get; set; }

        public virtual IEnumerable<LectureCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lecture, LectureViewModel>()
                .ForMember(lvm => lvm.VotesCount, options =>
                {
                    options.MapFrom(l => l.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
