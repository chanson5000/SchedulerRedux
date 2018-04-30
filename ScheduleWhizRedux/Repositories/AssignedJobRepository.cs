using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Repositories
{
    public class AssignedJobRepository : Repository, IAssignedJobRepository
    {
        /// <summary>
        /// Get jobs assigned to an employee by employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of strings.</returns>
        public List<string> Get(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var assigneJobIds = connection.Query<int>("select JobId from AssignedJobs where EmployeeId = @EmployeeId",
                    new
                    {
                        EmployeeId = id
                    }).ToList();

                List<string> result = new List<string>();

                List<Job> allJobs = Jobs.GetAllSorted();

                foreach (var assignedJobRecord in assigneJobIds)
                {
                    foreach (var jobRecord in allJobs)
                    {
                        if (assignedJobRecord == jobRecord.Id)
                        {
                            result.Add(jobRecord.JobTitle);
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Get jobs assigned to an employee by employee object.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>A list of strings.</returns>
        public List<string> Get(Employee employee)
        {
            return Get(employee.Id != 0 ? employee.Id : Employees.GetId(employee));
        }

        /// <summary>
        /// Get jobs available for assignment to an employee by employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of strings.</returns>
        public List<string> GetAvailable(int id)
        {
            var assignedJobs = Get(id);

            if (!assignedJobs.Any()) return Jobs.GetAllTitles();

            List<string> allAvailableJobs = Jobs.GetAllTitles();

            foreach (var job in assignedJobs)
            {
                if (allAvailableJobs.Contains(job))
                {
                    allAvailableJobs.Remove(job);
                }
            }

            return allAvailableJobs;
        }

        /// <summary>
        /// Get jobs available for assignement to an employee by employee object.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>A list of strings.</returns>
        public List<string> GetAvailable(Employee employee)
        {
            return Get(employee.Id != 0 ? employee.Id : Employees.GetId(employee));
        }

        public bool Add(Job job, Employee employee)
        {
            if (IsJobAssignedToEmployee(job.JobTitle, employee))
            {
                return false;
            }
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var queryAssignmentString =
                    "insert into AssignedJobs (JobId, EmployeeId) values (@JobId, @EmployeeId);";

                int result = connection.Execute(queryAssignmentString, new
                {
                    JobId = job.Id, EmployeeId = employee.Id
                });

                return result != 0;
            }
        }

        /// <summary>
        /// Unassign a job from an employee.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public bool Remove(Job job, Employee employee)
        {
            if (!IsJobAssignedToEmployee(job.JobTitle, employee))
            {
                return false;
            }
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query =
                    "delete from AssignedJobs where JobId = @JobId and EmployeeId = @EmployeeId;";

                int result = connection.Execute(query, new
                {
                    JobId = job.Id,
                    EmployeeId = employee.Id
                });

                return result != 0;
            }
        }

        /// <summary>
        /// Check if a job is assigned to an employee.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <param name="employee"></param>
        /// <returns>True if job is assigned to an employee.</returns>
        public bool IsJobAssignedToEmployee(string jobTitle, Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var jobId = Jobs.GetId(jobTitle);

                var query = "select * from AssignedJobs where EmployeeId = @EmployeeId and JobId = @JobId;";

                AssignedJob result = connection.QueryFirstOrDefault<AssignedJob>(query,
                    new
                    {
                        EmployeeId = employee.Id,
                        JobId = jobId
                    });

                return result != null;
            }
        }

        /// <summary>
        /// Check if a job is assigned to an employee.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="employee"></param>
        /// <returns>True if job is assigned to an employee.</returns>
        public bool IsJobAssignedToEmployee(Job job, Employee employee)
        {
            return IsJobAssignedToEmployee(job.JobTitle, employee);
        }

        /// <summary>
        /// Get all assigned job records.
        /// </summary>
        /// <returns>Returns a list of assigned job records.</returns>
        public List<AssignedJob> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = "select * from AssignedJobs;";

                List<AssignedJob> result = connection.Query<AssignedJob>(query).ToList();

                return result;
            }
        }
    }
}
