namespace Carpet.Services.Data.OrdersService
{
    using System.Linq;
    using System.Threading.Tasks;

    using Carpet.Web.InputModels.Administration.Orders.Create;
    using Carpet.Web.ViewModels.Administration.Orders.Create;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public interface IOrdersService
    {
        IQueryable<TViewModel> GetAllAsync<TViewModel>();

        Task<TViewModel> GetByIdAsync<TViewModel>(string id);

        Task<OrderCreateViewModel> CreateAsync(OrderCreateInputModel orderFromView, string username, ModelStateDictionary modelState);
    }
}
