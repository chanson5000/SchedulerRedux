using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScheduleWhizRedux.Models;


namespace ScheduleWhizRedux.UnitTests
{
    [TestFixture]
    public class ScheduleTests
    {
        private Schedule _schedule;
        private string _fileLocation;

        [SetUp]
        public void SetUp()
        {
            _schedule = new Schedule("Test Schedule");
        }

        [TearDown]
        public void Cleanup()
        {
            System.IO.File.Delete(_fileLocation);
        }

        [Test]
        public void Schedule_WhenObjectCreated_DefaultFileNameIsSet()
        {
            var result = _schedule.RootFileName;

            _fileLocation = _schedule.SavedFileLocation;

            Assert.That(result, Is.EqualTo("Schedule"));
        }
    }
}
