namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    //use private setters 
    //and constructor if needed
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
}
