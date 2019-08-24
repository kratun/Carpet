namespace Carpet.Services.Data.OrderItemsService
{
    using System.Threading.Tasks;

    public interface IOrderItemsService
    {
        Task<bool> DeleteByIdAsync(string id);
    }
}
