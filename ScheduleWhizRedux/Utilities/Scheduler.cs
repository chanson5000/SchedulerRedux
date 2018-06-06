using System;
using GemBox.Spreadsheet;
using ScheduleWhizRedux.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DayOfWeek = System.DayOfWeek;

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

        public Schedule Generate()
        {
            Schedule schedule = new Schedule("Generated Schedule");

            schedule.PopulateEmployeeNames(_employees);
            schedule.PopulateDaysOfWeek();
            schedule.PopulateSchedule(_employees, _availableShifts);
            
            // Format our worksheet.
            schedule.AutoFormat();

            // May use this for future use.
            string scheduleSavedAs = schedule.SaveToDisk();

            return schedule;
        }
    }
}
