namespace CodeBase.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using CodeBase.Data.Common.Repositories;
    using CodeBase.Data.Models;
    using CodeBase.Services.Data.Contracts;
    using CodeBase.Services.Mapping;

    public class BalanceService : IBalanceService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public BalanceService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddBalanceAsync(string userId, decimal balanceAmount)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

            user.Balance += balanceAmount;

            await this.userRepository.SaveChangesAsync();
        }

        public T GetBalance<T>(string userId)
        {
            return this.userRepository.All()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task RemoveBalanceAmountAsync(string userId, decimal balanceAmount)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

            user.Balance -= balanceAmount;

            await this.userRepository.SaveChangesAsync();
        }
    }
}
