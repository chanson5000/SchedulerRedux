using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.Models
{
    public class AssignedJob
    {
        public int EmployeeId { get; set; }
        public int JobId { get; set; }

        public Employee Employee => new Repository().Employees.Get(EmployeeId);
        public string JobTitle => new Repository().Jobs.Get(JobId).JobTitle;
    }
}
