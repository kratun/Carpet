namespace Carpet.Services.Data
{
    using System;
    using System.Linq;

    using Carpet.Common.Constants;
    using Carpet.Data.Common.Repositories;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<CarpetRole> rolesRepository;

        public RolesService(IDeletableEntityRepository<CarpetRole> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IQueryable<CarpetRole> GetAllAsync()
        {
            return this.rolesRepository.All();
        }

        public IQueryable<CarpetRole> GetAllWithoutAdministratorAsync()
        {
            return this.rolesRepository.All().Where(x => x.Name != GlobalConstants.AdministratorRoleName);
        }
    }
}
