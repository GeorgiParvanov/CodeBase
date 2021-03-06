﻿namespace CodeBase.Web.ViewModels.Cheatsheet
{
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class CheatsheetViewModel : IMapFrom<Cheatsheet>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CourseId { get; set; }
    }
}
