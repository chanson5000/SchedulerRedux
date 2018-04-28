using System.Configuration;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Repositories
{
    public class Repository : IRepository
    {
        public Repository()
        {
            Employees = new EmployeeRepository();
            Jobs = new JobRepository();
            AssignedJobs = new AssignedJobRepository();
            AssignedShifts = new AssignedShiftRepository();
        }

        public IEmployeeRepository Employees { get; }
        public IJobRepository Jobs { get; }
        public IAssignedJobRepository AssignedJobs { get; }
        public IAssignedShiftRepository AssignedShifts { get; }
        public string ConnectionString => ConfigurationManager. ConnectionStrings ["SWReDB"]. ConnectionString;
    }
}

