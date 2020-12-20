namespace CodeBase.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int lectureId, string userId, string content, int? parentId = null);

        bool IsInLectureId(int commentId, int lectureId);
    }
}
