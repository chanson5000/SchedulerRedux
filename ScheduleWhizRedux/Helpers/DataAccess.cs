using System;
using Dapper;
using ScheduleWhizRedux.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ScheduleWhizRedux.Helpers
{
    public static class DataAccess
    {
        //-- Employee DataAccess --//

        /// <summary>
        /// Add employee (object) to the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public static bool AddEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var insertQuery = "INSERT INTO Employees (FirstName, LastName, EmailAddress, PhoneNumber)" +
                                     "VALUES (@FirstName, @LastName, @EmailAddress, @PhoneNumber);";

                int result = connection.Execute(insertQuery,
                    new
                    {
                        employee.FirstName,
                        employee.LastName,
                        employee.EmailAddress,
                        employee.PhoneNumber
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Add employee to the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>True if successful.</returns>
        public static bool AddEmployee(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var employeeToAdd = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };

            return AddEmployee(employeeToAdd);
        }

        /// <summary>
        /// Get employee ID from employee object that. (Helpful if employee object contains no ID)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee ID. 0 if not found.</returns>
        public static int GetIdFromEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query = "select Id from Employees where " +
                            "FirstName = @FirstName and " +
                            "LastName = @LastName and " +
                            "EmailAddress = @EmailAddress and " +
                            "PhoneNumber = @PhoneNumber;";

                int result = connection.QueryFirstOrDefault<int>(query, new
                {
                    employee.FirstName,
                    employee.LastName,
                    employee.EmailAddress,
                    employee.PhoneNumber
                });

                return result;
            }
        }

        /// <summary>
        /// Get employee object from employee ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee object. Null if not found.</returns>
        public static Employee GetEmployeeFromId(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                Employee result = connection.QueryFirstOrDefault<Employee>("select * from Employees where Id = @Id;", new
                {
                    Id = id
                });

                return result;
            }
        }

        /// <summary>
        /// Get a sorted list of all employees. (by first name, last name, ID.)
        /// </summary>
        /// <returns>List of Employee objects.</returns>
        public static List<Employee> GetAllEmployees()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query = "select * from Employees order by LOWER(FirstName), LOWER(LastName), LOWER(Id);";

                List<Employee> result = connection.Query<Employee>(query).ToList();
                // TODO: Make sure we are not throwing an exception if there are no results.
                return result;
            }
        }

        /// <summary>
        /// Modify an employee in the database. (Uses the ID to determine which record to modify.)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public static bool ModifyEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var insertQuery =
                    "update Employees set FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber where Id = @Id;";

                int result = connection.Execute(insertQuery,
                    new
                    {
                        employee.FirstName,
                        employee.LastName,
                        employee.EmailAddress,
                        employee.PhoneNumber,
                        employee.Id
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove an employee from the database. Uses ID as identifier.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public static bool RemoveEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "DELETE FROM Employees WHERE Id = @Id;";

                int result = connection.Execute(insertQuery, new
                {
                    employee.Id
                });

                return result != 0;
            }
        }

        //-- Job DataAccess --//

        /// <summary>
        /// Get a job object from a job ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Job object.</returns>
        public static Job GetJobFromId(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select * from Jobs where Id @Id;";

                Job result = connection.Query<Job>(queryString,
                    new
                    {
                        Id = id
                    }).First();

                return result;
            }
        }

        /// <summary>
        /// Get a job ID from a job title.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>Integer for job ID.</returns>
        public static int GetJobIdFromTitle(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select Id from Jobs where JobTitle = @JobTitle;";

                int result = connection.QueryFirstOrDefault<int>(queryString,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result;
            }
        }

        /// <summary>
        /// Get a job record from a job title.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>Job object.</returns>
        public static Job GetJobRecordFromTitle(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault<Job>(queryString,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result;
            }
        }

        /// <summary>
        /// Check if a job exists in the database by title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>True if job exists.</returns>
        public static bool JobExists(string title)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string query = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault(query, new { JobTitle = title });

                return result != null;
            }
        }

        /// <summary>
        /// Check if a job exists by job record. Detects whether job ID has been correctly set.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public static bool JobExists(Job job)
        {
            return job.Id == 0 ? JobExists(job.JobTitle) : JobRecordExists(job);
        }

        /// <summary>
        /// Check if a job exists in the database by job object. More explicit than JobExists.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if job exists.</returns>
        public static bool JobRecordExists(Job job)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string query = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault<Job>(query,
                    new
                    {
                        job.Id,
                        job.JobTitle
                    });

                return result != null;
            }
        }

        /// <summary>
        /// Get a list of all job records from the database.
        /// </summary>
        /// <returns>A list of job objects.</returns>
        public static List<Job> GetAllJobRecords()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Job>("select * from Jobs order by LOWER(JobTitle);").ToList();
                // TODO: Make sure we are not throwing an exception if there are no results.
                return result;
            }
        }

        /// <summary>
        /// Get list of all job titles from the database.
        /// </summary>
        /// <returns>A list of strings.</returns>
        public static List<string> GetAllJobTitles()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select * from Jobs order by LOWER(JobTitle);";

                var result = connection.Query<Job>(queryString).ToList();

                List<string> listResult = new List<string>();

                foreach (var jobRecord in result)
                {
                    listResult.Add(jobRecord.JobTitle);
                }

                return listResult;
            }
        }

        /// <summary>
        /// Add a job to the database.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>True if successful.</returns>
        public static bool AddJob(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "INSERT INTO Jobs (JobTitle) VALUES (@JobTitle);";

                int result = connection.Execute(insertQuery, new
                {
                    JobTitle = jobTitle
                });

                return result != 0;
            }
        }

        /// <summary>
        /// Modify a job record. (Uses ID to determine which record to modify)
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public static bool ModifyJob(Job job)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string updateQuery = "UPDATE Jobs SET JobTitle = @JobTitle WHERE Id = @Id;";

                int result = connection.Execute(updateQuery,
                    new
                    {
                        job.JobTitle,
                        job.Id
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a job from a job object.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public static bool RemoveJob(Job job)
        {
            if (job.Id == 0)
            {
                return RemoveJob(job.JobTitle);
            }

            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string deleteQuery = "DELETE FROM Jobs WHERE Id = @Id;";

                int result = connection.Execute(deleteQuery,
                    new
                    {
                        job.Id
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a job from the database.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>True if successful.</returns>
        public static bool RemoveJob(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string deleteQuery = "DELETE FROM Jobs where JobTitle = @JobTitle;";

                int result = connection.Execute(deleteQuery,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result != 0;
            }
        }

        //-- AssignedJob DataAccess --//
        
        /// <summary>
        /// Get jobs assigned to an employee by employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of strings.</returns>
        public static List<string> GetEmployeeAssignedJobs(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var assigneJobIds = connection.Query<int>("select JobId from AssignedJobs where EmployeeId = @EmployeeId",
                    new
                    {
                        EmployeeId = id
                    }).ToList();

                List<string> result = new List<string>();
                List<Job> allJobs = GetAllJobRecords();

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
        public static List<string> GetEmployeeAssignedJobs(Employee employee)
        {
            return GetEmployeeAssignedJobs(employee.Id != 0 ? employee.Id : GetIdFromEmployee(employee));
        }

        /// <summary>
        /// Get jobs available for assignment to an employee by employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of strings.</returns>
        public static List<string> GetEmployeeAvailableJobs(int id)
        {
            var assignedJobs = GetEmployeeAssignedJobs(id);

            List<string> allAvailableJobs = GetAllJobTitles();

            if (!assignedJobs.Any()) return GetAllJobTitles();

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
        public static List<string> GetEmployeeAvailableJobs(Employee employee)
        {
            return GetEmployeeAssignedJobs(employee.Id != 0 ? employee.Id : GetIdFromEmployee(employee));
        }

        public static bool AssignJobToEmployee(Job job, Employee employee)
        {
            if (IsJobAssignedToEmployee(job.JobTitle, employee))
            {
                return false;
            }
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryAssignmentString =
                    "insert into AssignedJobs (JobId, EmployeeId) values (@JobId, @EmployeeId);";

                var result = connection.Execute(queryAssignmentString,
                    new { @JobId = job.Id, @EmployeeId = employee.Id });

                return result != 0;
            }
        }

        /// <summary>
        /// Unassign a job from an employee.
        /// </summary>
        /// <param name="job"></param>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public static bool UnAssignJobFromEmployee(Job job, Employee employee)
        {
            if (!IsJobAssignedToEmployee(job.JobTitle, employee))
            {
                return false;
            }
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryUnAssignmentString =
                    "delete from AssignedJobs where JobId = @JobId and EmployeeId = @EmployeeId;";

                var result = connection.Execute(queryUnAssignmentString,
                    new { @JobId = job.Id, @EmployeeId = employee.Id });

                return result != 0;
            }
        }

        /// <summary>
        /// Check if a job is assigned to an employee.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <param name="employee"></param>
        /// <returns>True if job is assigned to an employee.</returns>
        public static bool IsJobAssignedToEmployee(string jobTitle, Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var jobId = GetJobIdFromTitle(jobTitle);

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

        //-- Assigned Shifts Data Access --//
        
        /// <summary>
        /// Add a new shift for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <param name="numAvailable"></param>
        /// <returns>Return true if successful.</returns>
        public static bool AddShiftForJobOnDay(DayOfWeek day, string jobTitle, string shiftName, int numAvailable = 0)
        {
            var jobId = GetJobIdFromTitle(jobTitle);
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query =
                    "insert into AssignedShifts (DayOfWeek, JobId, ShiftName, NumAvailable) values (@DayOfWeek, @JobId, @ShiftName, @NumAvailable);";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName,
                        NumAvailable = numAvailable
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a shift for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <returns>True if successful.</returns>
        public static bool RemoveShiftForJobOnDay(DayOfWeek day, string jobTitle, string shiftName)
        {
            var jobId = GetJobIdFromTitle(jobTitle);
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query =
                    "delete from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Get available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <returns>A list of strings.</returns>
        public static List<string> GetAvailableShiftsForJobOnDay(DayOfWeek day, string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var jobId = GetJobIdFromTitle(jobTitle);

                var query = "select ShiftName from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId;";

                var result = connection.Query<string>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId
                    }).ToList();
                
                return result;
            }
        }

        /// <summary>
        /// Get the number of available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <returns>Returns an integer.</returns>
        public static int GetNumAvailableShiftsForJobOnDay(DayOfWeek day, string jobTitle, string shiftName)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var jobId = GetJobIdFromTitle(jobTitle);

                var query =
                    "select NumAvailable from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.QueryFirstOrDefault<int>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result;
            }
        }

        /// <summary>
        /// Sets the number of available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <param name="numShiftsAvailable"></param>
        /// <returns>Returns true if successful.</returns>
        public static bool SetNumAvailableShiftsForJobOnDay(DayOfWeek day, string jobTitle, string shiftName,
            int numShiftsAvailable)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var jobId = GetJobIdFromTitle(jobTitle);

                var query =
                    "update AssignedShifts set NumAvailable = @NumAvailable where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName,
                        NumAvailable = numShiftsAvailable
                    });

                return result != 0;
            }
        }

        // This may not be needed.
        /// <summary>
        /// Get an AssignedShift object record.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobId"></param>
        /// <param name="shiftName"></param>
        /// <returns>Returns an AssignedShift object.</returns>
        public static AssignedShift GetAssignedShiftInfo(DayOfWeek day, int jobId, string shiftName)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query =
                    "select * from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.QueryFirstOrDefault<AssignedShift>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result;
            }
        }
    }
}
