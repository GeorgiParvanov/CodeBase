namespace CodeBase.Web.ViewModels.Tag
{
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}
