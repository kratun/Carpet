namespace Carpet.Data
{
    using System.Security.Claims;

    using Carpet.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationRoleStore : RoleStore<
        CarpetRole,
        ApplicationDbContext,
        string,
        IdentityUserRole<string>,
        IdentityRoleClaim<string>>
    {
        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(CarpetRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
