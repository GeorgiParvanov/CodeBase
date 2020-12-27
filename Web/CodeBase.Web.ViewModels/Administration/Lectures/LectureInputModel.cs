namespace CodeBase.Web.ViewModels.Administration.Lectures
{
    using AutoMapper;
    using CodeBase.Common.Enums;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class LectureInputModel : IMapFrom<Lecture>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public Difficulty Difficulty { get; set; }

        public int ReadTime { get; set; }

        public int CourseId { get; set; }

        public bool IsDeleted { get; set; }

        public string CourseName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Lecture, LectureInputModel>()
                .ForMember(lim => lim.ReadTime, opt =>
                    opt.MapFrom(l => (int)l.ReadTime.TotalMinutes));
        }
    }
}
