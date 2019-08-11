namespace Carpet.Web.Areas.Administration.Controllers
{
    using Carpet.Common.Constants;
    using Carpet.Web.Controllers;
    using Carpet.Web.Infrastructure.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(ViewConstants.AdministrationAreaName)]
    public class AdministrationController : BaseController
    {
    }
}
