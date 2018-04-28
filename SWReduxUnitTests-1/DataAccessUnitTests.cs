using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;
using Xunit;
using Xunit.Sdk;

namespace SWReduxUnitTests_1
{


    public class EmployeeRepositoryUnitTests
    {
        private readonly Repository _repository = new Repository();

        [Fact]
        public void EmployeeAdd_ReturnTrueIfSuccessful()
        {

            Employee tAddEmployeeRecord = new Employee()
            {
                FirstName = "tAddEmpRepFirstName",
                LastName = "tAddEmpRepLastName",
                EmailAddress = "tAddEmpRepEmail",
                PhoneNumber = "tAddEmpRepPhone",
            };

            bool resultBoolEmployeeAdded = _repository.Employees.Add(tAddEmployeeRecord);

            // Record was added.
            Assert.True(resultBoolEmployeeAdded);

            // Cleanup.
            tAddEmployeeRecord.Id = _repository.Employees.GetId(tAddEmployeeRecord);
            _repository.Employees.Remove(tAddEmployeeRecord);
        }

        [Fact]
        public void EmployeeExists_ReturnTrueIfExists()
        {
            Employee tExistsEmployeeRecord = new Employee()
            {
                FirstName = "tExistsFirstName",
                LastName = "tExistsLastName",
                EmailAddress = "tExistsEmail",
                PhoneNumber = "tExistsPhone"
            };

            _repository.Employees.Add(tExistsEmployeeRecord);

            bool resultBoolEmployeeExists = _repository.Employees.Exists(tExistsEmployeeRecord);

            // Record exists.
            Assert.True(resultBoolEmployeeExists);

            // Cleanup.
            tExistsEmployeeRecord.Id = _repository.Employees.GetId(tExistsEmployeeRecord);
            _repository.Employees.Remove(tExistsEmployeeRecord);
        }

        [Fact]
        public void EmployeeGetId_ReturnEmployeeId()
        {
            Employee tGetIdEmployeeRecord = new Employee()
            {
                FirstName = "tGetIdFirstName",
                LastName = "tGetIdLastName",
                EmailAddress = "tGetIdEmail",
                PhoneNumber = "tGetIdPhone",
            };

            _repository.Employees.Add(tGetIdEmployeeRecord);

            int resultIntEmployeeId = _repository.Employees.GetId(tGetIdEmployeeRecord);

            // Returns an employee Id.
            Assert.NotEqual(0, resultIntEmployeeId);

            // Cleanup
            tGetIdEmployeeRecord.Id = resultIntEmployeeId;
            _repository.Employees.Remove(tGetIdEmployeeRecord);
        }

        [Fact]
        public void EmployeeGet_ReturnsEmployeeObject()
        {
            Employee tGetEmployeeObjectRecord = new Employee()
            {
                FirstName = "tGetEmpObjectFirst",
                LastName = "tGetEmpObjectLast",
                EmailAddress = "tGetEmpObjectEmail",
                PhoneNumber = "tGetEmpObjectPhone"
            };

            _repository.Employees.Add(tGetEmployeeObjectRecord);

            int resultIntEmployeeId = _repository.Employees.GetId(tGetEmployeeObjectRecord);

            Employee resultObjectEmployee = _repository.Employees.Get(resultIntEmployeeId);

            // Returned an Employee object.
            Assert.IsType<Employee>(resultObjectEmployee);

            //Cleanup
            tGetEmployeeObjectRecord.Id = resultIntEmployeeId;
            _repository.Employees.Remove(tGetEmployeeObjectRecord);
        }

