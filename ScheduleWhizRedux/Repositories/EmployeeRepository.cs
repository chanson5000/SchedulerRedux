using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Repositories
{
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        //private static string ConnectionString => ConfigurationManager.ConnectionStrings["SWReDB"].ConnectionString;
        /// <summary>
        /// Add an employee to the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public bool Add(Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = "insert into Employees (FirstName, LastName, EmailAddress, PhoneNumber) " +
                            "values (@FirstName, @LastName, @EmailAddress, @PhoneNumber);";

                // Execute queries return rows affected.
                int result = connection.Execute(query, new
                    {
                        employee.FirstName,
                        employee.LastName,
                        employee.EmailAddress,
                        employee.PhoneNumber
                    });

                // Return true if a row was affected.
                return result != 0;
            }
        }

        /// <summary>
        /// Add an employee to the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>True if successful.</returns>
        public bool Add(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            var employeeToAdd = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };

            return Add(employeeToAdd);
        }

        /// <summary>
        /// Check if an employee exists in the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Returns true if exists.</returns>
        public bool Exists(Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = "select * from Employees where " +
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

                return result != 0;
            }
        }

        /// <summary>
        /// Get an employee ID from an employee object.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee ID. 0 if not found.</returns>
        public int GetId(Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
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
        /// Get an employee object from an employee ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee object. Null if not found.</returns>
        public Employee Get(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                Employee result = connection.QueryFirstOrDefault<Employee>("select * from Employees where Id = @Id;", new
                {
                    Id = id
                });

                return result;
            }
        }

        /// <summary>
        /// Get a sorted list of all employees. (first name, last name, id)
        /// </summary>
        /// <returns>List of Employee object.</returns>
        public List<Employee> GetAllSorted()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = "select * from Employees order by lower(FirstName), lower(LastName), lower(Id);";

                List<Employee> result = connection.Query<Employee>(query).ToList();

                return result;
            }
        }

        /// <summary>
        /// Modify an employee in the database. Uses Id as an identifier.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public bool Modify(Employee employee)
        {
            // Employee Id is used to find the record to modify.
            if (employee.Id.Equals(0)) return false;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query =
                    "update Employees set FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber where Id = @Id;";

                var result = connection.Execute(query,
                    new
                    {
                        employee.FirstName,
                        employee.LastName,
                        employee.EmailAddress,
                        employee.PhoneNumber,
                        employee.Id
                    });

                // Return true if a row was affected.
                return result != 0;
            }
        }

        /// <summary>
        /// Remove an employee from the database. Uses Id as identifier.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if successful.</returns>
        public bool Remove(Employee employee)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string insertQuery = "delete from Employees where Id = @Id;";

                int result = connection.Execute(insertQuery, new
                {
                    employee.Id
                });

                // Return true if a row was affected.
                return result != 0;
            }
        }


    }
}
