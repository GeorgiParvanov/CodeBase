namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CodeBase.Data.Common.Enums;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Moq;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public void GetVotesShouldReturnTheSumOfPositiveVotes()
        {
            var list = new List<Vote>()
            {
                new Vote { Id = 1, Type = VoteType.UpVote, LectureId = 1 },
                new Vote { Id = 2, Type = VoteType.UpVote, LectureId = 1 },
                new Vote { Id = 3, Type = VoteType.DownVote, LectureId = 1 },
            };
            var votesRepository = new Mock<IRepository<Vote>>();
            votesRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new VotesService(votesRepository.Object);

            var actual = service.GetVotes(1);

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task VoteAsyncShouldUpdateOrCreateNewVote()
        {
            var list = new List<Vote>()
            {
                new Vote { Id = 1, Type = VoteType.UpVote, LectureId = 1, UserId = "Name1" },
                new Vote { Id = 2, Type = VoteType.UpVote, LectureId = 1, UserId = "Name2" },
                new Vote { Id = 3, Type = VoteType.DownVote, LectureId = 1, UserId = "Name" },
            };
            var votesRepository = new Mock<IRepository<Vote>>();
            votesRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            votesRepository.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback(
                (Vote vote) => list.Add(vote));
            var service = new VotesService(votesRepository.Object);

            await service.VoteAsync(1, "Name", true);
            await service.VoteAsync(1, "Name3", false);
            var actual = service.GetVotes(1);

            Assert.Equal(4, list.Count);
            Assert.Equal(2, actual);
        }
    }
}
