namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public int Duration { get; private set; }
        public Project() { }

        public Project(int id, string name, int duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }
    }
}
