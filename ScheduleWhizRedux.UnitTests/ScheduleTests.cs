using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScheduleWhizRedux.Models;
using GemBox.Spreadsheet;


namespace ScheduleWhizRedux.UnitTests
{
    [TestFixture]
    public class ScheduleTests
    {
        private Schedule _schedule;

        [SetUp]
        public void SetUp()
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

            Assert.That(result, Is.EqualTo($"{employee.FirstName} {employee.LastName}"));
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
    }
}
