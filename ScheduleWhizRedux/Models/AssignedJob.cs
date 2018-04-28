using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.Models
{
    public class AssignedJob
    {
        public int EmployeeId { get; set; }
        public int JobId { get; set; }

        public Employee Employee => new EmployeeRepository().Get(EmployeeId);
        public string JobTitle => new JobRepository().Get(JobId).JobTitle;
    }
}
