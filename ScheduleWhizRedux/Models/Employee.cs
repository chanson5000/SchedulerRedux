using System.Collections.Generic;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.Models
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public string FullInfo => $"{FirstName} {LastName} ({EmailAddress})";
        public string Details => $"{EmailAddress} - {PhoneNumber}";
  
        public List<string> AssignedJobs => new Repository().AssignedJobs.Get(Id);
        public List<string> AvailableJobs => new Repository().AssignedJobs.GetAvailable(Id);

    }
}
