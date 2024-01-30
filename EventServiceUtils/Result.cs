using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace EventService.EventService
{
    public class Result
    {
        public string CurrentTime { get; set; }
        public int Sum { get; set; }
        public Result(Queue<Event> events)
        {
            foreach(var e in events)
            {
                Sum += e.Value;
            }
            var TempTime = DateTime.Now;
            CurrentTime = (new DateTime(TempTime.Year,TempTime.Month,TempTime.Day, TempTime.Hour, TempTime.Minute, 0)).ToString();
        }
    }
}
