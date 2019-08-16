namespace Carpet.Services.Data
{
    using System.Linq;

    using Carpet.Data.Models;

    public interface IRolesService
    {
        IQueryable<CarpetRole> GetAllAsync();

        IQueryable<CarpetRole> GetAllWithoutAdministratorAsync();
    }
}
