namespace WebAPI.Application.DataTransferObjects
{
    public class EmployeeGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHigherEducation { get; set; }
    }
}
