namespace CodeBase.Web.ViewModels.Balance
{
    using System.ComponentModel.DataAnnotations;

    using CodeBase.Data.Models;
    using CodeBase.Services.Mapping;

    public class BalanceInputModel : IMapFrom<ApplicationUser>
    {
        [Range(0, 50000, ErrorMessage = "{0} has to be positive and less than {2} bgn.")]
        public decimal Balance { get; set; }
    }
}
