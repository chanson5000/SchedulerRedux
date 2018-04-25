using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWhizRedux.Helpers;

namespace ScheduleWhizRedux.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullInfo => $"{FirstName} {LastName} ({EmailAddress})";

        public string Details => $"{EmailAddress} - {PhoneNumber}";

        public List<string> AssignedJobs => DataAccess.GetEmployeeAssignedJobs(Id);

        public List<string> AvailableJobs => DataAccess.GetEmployeeAvailableJobs(Id);
    }
}
