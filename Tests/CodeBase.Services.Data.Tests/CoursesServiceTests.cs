namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Moq;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public async Task AddUserToCourseShouldSuccessfullyAddUserToCourse()
        {
            var list = new List<Course>() { new Course { Id = 1, Users = new List<UserCourse>() } };
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            await service.AddUserToCourse(1, "test");

            Assert.Equal(1, list.First().Users.Count);
        }

        [Fact]
        public async Task CreateShouldSuccessfullyAddNewCourseToTheRepository()
        {
            var list = new List<Course>();
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback(
                (Course course) => list.Add(course));
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            await service.Create(new Web.ViewModels.Administration.Courses.CourseInputModel { Id = 1, Description = "test", Name = "test", Price = 13 });

            Assert.Single(list);
        }

        [Fact]
        public void GetBalanceAmountShouldReturnAmmountWhenGivenCourseId()
        {
            var list = new List<Course>() { new Course { Id = 1, Price = 13 } };
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            var actual = service.GetBalanceAmount(1);

            Assert.Equal(13, actual);
        }

        [Fact]
        public void GetCountShouldReturnCorrectAmmountOfEntitiesPresentInTheRepository()
        {
            var list = new List<Course>() { new Course { Id = 1, Price = 13 } };
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            var actual = service.GetCount();

            Assert.Equal(1, actual);
        }

        [Fact]
        public void GetCountWithDeletedShouldReturnCorrectAmmountOfEntitiesPresentInTheRepository()
        {
            var list = new List<Course>() { new Course { Id = 1, Price = 13 } };
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            var actual = service.GetCountWithDeleted();

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task UpdateAsyncShouldSuccessfullyUpdateACourse()
        {
            var list = new List<Course>() { new Course { Id = 1, Description = "test" } };
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            await service.UpdateAsync(1, new Web.ViewModels.Administration.Courses.CourseInputModel { Description = "test2" });

            Assert.NotEqual("test", list.First().Description);
        }

        [Fact]
        public async Task DeleteAsyncShouldSuccessfullyDeleteACourse()
        {
            var course = new Course { Id = 1, Description = "test" };
            var list = new List<Course>();
            list.Add(course);
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            courseRepository.Setup(x => x.Delete(It.IsAny<Course>())).Callback(
                (Course course) => list.Remove(course));
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            await service.DeleteAsync(course.Id);

            Assert.Empty(list);
        }

        [Fact]
        public void CourseExistShouldReturnTrueIfCourseWithIdExistsInRepository()
        {
            var course = new Course { Id = 1, Description = "test" };
            var list = new List<Course>();
            list.Add(course);
            var courseRepository = new Mock<IDeletableEntityRepository<Course>>();
            courseRepository.Setup(x => x.AllWithDeleted()).Returns(list.AsQueryable());
            var userRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var service = new CoursesService(courseRepository.Object, userRepository.Object);

            var actual = service.CourseExist(course.Id);

            Assert.True(actual);
        }
    }
}
