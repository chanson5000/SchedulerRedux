using System;
using System.Collections.Generic;

namespace ScheduleWhizRedux.Interfaces
{
    public interface IAssignedShiftRepository
    {
        bool Add(DayOfWeek day, string jobTitle, string shiftName, int numAvailable);
        bool Remove(DayOfWeek day, string jobTitle, string shiftName);
        List<string> GetAvailable(DayOfWeek day, string jobTitle);
        int GetNumAvailable(DayOfWeek day, string jobTitle, string shiftName);
        bool SetNumAvailable(DayOfWeek day, string jobTitle, string shiftName, int numShiftsAvailable);
    }
}
