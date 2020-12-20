namespace CodeBase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int lectureId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                LectureId = lectureId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInLectureId(int commentId, int lectureId)
        {
            var commentLectureId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.LectureId).FirstOrDefault();

            return commentLectureId == lectureId;
        }
    }
}
