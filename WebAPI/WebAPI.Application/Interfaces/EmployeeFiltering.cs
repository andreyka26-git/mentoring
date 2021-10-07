namespace WebAPI.Application.Interfaces
{
    public class EmployeeFiltering
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHigherEducation { get; set; } = true;
    }
}
