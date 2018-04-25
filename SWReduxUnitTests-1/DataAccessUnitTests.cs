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
        public void AddJob_ShouldReturnTrue()
        {
            Job newTestJob = new Job()
            {
                JobTitle = "Add Job Test"
            };

            var result = DataAccess.AddJob(newTestJob.JobTitle);

            newTestJob.Id = DataAccess.GetJobIdFromTitle(newTestJob.JobTitle);

            List<Job> allJobs = DataAccess.GetAllJobRecords();

            DataAccess.RemoveJob(newTestJob);

            Assert.True(result);
            // TODO: Why does this Assert not work like I think it should.
            Assert.DoesNotContain(newTestJob, allJobs);
        }

        [Fact]
        public void AssignJobToEmployee_ShouldReturnTrue()
        {
            Job newTestJobToAssign = new Job()
            {
                JobTitle = "AssignJobTest"
            };

            Employee newTestAssignJobEmployee = new Employee()
            {
                FirstName = "TAJFirstName",
                LastName = "TAJLastName",
                EmailAddress = "TAJEmailAddress",
                PhoneNumber = "TAJPhoneNumber"
            };

            DataAccess.AddJob(newTestJobToAssign.JobTitle);
            DataAccess.AddEmployee(newTestAssignJobEmployee);

            newTestJobToAssign.Id = DataAccess.GetJobIdFromTitle(newTestJobToAssign.JobTitle);

            newTestAssignJobEmployee.Id = DataAccess.GetIdFromEmployee(newTestAssignJobEmployee);

            var result = DataAccess.AssignJobToEmployee(newTestJobToAssign, newTestAssignJobEmployee);

            DataAccess.RemoveJob(newTestJobToAssign);
            DataAccess.RemoveEmployee(newTestAssignJobEmployee);

            Assert.True(result);
        }

        [Fact]
        public void AddShiftForJobOnDay_ReturnTrueIfSuccessful()
        {
            var jobTitle = "TestJobAssignment";
            DataAccess.AddJob(jobTitle);
            var dayOfWeek = DayOfWeek.Monday;
            var shiftToAdd = "TestShiftAssignment";


            var result = DataAccess.AddShiftForJobOnDay(dayOfWeek, jobTitle, shiftToAdd);

            DataAccess.RemoveShiftForJobOnDay(dayOfWeek, jobTitle, shiftToAdd);

            Job jobToRemove = new Job()
            {
                Id = DataAccess.GetJobIdFromTitle(jobTitle),
                JobTitle = jobTitle
            };

            DataAccess.RemoveJob(jobToRemove);

            Assert.True(result);
        }

        [Fact]
        public void JobExists_ReturnTrueIfExists()
        {
            Job jobObjectToTestIfExists = new Job()
            {
                JobTitle = "jobToTestIfExists"
            };

            DataAccess.AddJob(jobObjectToTestIfExists.JobTitle);

            var result = DataAccess.JobExists(jobObjectToTestIfExists);

            DataAccess.RemoveJob(jobObjectToTestIfExists.JobTitle);

            Assert.True(result);
        }

        [Fact]
        public void GetJobIdFromTitle_ReturnJobId()
        {
            Job testGetJobId = new Job()
            {
                JobTitle = "TestGetJobId"
            };

            DataAccess.AddJob(testGetJobId.JobTitle);
            
            testGetJobId.Id = DataAccess.GetJobIdFromTitle(testGetJobId.JobTitle);

            var result = DataAccess.JobRecordExists(testGetJobId);

            DataAccess.RemoveJob(testGetJobId);

            Assert.True(result);
        }

        
    }
}
