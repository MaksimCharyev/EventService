namespace EventService.EventService
{
    public class Event
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Event(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