        [Fact]
        public void EmployeeGetAllSorted_ReturnsASortedListOfEmployees()
        {
            Employee tGetAllEmployee1 = new Employee()
            {
                FirstName = "tGAFirst1",
                LastName = "tGALast1",
                EmailAddress = "tGAEmail1",
                PhoneNumber = "tGAPhone1"
            };
            Employee tGetAllEmployee2 = new Employee()
            {
                FirstName = "tGAFirst2",
                LastName = "tGALast2",
                EmailAddress = "tGAEmail2",
                PhoneNumber = "tGAPhone2"
            };
            Employee tGetAllEmployee3 = new Employee()
            {
                FirstName = "tGAFirst3",
                LastName = "tGALast3",
                EmailAddress = "tGAEmail3",
                PhoneNumber = "tGAPhone3"
            };

            _repository.Employees.Add(tGetAllEmployee1);
            _repository.Employees.Add(tGetAllEmployee2);
            _repository.Employees.Add(tGetAllEmployee3);

            List<Employee> resultSortedList = _repository.Employees.GetAllSorted();

            // Returned a list
            Assert.IsType<List<Employee>>(resultSortedList);

            // Cleanup
            foreach (var employee in resultSortedList)
            {
                _repository.Employees.Remove(employee);
            }
        }

        [Fact]
        public void EmployeeModify_ReturnsModifiedEmployee()
        {
            Employee tModifyEmployeeStart = new Employee()
            {
                FirstName = "tEmpModFirstStart",
                LastName = "tEmpModLastStart",
                EmailAddress = "tEmpModEmailStart",
                PhoneNumber = "tEmpModPhoneStart"
            };

            Employee tModifyEmployeeEnd = new Employee()
            {
                FirstName = "tEmpModFirstEnd",
                LastName = "tEmpModLastEnd",
                EmailAddress = "tEmpModEmailEnd",
                PhoneNumber = "tEmpModPhoneEnd"
            };

            _repository.Employees.Add(tModifyEmployeeStart);

            tModifyEmployeeEnd.Id = _repository.Employees.GetId(tModifyEmployeeStart);

            bool resultTrueIfModified = _repository.Employees.Modify(tModifyEmployeeEnd);

            // Returned true.
            Assert.True(resultTrueIfModified);

            // Cleanup.
            _repository.Employees.Remove(tModifyEmployeeEnd);
        }

        [Fact]
        public void EmployeeRemove_ReturnTrueIfRemoved()
        {
            Employee tEmployeeRemoveRecord = new Employee()
            {
                FirstName = "tEmpRemFirst",
                LastName = "tEmpRemLast",
                EmailAddress = "tEmpRemEmail",
                PhoneNumber = "tEmpRemPhone"
            };

            _repository.Employees.Add(tEmployeeRemoveRecord);

            tEmployeeRemoveRecord.Id = _repository.Employees.GetId(tEmployeeRemoveRecord);

            bool resultReturnTrue = _repository.Employees.Remove(tEmployeeRemoveRecord);

            // Returned true.
            Assert.True(resultReturnTrue);

            // Cleanup.
            _repository.Employees.Remove(tEmployeeRemoveRecord);

        }
    }

    // Do not need these for now.

    //public class DataAccessUnitTests
    //{
    //    [Fact]
    //    public void AddEmployee_ReturnTrueIfSuccessful()
    //    {
    //        Employee employeeTestCase = new Employee()
    //        {
    //            FirstName = "TestAddFirstName",
    //            LastName = "TestAddLastName",
    //            EmailAddress = "TestAddEmailAddress",
    //            PhoneNumber = "TestAddPhoneNumber"
    //        };
    //        var result = DataAccess.AddEmployee(employeeTestCase);
    //        employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);
    //        DataAccess.RemoveEmployee(employeeTestCase);

    //        Assert.True(result);
    //    }

    //    [Fact]
    //    public void GetIdFromEmployee_ReturnEmployeeId()
    //    {
    //        Employee employeeTestCase = new Employee()
    //        {
    //            FirstName = "TestGetIdFirstName",
    //            LastName = "TestGetIdLastName",
    //            EmailAddress = "TestGetIdEmailAddress",
    //            PhoneNumber = "TestGetIdPhoneNumber"
    //        };
    //        DataAccess.AddEmployee(employeeTestCase);
    //        var result = DataAccess.GetIdFromEmployee(employeeTestCase);
    //        employeeTestCase.Id = result;

