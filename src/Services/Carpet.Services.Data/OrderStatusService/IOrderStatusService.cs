namespace Carpet.Services.Data.OrderStatusService
{
    using System.Threading.Tasks;

    using Carpet.Data.Models;

    public interface IOrderStatusService
    {
        Task<OrderStatus> CreateAsync(string orderStatusName);

        Task<int> GetIdByNameAsync(string name);
    }
}
