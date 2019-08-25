namespace Carpet.Services.Data.OrderItemsService
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IOrderItemsService
    {
        Task<bool> DeleteByIdAsync(string id);

        IQueryable<TViewModel> GetAllAsNoTrackingWithDeteletedAsync<TViewModel>();
    }
}
