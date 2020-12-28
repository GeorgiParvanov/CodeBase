namespace CodeBase.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;
    using CodeBase.Web.ViewModels.Balance;
    using Moq;
    using Xunit;

    public class BalanceServiceTests
    {
        [Fact]
        public async Task AddBalanceShouldIncreaseUserBalanceWithCorrectAmount()
        {
            var list = new List<ApplicationUser>() { new ApplicationUser { Id = "Id", Balance = 0 } };
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new BalanceService(repository.Object);

            await service.AddBalanceAsync("Id", 5);

            Assert.Equal(5, list.First().Balance);
        }

        //[Fact]
        //public void GetBalanceShouldReturnTheSearchUserBalanceById()
        //{
        //    var list = new List<ApplicationUser>() { new ApplicationUser { Id = "Id", Balance = 10 } };
        //    var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
        //    repository.Setup(x => x.All()).Returns(list.AsQueryable());
        //    var service = new BalanceService(repository.Object);

        //    var balance = service.GetBalance<BalanceViewModel>("Id").Balance;

        //    Assert.Equal(10, balance);
        //}

        [Fact]
        public async Task RemoveBalanceAmountAsyncShouldDecreaseUserBalanceWithCorrectAmount()
        {
            var list = new List<ApplicationUser>() { new ApplicationUser { Id = "Id", Balance = 10 } };
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new BalanceService(repository.Object);

            await service.RemoveBalanceAmountAsync("Id", 5);

            Assert.Equal(5, list.First().Balance);
        }
    }
}
