using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Repositories
{
    internal class AssignedShiftRepository : Repository, IAssignedShiftRepository
    {
        /// <summary>
        /// Add a new shift for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <param name="numAvailable"></param>
        /// <returns>Return true if successful.</returns>
        public bool Add(DayOfWeek day, string jobTitle, string shiftName, int numAvailable = 0)
        {
            var jobId = Jobs.GetId(jobTitle);
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query =
                    "insert into AssignedShifts (DayOfWeek, JobId, ShiftName, NumAvailable) values (@DayOfWeek, @JobId, @ShiftName, @NumAvailable);";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName,
                        NumAvailable = numAvailable
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a shift for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <returns>True if successful.</returns>
        public bool Remove(DayOfWeek day, string jobTitle, string shiftName)
        {
            var jobId = Jobs.GetId(jobTitle);
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query =
                    "delete from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Get available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <returns>A list of strings.</returns>
        public List<string> GetAvailable(DayOfWeek day, string jobTitle)
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                var jobId = Jobs.GetId(jobTitle);

                var query = "select ShiftName from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId;";

                var result = connection.Query<string>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId
                    }).ToList();

                return result;
            }
        }

        /// <summary>
        /// Get the number of available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <returns>Returns an integer.</returns>
        public int GetNumAvailable(DayOfWeek day, string jobTitle, string shiftName)
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                var jobId = Jobs.GetId(jobTitle);

                var query =
                    "select NumAvailable from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.QueryFirstOrDefault<int>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result;
            }
        }

        /// <summary>
        /// Sets the number of available shifts for a job on a day of the week.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobTitle"></param>
        /// <param name="shiftName"></param>
        /// <param name="numShiftsAvailable"></param>
        /// <returns>Returns true if successful.</returns>
        public bool SetNumAvailable(DayOfWeek day, string jobTitle, string shiftName, int numShiftsAvailable)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var jobId = Jobs.GetId(jobTitle);

                var query =
                    "update AssignedShifts set NumAvailable = @NumAvailable where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.Execute(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName,
                        NumAvailable = numShiftsAvailable
                    });

                return result != 0;
            }
        }

        //-- This may not be needed. --//
        /// <summary>
        /// Get an AssignedShift object record.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="jobId"></param>
        /// <param name="shiftName"></param>
        /// <returns>Returns an AssignedShift object.</returns>
        public AssignedShift GetAssignedShiftInfo(DayOfWeek day, int jobId, string shiftName)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query =
                    "select * from AssignedShifts where DayOfWeek = @DayOfWeek and JobId = @JobId and ShiftName = @ShiftName;";

                var result = connection.QueryFirstOrDefault<AssignedShift>(query,
                    new
                    {
                        DayOfWeek = day,
                        JobId = jobId,
                        ShiftName = shiftName
                    });

                return result;
            }
        }
    }
}
