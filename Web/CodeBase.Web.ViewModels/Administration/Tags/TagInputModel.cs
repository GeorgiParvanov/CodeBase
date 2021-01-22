namespace CodeBase.Web.ViewModels.Administration.Tags
{
    using System;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class TagInputModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
