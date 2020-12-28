namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using Moq;
    using Xunit;

    public class CheatsheetServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectCountOfCheatsheets()
        {
            var list = new List<Cheatsheet>() { new Cheatsheet { Id = 1 } };
            var repository = new Mock<IDeletableEntityRepository<Cheatsheet>>();
            repository.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            var service = new CheatsheetService(repository.Object);

            var actual = service.GetCount();

            Assert.Equal(1, actual);
        }
    }
}
