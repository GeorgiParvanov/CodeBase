namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Moq;
    using Xunit;

    public class LecturesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectAmmountOfEntitiesPresentInTheRepository()
        {
            var list = new List<Lecture>() { new Lecture { Id = 1 } };
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new LecturesService(lectureRepository.Object);

            var actual = service.GetCount();

            Assert.Equal(1, actual);
        }

        [Fact]
        public void GetCountWithDeletedShouldReturnCorrectAmmountOfEntitiesPresentInTheRepository()
        {
            var list = new List<Lecture>() { new Lecture { Id = 1 } };
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var service = new LecturesService(lectureRepository.Object);

            var actual = service.GetCountWithDeleted();

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task CreateShouldSuccessfullyAddNewCourseToTheRepository()
        {
            var list = new List<Lecture>();
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AddAsync(It.IsAny<Lecture>())).Callback(
                (Lecture lecture) => list.Add(lecture));
            var service = new LecturesService(lectureRepository.Object);

            await service.CreateAsync(new Web.ViewModels.Administration.Lectures.LectureInputModel { Id = 1, Content = "test", Name = "test"});

            Assert.Single(list);
        }

        [Fact]
        public async Task UpdateAsyncShouldSuccessfullyUpdateACourse()
        {
            var list = new List<Lecture>() { new Lecture { Id = 1, Content = "test" } };
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var service = new LecturesService(lectureRepository.Object);

            await service.UpdateAsync(1, new Web.ViewModels.Administration.Lectures.LectureInputModel { Content = "test2" });

            Assert.NotEqual("test", list.First().Content);
        }

        [Fact]
        public async Task DeleteAsyncShouldSuccessfullyDeleteALecture()
        {
            var lecture = new Lecture { Id = 1, Content = "test" };
            var list = new List<Lecture>();
            list.Add(lecture);
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            lectureRepository.Setup(x => x.Delete(It.IsAny<Lecture>())).Callback(
                (Lecture course) => list.Remove(lecture));
            var service = new LecturesService(lectureRepository.Object);

            await service.DeleteAsync(lecture.Id);

            Assert.Empty(list);
        }

        [Fact]
        public void LectureExistShouldReturnTrueIfCourseWithIdExistsInRepository()
        {
            var lecture = new Lecture { Id = 1, Content = "test" };
            var list = new List<Lecture>();
            list.Add(lecture);
            var lectureRepository = new Mock<IDeletableEntityRepository<Lecture>>();
            lectureRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var service = new LecturesService(lectureRepository.Object);

            var actual = service.LectureExist(lecture.Id);

            Assert.True(actual);
        }
    }
}
