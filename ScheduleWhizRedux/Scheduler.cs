using System;
using Independentsoft.Office.Odf;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Independentsoft.Office.Odf.Styles;
using ScheduleWhizRedux.Repositories;
using DayOfWeek = System.DayOfWeek;

namespace ScheduleWhizRedux
{
    public class Scheduler
    {
        private List<Employee> _employees;
        private List<Job> _jobs;
        private List<AssignedJob> _availableJobs;
        private List<AssignedShift> _availableShifts;
        Random random = new Random();

        public Scheduler(List<Employee> employees, List<Job> jobs, List<AssignedJob> assignedJobs,
            List<AssignedShift> assignedShifts)
        {
            _employees = employees;
            _jobs = jobs;
            _availableJobs = assignedJobs;
            _availableShifts = assignedShifts;
        }

        public void Generate()
        {
            Table scheduleTable = new Table();

            //table1[1, 1] = new Cell(1);
            //table1[2, 1] = new Cell(2);
            //table1[3, 1] = new Cell(3);
            //table1[4, 1] = new Cell(4);
            //table1[5, 1] = new Cell(5);

            //table1["A", 2] = new Cell(11);
            //table1["B", 2] = new Cell(22);
            //table1["C", 2] = new Cell(33);
            //table1["D", 2] = new Cell(44);
            //table1["E", 2] = new Cell(55);

            //table1["A3"] = new Cell(111);
            //table1["B3"] = new Cell(222);
            //table1["C3"] = new Cell(333);
            //table1["D3"] = new Cell(444);
            //table1["E3"] = new Cell(555);

            //Spreadsheet spreadsheet = new Spreadsheet();
            //spreadsheet.Tables.Add(table1);

            //spreadsheet.Save("c:\\test\\output.ods", true);

            scheduleTable[2, 1] = new Cell("Generated Schedule");
            const int ColumnStart = 3;
            const int RowStart = 3;

            int column = ColumnStart;
            int row = RowStart;

            foreach (Employee employee in _employees)
            {
                scheduleTable[1, row] = new Cell(employee.FullName);
                row++;
            }


            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {

                scheduleTable[column, 2] = new Cell(day.ToString());
                while (_availableShifts.Any(x => x.DayOfWeek.Equals(day)))
                {
                    row = RowStart;

                    foreach (Employee employee in _employees)
                    {
                        var cellInfo = PlotShift(day, employee);
                        if (cellInfo != "")
                        {
                            scheduleTable[column, row] = new Cell(cellInfo);
                        }

                        row++;
                    }
                }

                column += 2;
            }

            Spreadsheet spreadsheet = new Spreadsheet();

            spreadsheet.Tables.Add(scheduleTable);
            string defaultFileName = "Schedule";

            if (!System.IO.File.Exists($"{defaultFileName}.ods"))
            {
                spreadsheet.Save($"{defaultFileName}.ods", false);
                System.Diagnostics.Process.Start($"{defaultFileName}.ods");
            }
            else
            {
                int saveCopy = 1;
                while (System.IO.File.Exists($"{defaultFileName}-{saveCopy}.ods"))
                {
                    saveCopy++;
                }
                    spreadsheet.Save($"{defaultFileName}-{saveCopy}.ods", false);
                    System.Diagnostics.Process.Start($"{defaultFileName}-{saveCopy}.ods");
            }
        }


        public string PlotShift(DayOfWeek day, Employee employee)
        {
            if (random.Next(0, 3) == 0)
            {
                return "";
            }

            var shiftsForDay = _availableShifts.FindAll(x => x.DayOfWeek.Equals(day));

            int jobId;
            foreach (var job in employee.AvailableJobs)
            {
                jobId = new AssignedJobRepository().Jobs.GetId(job);
                if (shiftsForDay.Any(x => x.JobId == jobId))
                {
                    shiftsForDay.RemoveAll(x => x.JobId.Equals(jobId));
                }
            }

            if (!shiftsForDay.Any()) return "";
            var shiftToPlot = shiftsForDay[random.Next(0, shiftsForDay.Count)];
            _availableShifts.Remove(shiftToPlot);
            if (shiftToPlot.NumAvailable <= 1) return $"{shiftToPlot.ShiftName} - {shiftToPlot.JobTitle}";
            shiftToPlot.NumAvailable--;
            _availableShifts.Add(shiftToPlot);

            return $"{shiftToPlot.ShiftName} - {shiftToPlot.JobTitle}";

        }
    }
}