    //        DataAccess.RemoveEmployee(employeeTestCase);

    //        Assert.NotEqual(0, result);
    //    }

    //    [Fact]
    //    public void GetEmployeeFromId_IdShouldEqualId()
    //    {
    //        Employee employeeTestCase = new Employee()
    //        {
    //            FirstName = "TestGetEmpFirstName",
    //            LastName = "TestGetEmpLastName",
    //            EmailAddress = "TestGetEmpEmailAddress",
    //            PhoneNumber = "TestGetEmpPhoneNumber"
    //        };
    //        DataAccess.AddEmployee(employeeTestCase);
    //        employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);
    //        var result = DataAccess.GetEmployeeFromId(employeeTestCase.Id);

    //        DataAccess.RemoveEmployee(employeeTestCase);

    //        Assert.Equal(employeeTestCase.Id, result.Id);
    //    }

    //    [Fact]
    //    public void RemoveEmployee_ShouldRemove()
    //    {
    //        Employee employeeTestCase = new Employee()
    //        {
    //            FirstName = "TestRemoveEmpFirstName",
    //            LastName = "TestRemoveEmpLastName",
    //            EmailAddress = "TestRemoveEmpEmailAddress",
    //            PhoneNumber = "TestRemoveEmpPhoneNumber"
    //        };
    //        DataAccess.AddEmployee(employeeTestCase);
    //        employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);

    //        var result = DataAccess.RemoveEmployee(employeeTestCase);

    //        Assert.True(result);
    //    }

    //    [Fact]
    //    public void GetAllEmployees_ShouldReturnEmployeeList()
    //    {
    //        var result = DataAccess.GetAllEmployees();

    //        Assert.IsType<List<Employee>>(result);
    //    }

    //    [Fact]
    //    public void ModifyEmployee_ShouldUpdate()
    //    {
    //        Employee employeeTestCase = new Employee()
    //        {
    //            FirstName = "TestModFirstName",
    //            LastName = "TestModLastName",
    //            EmailAddress = "TestModEmailAddress",
    //            PhoneNumber = "TestModPhoneNumber"
    //        };

    //        DataAccess.AddEmployee(employeeTestCase);

    //        employeeTestCase.Id = DataAccess.GetIdFromEmployee(employeeTestCase);

    //        employeeTestCase.FirstName = "TestNewFirstName";
    //        employeeTestCase.LastName = "TestNewLastName";
    //        employeeTestCase.EmailAddress = "TestNewEmailAddress";
    //        employeeTestCase.PhoneNumber = "testNewPhoneNumber";

    //        DataAccess.ModifyEmployee(employeeTestCase);

    //        Employee modifiedFromDatabase = DataAccess.GetEmployeeFromId(employeeTestCase.Id);

    //        DataAccess.RemoveEmployee(employeeTestCase);
            
    //        Assert.Equal(employeeTestCase.Id, employeeTestCase.Id);
    //        Assert.Equal(employeeTestCase.FirstName, modifiedFromDatabase.FirstName);
    //        Assert.Equal(employeeTestCase.LastName, modifiedFromDatabase.LastName);
    //        Assert.Equal(employeeTestCase.EmailAddress, modifiedFromDatabase.EmailAddress);
    //        Assert.Equal(employeeTestCase.PhoneNumber, modifiedFromDatabase.PhoneNumber);
    //    }

    //    [Fact]
    //    public void AddJob_ShouldReturnTrue()
    //    {
    //        Job newTestJob = new Job()
    //        {
    //            JobTitle = "Add Job Test"
    //        };

    //        var result = DataAccess.AddJob(newTestJob.JobTitle);

    //        newTestJob.Id = DataAccess.GetJobIdFromTitle(newTestJob.JobTitle);

