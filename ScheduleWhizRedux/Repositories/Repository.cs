using System.Configuration;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Repositories
{
    public abstract class Repository : IRepository
    {
        public IEmployeeRepository Employees => new EmployeeRepository();
        public IJobRepository Jobs => new JobRepository();
        public IAssignedJobRepository AssignedJobs => new AssignedJobRepository();
        public IAssignedShiftRepository AssignedShifts => new AssignedShiftRepository();

        public string ConnectionString => ConfigurationManager.ConnectionStrings ["SWReDB"]. ConnectionString;
    }
}

