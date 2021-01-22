namespace CodeBase.Web.ViewModels.Administration.Cheatsheets
{
    using System;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CheatsheetViewModel : IMapFrom<Cheatsheet>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
