using System;
using System.Collections.Generic;
using GemBox.Spreadsheet;
using NUnit.Framework;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.UnitTests
{
    [TestFixture]
    public class ScheduleTests
    {
        private Schedule _schedule;

        [SetUp]
        public void Init()
        {
            _schedule = new Schedule("Test Schedule");
        }

        [Test]
        public void Schedule_WhenObjectCreated_DefaultFileNameIsSet()
        {
            var result = _schedule.RootFileName;

            Assert.That(result, Is.EqualTo("Schedule"));
        }

        [Test]
        public void PopulateSchedule_HasEmployeeListInput_WorksheetHasEmployee()
        {
            Employee employee = new Employee()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            _schedule.PopulateSchedule(employeeList, new List<AssignedShift>());

            var result = _schedule.Worksheet.Cells[2, 0].StringValue;

            Assert.That(result, Is.EqualTo($"{employee.FullName}"));
        }

        [Test]
        [Ignore("Fix calling of new instance of JobRepository")]
        public void PopulateSchedule_HasValidAssignedShiftInput_WorksheetHasShift()
        {
            Employee employee = new Employee()
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            AssignedShift assignedShift = new AssignedShift()
            {
                DayOfWeek = DayOfWeek.Sunday,
                ShiftName = "TestShift",
                NumAvailable = 1,
                JobId = 1,
                JobTitle = "TestJob"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            List<AssignedShift> assignedShiftList = new List<AssignedShift>()
            {
                assignedShift
            };

            _schedule.PopulateSchedule(employeeList, assignedShiftList);

            var result = _schedule.Worksheet.Cells[3, 1].StringValue;

            Assert.That(result, Is.Not.EqualTo($"{assignedShift.ShiftName} - {assignedShift.JobTitle}"));
        }

        // TODO: Possibly test for a range here.
        [Test]
        public void PopulateSchedule_WhenCalled_HasDaysOfTheWeek()
        {
            _schedule.PopulateSchedule(new List<Employee>(), new List<AssignedShift>());

            var result = _schedule.Worksheet.Cells[1, 1].StringValue;

            Assert.That(result, Is.EqualTo("Sunday"));
        }

        // TODO: Add checks for when relevant employee and shifts are provided.

        // -------
        [Test]
        public void FileType_WhenScheduleInstanciated_FileTypeIsDefault()
        {
            var result = _schedule.FileType;

            Assert.That(result, Is.EqualTo("xlsx"));
        }

        [Test]
        public void FileType_WhenSetInvalidFileType_FileTypeIsDefault()
        {
            _schedule.FileType = "Invalid File Type";

            var result = _schedule.FileType;

            Assert.That(result, Is.EqualTo("xlsx"));
        }

        [Test]
        [TestCase("xlsx")]
        [TestCase("ods")]
        [TestCase("csv")]
        [TestCase("html")]
        [TestCase("pdf")]
        [TestCase("png")]
        public void FileType_WhenSetValieFileType_FileTypeIsCorrect(string setFileType)
        {
            var result = _schedule.FileType = setFileType;

            Assert.That(result, Is.EqualTo(setFileType));
        }

        [Test]
        public void SavedFileLocation_WhenCalled_SavedFileLocationEqualsSaveToDiskReturnValue()
        {
            Employee employee = new Employee()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            _schedule.PopulateSchedule(employeeList, new List<AssignedShift>());

            var saveToDiskReturnValue = _schedule.SaveToDisk();

            var result = _schedule.SavedFileLocation;

            System.IO.File.Delete(saveToDiskReturnValue);

            Assert.That(result, Is.EqualTo(saveToDiskReturnValue));
        }

        [Test]
        public void SaveToDisk_WhenCalled_FileExistsAtReturnValueLocation()
        {
            Employee employee = new Employee()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            _schedule.PopulateSchedule(employeeList, new List<AssignedShift>());

            var fileLocation = _schedule.SaveToDisk();

            var result = System.IO.File.Exists(fileLocation);

            System.IO.File.Delete(fileLocation);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveToDisk_WhenFileExists_SaveToDifferentFileName()
        {
            Employee employee = new Employee()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            _schedule.PopulateSchedule(employeeList, new List<AssignedShift>());

            var firstFileLocation = _schedule.SaveToDisk();

            var secondFileLocation = _schedule.SaveToDisk();

            System.IO.File.Delete(firstFileLocation);
            System.IO.File.Delete(secondFileLocation);

            Assert.That(firstFileLocation, Is.Not.EqualTo(secondFileLocation));
        }

        [Test]
        public void SaveToDisk_WhenMoreThanOneFileExists_SaveToDifferentFileName()
        {
            Employee employee = new Employee()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            List<Employee> employeeList = new List<Employee>()
            {
                employee
            };

            _schedule.PopulateSchedule(employeeList, new List<AssignedShift>());

            var firstFileLocation = _schedule.SaveToDisk();

            var secondFileLocation = _schedule.SaveToDisk();

            var thirdFileLocation = _schedule.SaveToDisk();

            System.IO.File.Delete(firstFileLocation);
            System.IO.File.Delete(secondFileLocation);
            System.IO.File.Delete(thirdFileLocation);

            Assert.That(secondFileLocation, Is.Not.EqualTo(thirdFileLocation));
        }

        [Test]
        public void RootFileName_WhenCalled_IsRootFileName()
        {
            var result = _schedule.RootFileName;

            Assert.That(result, Is.EqualTo("Schedule"));
        }

        [Test]
        public void Worksheet_WhenCalled_WorksheetIsExcelWorksheetObject()
        {
            var result = _schedule.Worksheet;

            Assert.That(result, Is.TypeOf<ExcelWorksheet>());
        }

        [Test]
        public void SpreadsheetName_WhenCalled_IsEqualToArgumentValue()
        {
            var result = _schedule.SpreadsheetName;

            Assert.That(result, Is.EqualTo("Test Schedule"));
        }
    }
}
