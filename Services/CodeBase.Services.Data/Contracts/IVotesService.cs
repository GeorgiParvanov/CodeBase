namespace CodeBase.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        /// <summary>
        /// </summary>
        /// <param name="lectureId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpVote">If true - up vote, else - down vote.</param>
        /// <returns></returns>
        Task VoteAsync(int lectureId, string userId, bool isUpVote);

        int GetVotes(int lectureId);
    }
}
