namespace WebAPI.Domain.Aggregates.EmployeeAggregate
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsHigherEducation { get; private set; }
        public int? ProjectId { get; set; }
        public Employee() { }
        public Employee(int id, string firstName, string lastName, bool isHigherEducation, int projectId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsHigherEducation = isHigherEducation;
            ProjectId = projectId;
        }
    }
}
