using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Models
{
    public class Job : IJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
    }
}
