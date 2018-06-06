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

        [SetUp]
        public void SetUp()
        {
            _scheduler = new Scheduler(new List<Employee>(), new List<AssignedShift>());
        }

        [Test]
        public void Generate_WhenCalled_ReturnScheduleObject()
        {
            var result = _scheduler.Generate();

            Assert.That(result, Is.TypeOf<Schedule>());
        }
    }
}
