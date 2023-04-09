namespace Application.Events
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string HostUsername { get; set; }
        public string HostDisplayname { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
