
namespace ScheduleWhizRedux.Interfaces
{
    public interface IRepository
    {
        IEmployeeRepository Employees { get; }
        IJobRepository Jobs { get; }
        IAssignedJobRepository AssignedJobs { get; }
        IAssignedShiftRepository AssignedShifts { get; }
    }
}
