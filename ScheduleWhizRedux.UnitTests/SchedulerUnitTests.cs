using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Utilities;

namespace ScheduleWhizRedux.UnitTests
{
    [TestFixture]
    public class SchedulerUnitTests
    {
        private Scheduler _scheduler;
        private string _fileLocation;

        [SetUp]
        public void SetUp()
        {
            _scheduler = new Scheduler(new List<Employee>(), new List<AssignedShift>());
        }

        [Test]
        public void Generate_WhenCalled_ReturnScheduleObject()
        {
            var result = _scheduler.Generate();

            _fileLocation = result.SavedFileLocation;

            Assert.That(result, Is.TypeOf<Schedule>());
        }

        [TearDown]
        public void Cleanup()
        {
            System.IO.File.Delete(_fileLocation);
        }
    }
}
