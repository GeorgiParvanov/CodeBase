namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Moq;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateShouldSuccessfullyAddNewCommentToTheRepository()
        {
            var list = new List<Comment>();
            var repository = new Mock<IDeletableEntityRepository<Comment>>();
            repository.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback(
                (Comment comment) => list.Add(comment));
            var service = new CommentsService(repository.Object);

            await service.Create(1, "test", "content", null);

            Assert.Single(list);
        }

        [Fact]
        public void IsInLectureIdShouldReturnTrueWhenGivenValidData()
        {
            var list = new List<Comment>() { new Comment { Id = 1, LectureId = 1 } };
            var repository = new Mock<IDeletableEntityRepository<Comment>>();
            repository.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new CommentsService(repository.Object);

            var actual = service.IsInLectureId(1, 1);

            Assert.True(actual);
        }

        [Fact]
        public void IsInLectureIdShouldReturnFalseWhenDBIsEmpty()
        {
            var list = new List<Comment>();
            var repository = new Mock<IDeletableEntityRepository<Comment>>();
            repository.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new CommentsService(repository.Object);

            var actual = service.IsInLectureId(1, 1);

            Assert.False(actual);
        }
    }
}
