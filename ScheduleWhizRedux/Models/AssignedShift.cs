using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Models
{
    public class AssignedShift : IShift, IJob, INumAvailable
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string ShiftName { get; set; }
        public int NumAvailable { get; set; }
        public int JobId { get; set; }

        public string JobTitle
        {
            get => DataAccess.GetJobFromId(JobId).JobTitle;
            set { }
        }
    }
}
