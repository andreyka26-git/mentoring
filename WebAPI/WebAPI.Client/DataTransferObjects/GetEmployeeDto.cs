namespace WebAPI.Client.DataTransferObjects
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHigherEducation { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Full name: {FirstName} {LastName}, Higher education: {IsHigherEducation};";
        }
    }
}
