using ScheduleWhizRedux.Models;
using System.Collections.Generic;

namespace ScheduleWhizRedux.Utilities
{
    public class Scheduler
    {
        private readonly List<Employee> _employees;
        private readonly List<AssignedShift> _availableShifts;

        public Scheduler(List<Employee> employees, List<AssignedShift> assignedShifts)
        {
            _employees = employees;
            _availableShifts = assignedShifts;
        }

        public Schedule GenerateSchedule()
        {
            Schedule schedule = new Schedule("Generated Schedule");

            schedule.PopulateSchedule(_employees, _availableShifts);

            // SaveToDisk can return file name.
            schedule.SaveToDisk();

            return schedule;
        }
    }
}
