using System;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.Models
{
    public class AssignedShift : IShift, IJob
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string ShiftName { get; set; }
        public int NumAvailable { get; set; }
        public int JobId { get; set; }

        public string JobTitle
        {
            get => new Repository().Jobs.Get(JobId).JobTitle;
            set { }
        }
    }
}
