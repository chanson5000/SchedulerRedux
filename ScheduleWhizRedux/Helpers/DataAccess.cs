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
        // Employee DataAcess
        public static bool AddEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "INSERT INTO Employees (FirstName, LastName, EmailAddress, PhoneNumber)" +
                                     "VALUES (@FirstName, @LastName, @EmailAddress, @PhoneNumber);";

                var result = connection.Execute(insertQuery,
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

        public static bool AddEmployee(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            Employee employeeToAdd = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };

            return AddEmployee(employeeToAdd);
        }

        // Get Id from Employee // For unit testing purposes.
        public static int GetIdFromEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var query = "select * from Employees where " +
                            "FirstName = @FirstName and " +
                            "LastName = @LastName and " +
                            "EmailAddress = @EmailAddress and " +
                            "PhoneNumber = @PhoneNumber;";

                var result = connection.QueryFirstOrDefault<Employee>(query, new
                {
                    employee.FirstName,
                    employee.LastName,
                    employee.EmailAddress,
                    employee.PhoneNumber
                });

                return result?.Id ?? 0;
            }
        }

        public static Employee GetEmployeeFromId(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Employee>("select * from Employees where Id = @Id;", new
                {
                    Id = id
                }).First();

                return result;
            }
        }

        public static List<Employee> GetAllEmployees()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Employee>("select * from Employees order by LOWER(FirstName), LOWER(LastName), LOWER(Id);").ToList();

                return result;
            }
        }

        public static bool ModifyEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery =
                    "update Employees set FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber where Id = @Id;";

                var result = connection.Execute(insertQuery,
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

        public static bool RemoveEmployee(Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "DELETE FROM Employees WHERE Id = @Id;";

                var result = connection.Execute(insertQuery, new
                {
                    employee.Id
                });

                return result != 0;
            }
        }

        // Job DataAccess
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

        public static int GetJobIdFromTitle(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select Id from Jobs where Title = @Title;";

                var result = connection.QueryFirstOrDefault<Job>(queryString,
                    new
                    {
                        Title = jobTitle
                    });

                return result.Id;
            }
        }

        public static Job GetJobRecordFromTitle(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select * from Jobs where Title = @Title;";

                var result = connection.QueryFirstOrDefault<Job>(queryString,
                    new
                    {
                        Title = jobTitle
                    });

                return result;
            }
        }

        public static bool JobExists(string title)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string query = "select * from Jobs where Title = @Title;";

                var result = connection.QueryFirstOrDefault(query, new { Title = title });

                return result != null;
            }
        }

        public static bool JobRecordExists(Job job)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string query = "select * from Jobs where Title = @Title;";

                var result = connection.QueryFirstOrDefault(query,
                    new
                    {
                        job.Id,
                        job.Title
                    });

                return result != null;
            }
        }

        // Probably unecessary.
        public static bool JobExists(Job job)
        {
            return JobExists(job.Title);
        }

        public static List<Job> GetAllJobRecords()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Job>("select * from Jobs order by LOWER(Title);").ToList();

                return result;
            }
        }

        public static List<string> GetAllJobTitles()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var queryString = "select * from Jobs order by LOWER(Title);";

                var result = connection.Query<Job>(queryString).ToList();

                List<string> listResult = new List<string>();

                foreach (var jobRecord in result)
                {
                    listResult.Add(jobRecord.Title);
                }

                return listResult;
            }
        }

        public static bool AddJob(string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "INSERT INTO Jobs (Title) VALUES (@Title);";

                int result = connection.Execute(insertQuery, new
                {
                    Title = jobTitle
                });

                return result != 0;
            }
        }

        public static bool ModifyJob(Job job)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string updateQuery = "UPDATE Jobs SET Title = @Title WHERE Id = @Id;";

                int result = connection.Execute(updateQuery,
                    new
                    {
                        job.Title,
                        job.Id
                    });

                return result != 0;
            }
        }

        public static bool RemoveJob(Job job)
        {
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

        // AssignedJob DataAccess
        public static List<string> GetEmployeeAssignedJobs(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var assignedJobRecords = connection.Query<AssignedJob>("select * from AssignedJobs where EmployeeId = @EmployeeId",
                    new
                    {
                        EmployeeId = id
                    }).ToList();

                List<string> result = new List<string>();
                List<Job> allJobs = GetAllJobRecords();

                foreach (var assignedJobRecord in assignedJobRecords)
                {
                    foreach (var jobRecord in allJobs)
                    {
                        if (assignedJobRecord.JobId == jobRecord.Id)
                        {
                            result.Add(jobRecord.Title);
                        }
                    }
                }

                return result;
            }
        }

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

        public static bool AssignJobToEmployee(Job job, Employee employee)
        {
            if (IsJobAssignedToEmployee(job.Title, employee))
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

        public static bool UnAssignJobFromEmployee(Job job, Employee employee)
        {
            if (!IsJobAssignedToEmployee(job.Title, employee))
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

        public static bool IsJobAssignedToEmployee(string jobTitle, Employee employee)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var jobId = GetJobIdFromTitle(jobTitle);

                var query = "select * from AssignedJobs where EmployeeId = @EmployeeId and JobId = @JobId;";

                var result = connection.QueryFirstOrDefault(query,
                    new
                    {
                        EmployeeId = employee.Id,
                        JobId = jobId
                    });

                return result != null;
            }
        }
    }
}
