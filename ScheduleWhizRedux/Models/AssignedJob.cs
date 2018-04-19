using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWhizRedux.Helpers;

namespace ScheduleWhizRedux.Models
{
    public class AssignedJob
    {
        public int EmployeeId { get; set; }
        public int JobId { get; set; }

        public Employee Employee
        {
            get { return DataAccess.GetEmployeeFromId(EmployeeId); }
        }

        public string JobTitle
        {
            get { return DataAccess.GetJobTitleFromId(JobId); }
        }
    }
}
