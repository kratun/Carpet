namespace Carpet.Data.Common.Models
{
    public interface INamableEntity
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string FullName { get; }
    }
}
