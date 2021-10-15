namespace WebAPI.Domain.Aggregates.ProjectAggregate
{
    public class Project
    {
        private Project() { }

        public Project(int id, string name, int duration)
        {
            Id = id;
            Name = name;
            Duration = duration;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Duration { get; private set; }
    }
}
