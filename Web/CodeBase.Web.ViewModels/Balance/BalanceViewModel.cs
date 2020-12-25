namespace CodeBase.Web.ViewModels.Balance
{
    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class BalanceViewModel : IMapFrom<ApplicationUser>
    {
        public decimal Balance { get; set; }
    }
}
