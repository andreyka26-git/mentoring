namespace WebAPI.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHigherEducation { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
