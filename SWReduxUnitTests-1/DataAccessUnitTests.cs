using System;
using System.Collections.Generic;
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
    
}
