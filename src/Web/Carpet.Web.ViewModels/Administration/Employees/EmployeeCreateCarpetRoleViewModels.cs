namespace Carpet.Web.ViewModels.Administration.Employees
{
    using System.ComponentModel.DataAnnotations;

    using Carpet.Common.Constants;
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class EmployeeCreateCarpetRoleViewModels : IMapTo<CarpetRole>, IMapFrom<CarpetRole>
    {
        [Display(Name = EmployeeConstants.DisplayNameEmployeeRoleName)]
        public string Nane { get; set; }
    }
}
