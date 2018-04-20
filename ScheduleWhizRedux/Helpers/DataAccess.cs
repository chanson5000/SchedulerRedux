using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ScheduleWhizRedux.Models;
using Dapper;

namespace ScheduleWhizRedux.Helpers
{
    public static class DataAccess
    {
        // Employee DataAcess
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
        public static string GetJobTitleFromId(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Job>("select Title from Jobs where Id = @Id;",
                    new
                    {
                        Id = id
                    }).ToString();

                return result;
            }
        }

        public static bool JobExists(string title)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string query = "select * from Jobs where Titel = @Title;";

                var result = connection.QueryFirstOrDefault(query, new {Title = title});

                return result != null;
            }
        }

        public static List<Job> GetAllJobs()
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var result = connection.Query<Job>("select * from Jobs order by LOWER(Title);").ToList();

                return result;
            }
        }

        public static bool AddJob(string job)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                string insertQuery = "INSERT INTO Jobs (Title) VALUES (@Title);";

                int result = connection.Execute(insertQuery, new
                {
                    Title = job
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
                var jobIds = connection.Query<AssignedJob>("select * from AssignedJobs where EmployeeId = @EmployeeId", new { EmployeeId = id }).ToList();

                List<string> result = new List<string>();

                foreach (var record in GetAllJobs())
                {
                    foreach (var jobId in jobIds)
                    {
                        if (record.Id == jobId.JobId)
                        {
                            result.Add(record.Title);
                        }
                    }
                }

                return result;
            }
        }

        public static List<string> GetEmployeeAvailableJobs(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(Helper.SQLiteConnString()))
            {
                var assignedJobRecords = connection.Query<AssignedJob>("select * from AssignedJobs where EmployeeId = @EmployeeId",
                    new { EmployeeId = id }).ToList();

                List<string> result = new List<string>();

                foreach (var record in assignedJobRecords)
                {
                    foreach (var job in GetAllJobs())
                    {
                        if (record.JobId != job.Id)
                        {
                            result.Add(job.Title);
                        }
                    }
                }

                return result;
            }
        }
    }
}
