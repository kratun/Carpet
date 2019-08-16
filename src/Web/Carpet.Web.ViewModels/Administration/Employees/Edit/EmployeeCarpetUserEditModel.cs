namespace Carpet.Web.ViewModels.Administration.Employees.Edit
{
    using Carpet.Data.Models;
    using Carpet.Services.Mapping;

    public class EmployeeCarpetUserEditModel : IMapFrom<CarpetUser>
    {
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
