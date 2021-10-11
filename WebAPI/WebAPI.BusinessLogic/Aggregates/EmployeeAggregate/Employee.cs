﻿namespace WebAPI.Domain.Aggregates.EmployeeAggregate
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHigherEducation { get; set; }
        public int? ProjectId { get; set; }
    }
}
