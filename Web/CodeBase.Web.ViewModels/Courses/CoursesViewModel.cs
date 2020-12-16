namespace CodeBase.Web.ViewModels.Courses
{
    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    // using System.Collections.Generic;
    public class CoursesViewModel : IMapFrom<Course>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsBought { get; set; }

        // TODO: Difficulty comes from CodeBase.Data.Common.Enums - should I move those enums to CodeBase.Common ???
        public Difficulty Difficulty { get; set; }

        // TODO: This has to be am IEnumerable collection of TagViewModels after I change the bd ???
        public virtual Tag Tag { get; set; }

        public virtual Cheatsheet Cheatsheet { get; set; }

        // public virtual IEnumerable<LectureViewModel> Lectures { get; set; }
    }
}
