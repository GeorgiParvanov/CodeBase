namespace CodeBase.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IBalanceService
    {
        T GetBalance<T>(string userId);

        Task AddBalanceAsync(string userId, decimal balanceAmount);

        Task RemoveBalanceAmountAsync(string userId, decimal balanceAmount);
    }
}
