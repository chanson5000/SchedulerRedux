using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;
using Xunit;
using Xunit.Sdk;

namespace SWReduxUnitTests_1
{


    public class EmployeeRepositoryUnitTests
    {
        private readonly IEmployeeRepository _employees;

        public EmployeeRepositoryUnitTests()
        {
            _employees = new EmployeeRepository();
        }

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

            bool resultBoolEmployeeAdded = _employees.Add(tAddEmployeeRecord);

            // Record was added.
            Assert.True(resultBoolEmployeeAdded);

            // Cleanup.
            tAddEmployeeRecord.Id = _employees.GetId(tAddEmployeeRecord);
            _employees.Remove(tAddEmployeeRecord);
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

            _employees.Add(tExistsEmployeeRecord);

            bool resultBoolEmployeeExists = _employees.Exists(tExistsEmployeeRecord);

            // Record exists.
            Assert.True(resultBoolEmployeeExists);

            // Cleanup.
            tExistsEmployeeRecord.Id = _employees.GetId(tExistsEmployeeRecord);
            _employees.Remove(tExistsEmployeeRecord);
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

            _employees.Add(tGetIdEmployeeRecord);

            int resultIntEmployeeId = _employees.GetId(tGetIdEmployeeRecord);

            // Returns an employee Id.
            Assert.NotEqual(0, resultIntEmployeeId);

            // Cleanup
            tGetIdEmployeeRecord.Id = resultIntEmployeeId;
            _employees.Remove(tGetIdEmployeeRecord);
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

            _employees.Add(tGetEmployeeObjectRecord);

            int resultIntEmployeeId = _employees.GetId(tGetEmployeeObjectRecord);

            Employee resultObjectEmployee = _employees.Get(resultIntEmployeeId);

            // Returned an Employee object.
            Assert.IsType<Employee>(resultObjectEmployee);

            //Cleanup
            tGetEmployeeObjectRecord.Id = resultIntEmployeeId;
            _employees.Remove(tGetEmployeeObjectRecord);
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

            _employees.Add(tGetAllEmployee1);
            _employees.Add(tGetAllEmployee2);
            _employees.Add(tGetAllEmployee3);

            List<Employee> resultSortedList = _employees.GetAllSorted();

            // Returned a list
            Assert.IsType<List<Employee>>(resultSortedList);

            // Cleanup
            foreach (var employee in resultSortedList)
            {
                _employees.Remove(employee);
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

            _employees.Add(tModifyEmployeeStart);

            tModifyEmployeeEnd.Id = _employees.GetId(tModifyEmployeeStart);

            bool resultTrueIfModified = _employees.Modify(tModifyEmployeeEnd);

            // Returned true.
            Assert.True(resultTrueIfModified);

            // Cleanup.
            _employees.Remove(tModifyEmployeeEnd);
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

            _employees.Add(tEmployeeRemoveRecord);

            tEmployeeRemoveRecord.Id = _employees.GetId(tEmployeeRemoveRecord);

            bool resultReturnTrue = _employees.Remove(tEmployeeRemoveRecord);

            // Returned true.
            Assert.True(resultReturnTrue);

            // Cleanup.
            _employees.Remove(tEmployeeRemoveRecord);

        }
    }
    
}
