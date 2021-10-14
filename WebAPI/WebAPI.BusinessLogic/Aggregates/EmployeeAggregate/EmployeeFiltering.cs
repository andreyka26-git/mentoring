using WebAPI.Domain.Aggregates.Common;

namespace WebAPI.Domain.Aggregates.EmployeeAggregate
{
    public class EmployeeFiltering
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsHigherEducation { get; set; }
        public PagingModel PagingModel { get; set; } = new();
    }
}