    //        List<Job> allJobs = DataAccess.GetAllJobRecords();

    //        DataAccess.RemoveJob(newTestJob);

    //        Assert.True(result);
    //        // TODO: Why does this Assert not work like I think it should.
    //        Assert.DoesNotContain(newTestJob, allJobs);
    //    }

    //    [Fact]
    //    public void AssignJobToEmployee_ShouldReturnTrue()
    //    {
    //        Job newTestJobToAssign = new Job()
    //        {
    //            JobTitle = "AssignJobTest"
    //        };

    //        Employee newTestAssignJobEmployee = new Employee()
    //        {
    //            FirstName = "TAJFirstName",
    //            LastName = "TAJLastName",
    //            EmailAddress = "TAJEmailAddress",
    //            PhoneNumber = "TAJPhoneNumber"
    //        };

    //        DataAccess.AddJob(newTestJobToAssign.JobTitle);
    //        DataAccess.AddEmployee(newTestAssignJobEmployee);

    //        newTestJobToAssign.Id = DataAccess.GetJobIdFromTitle(newTestJobToAssign.JobTitle);

    //        newTestAssignJobEmployee.Id = DataAccess.GetIdFromEmployee(newTestAssignJobEmployee);

    //        var result = DataAccess.AssignJobToEmployee(newTestJobToAssign, newTestAssignJobEmployee);

    //        DataAccess.UnAssignJobFromEmployee(newTestJobToAssign, newTestAssignJobEmployee);
    //        DataAccess.RemoveJob(newTestJobToAssign);
    //        DataAccess.RemoveEmployee(newTestAssignJobEmployee);
            

    //        Assert.True(result);
    //    }

    //    [Fact]
    //    public void AddShiftForJobOnDay_ReturnTrueIfSuccessful()
    //    {
    //        var jobTitle = "TestJobAssignment";
    //        DataAccess.AddJob(jobTitle);
    //        var dayOfWeek = DayOfWeek.Monday;
    //        var shiftToAdd = "TestShiftAssignment";


    //        var result = DataAccess.AddShiftForJobOnDay(dayOfWeek, jobTitle, shiftToAdd);

    //        DataAccess.RemoveShiftForJobOnDay(dayOfWeek, jobTitle, shiftToAdd);

    //        Job jobToRemove = new Job()
    //        {
    //            Id = DataAccess.GetJobIdFromTitle(jobTitle),
    //            JobTitle = jobTitle
    //        };

    //        DataAccess.RemoveJob(jobToRemove);

    //        Assert.True(result);
    //    }

    //    [Fact]
    //    public void JobExists_ReturnTrueIfExists()
    //    {
    //        Job jobObjectToTestIfExists = new Job()
    //        {
    //            JobTitle = "jobToTestIfExists"
    //        };

    //        DataAccess.AddJob(jobObjectToTestIfExists.JobTitle);

    //        var result = DataAccess.JobExists(jobObjectToTestIfExists);

    //        DataAccess.RemoveJob(jobObjectToTestIfExists.JobTitle);

    //        Assert.True(result);
    //    }

    //    [Fact]
    //    public void GetJobIdFromTitle_ReturnJobId()
    //    {
    //        Job testGetJobId = new Job()
    //        {
    //            JobTitle = "TestGetJobId"
    //        };

    //        DataAccess.AddJob(testGetJobId.JobTitle);
            
    //        testGetJobId.Id = DataAccess.GetJobIdFromTitle(testGetJobId.JobTitle);

    //        var result = DataAccess.JobRecordExists(testGetJobId);

    //        DataAccess.RemoveJob(testGetJobId);

    //        Assert.True(result);
    //    }

    //    //[Fact]
    //    //public void GetJobFromId_ShouldReturnJobObject()
    //    //{
    //    //    Job testGetJobObject = new Job
    //    //    {
    //    //        JobTitle = "TestGetJobObject"
    //    //    };
    //    //}

        
    //}
}
