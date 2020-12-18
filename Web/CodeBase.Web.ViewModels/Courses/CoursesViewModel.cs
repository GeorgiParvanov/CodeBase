namespace CodeBase.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using AutoMapper;
    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Cheatsheet;
    using CodeBase.Web.ViewModels.Lectures;
    using CodeBase.Web.ViewModels.Tag;

    public class CoursesViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsBought { get; set; }

        // TODO: Difficulty comes from CodeBase.Data.Common.Enums - should I move those enums to CodeBase.Common ???
        public Difficulty Difficulty { get; set; }

        // TODO: This has to be am IEnumerable collection of TagViewModels after I change the bd ???
        public virtual IEnumerable<TagViewModel> Tags { get; set; }

        public virtual CheatsheetViewModel Cheatsheet { get; set; }

        public virtual IEnumerable<LectureViewModel> Lectures { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CourseTag, TagViewModel>()
                .ForMember(t => t.Name, opt => opt.MapFrom(ct => ct.Tag.Name));

                // .ReverseMap(); TODO:
        }
    }
}
