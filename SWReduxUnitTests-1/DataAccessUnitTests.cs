using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;
using Xunit;

namespace SWReduxUnitTests_1
{
    public class DataAccessUnitTests
    {
        [Fact]
        public void AddEmployee_ReturnTrueIfSuccessful()
        {
            Employee employeeTestCase = new Employee()
            {
                FirstName = "TestAddFirstName",
                LastName = "TestAddLastName",
                EmailAddress = "TestAddEmailAddress",
                PhoneNumber = "TestAddPhoneNumber"
            };
            var result = DataAccess.AddEmployee(employeeTestCase);
            employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);
            DataAccess.RemoveEmployee(employeeTestCase);

            Assert.True(result);
        }

        [Fact]
        public void GetIdFromEmployee_ReturnEmployeeId()
        {
            Employee employeeTestCase = new Employee()
            {
                FirstName = "TestGetIdFirstName",
                LastName = "TestGetIdLastName",
                EmailAddress = "TestGetIdEmailAddress",
                PhoneNumber = "TestGetIdPhoneNumber"
            };
            DataAccess.AddEmployee(employeeTestCase);
            var result = DataAccess.GetIdFromEmployee(employeeTestCase);
            employeeTestCase.Id = result;

            DataAccess.RemoveEmployee(employeeTestCase);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void GetEmployeeFromId_IdShouldEqualId()
        {
            Employee employeeTestCase = new Employee()
            {
                FirstName = "TestGetEmpFirstName",
                LastName = "TestGetEmpLastName",
                EmailAddress = "TestGetEmpEmailAddress",
                PhoneNumber = "TestGetEmpPhoneNumber"
            };
            DataAccess.AddEmployee(employeeTestCase);
            employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);
            var result = DataAccess.GetEmployeeFromId(employeeTestCase.Id);

            DataAccess.RemoveEmployee(employeeTestCase);

            Assert.Equal(employeeTestCase.Id, result.Id);
        }

        [Fact]
        public void RemoveEmployee_ShouldRemove()
        {
            Employee employeeTestCase = new Employee()
            {
                FirstName = "TestRemoveEmpFirstName",
                LastName = "TestRemoveEmpLastName",
                EmailAddress = "TestRemoveEmpEmailAddress",
                PhoneNumber = "TestRemoveEmpPhoneNumber"
            };
            DataAccess.AddEmployee(employeeTestCase);
            employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);

            var result = DataAccess.RemoveEmployee(employeeTestCase);

            Assert.True(result);
        }

        [Fact]
        public void GetAllEmployees_ShouldReturnEmployeeList()
        {
            var result = DataAccess.GetAllEmployees();

            Assert.IsType<List<Employee>>(result);
        }

        [Fact]
        public void ModifyEmployee_ShouldUpdate()
        {
            Employee employeeTestCase = new Employee()
            {
                FirstName = "TestModFirstName",
                LastName = "TestModLastName",
                EmailAddress = "TestModEmailAddress",
                PhoneNumber = "TestModPhoneNumber"
            };

            DataAccess.AddEmployee(employeeTestCase);

            employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);

            employeeTestCase.FirstName = "TestNewFirstName";
            employeeTestCase.LastName = "TestNewLastName";
            employeeTestCase.EmailAddress = "TestNewEmailAddress";
            employeeTestCase.PhoneNumber = "testNewPhoneNumber";

            DataAccess.ModifyEmployee(employeeTestCase);

            Employee modifiedFromDatabase = DataAccess.GetEmployeeFromId(employeeTestCase.Id);

            DataAccess.RemoveEmployee(employeeTestCase);
            
            Assert.Equal(employeeTestCase.Id, employeeTestCase.Id);
            Assert.Equal(employeeTestCase.FirstName, modifiedFromDatabase.FirstName);
            Assert.Equal(employeeTestCase.LastName, modifiedFromDatabase.LastName);
            Assert.Equal(employeeTestCase.EmailAddress, modifiedFromDatabase.EmailAddress);
            Assert.Equal(employeeTestCase.PhoneNumber, modifiedFromDatabase.PhoneNumber);
        }

        [Fact]
        public void AddJob()
        {
            Job newTestJob = new Job()
            {
                Title = "Add Job Test"
            };

            var result = DataAccess.AddJob(newTestJob.Title);

            newTestJob.Id = DataAccess.GetJobIdFromTitle(newTestJob.Title);

            List<Job> allJobs = DataAccess.GetAllJobs();

            DataAccess.RemoveJob(newTestJob);

            Assert.True(result);
            // Why does this Assert not work like I think it should.
            Assert.DoesNotContain(newTestJob, allJobs);
        }
    }
}
