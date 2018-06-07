using System.Collections.Generic;
using NUnit.Framework;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Utilities;

namespace ScheduleWhizRedux.UnitTests
{
    [TestFixture]
    public class SchedulerTests
    {
        private Scheduler _scheduler;
        private string _fileLocation;

        [SetUp]
        public void Init()
        {
            _scheduler = new Scheduler(new List<Employee>(), new List<AssignedShift>());
        }

        [Test]
        public void GenerateSchedule_WhenCalled_ReturnScheduleObject()
        {
            var result = _scheduler.GenerateSchedule();

            _fileLocation = result.SavedFileLocation;

            Assert.That(result, Is.TypeOf<Schedule>());
        }

        [Test]
        public void GenerateSchedule_WhenCalled_ScheduleObjectContainsSchedule()
        {

        }

        [Test]
        public void GenerateSchedule_WhenCalled_FileIsCreated()
        {
            var scheduleObject = _scheduler.GenerateSchedule();

            _fileLocation = scheduleObject.SavedFileLocation;

            var result = System.IO.File.Exists(_fileLocation);

            Assert.That(result, Is.True);
        }

        [TearDown]
        public void Cleanup()
        {
            System.IO.File.Delete(_fileLocation);
        }
    }
}
