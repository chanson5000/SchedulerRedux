using System;
using GemBox.Spreadsheet;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScheduleWhizRedux.Repositories;
using DayOfWeek = System.DayOfWeek;

namespace ScheduleWhizRedux
{
    public class Scheduler
    {
        private readonly List<Employee> _employees;
        private List<Job> _jobs;
        private List<AssignedJob> _availableJobs;
        private readonly List<AssignedShift> _availableShifts;
        private readonly Random _random = new Random();

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
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet worksheet = excelFile.Worksheets.Add("Generated Schedule");

            const int columnStart = 1;
            const int rowStart = 3;

            int column = columnStart;
            int row = rowStart;

            foreach (Employee employee in _employees)
            {
                worksheet.Cells[0, row].Value = employee.FullName;
                row++;
            }

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                worksheet.Cells[column, 2].Value = day.ToString();

                while (_availableShifts.Any(x => x.DayOfWeek.Equals(day)))
                {
                    row = rowStart;

                    foreach (Employee employee in _employees)
                    {
                        var cellInfo = PlotShift(day, employee);
                        if (cellInfo != "")
                        {
                            worksheet.Cells[column, row].Value = cellInfo;
                        }

                        row++;
                    }
                }

                column += 2;
            }

            string defaultFileName = "Schedule";

            SaveSpreadsheet(excelFile, defaultFileName);
        }

        private string PlotShift(DayOfWeek day, Employee employee)
        {
            if (_random.Next(0, 3) == 0)
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
            var shiftToPlot = shiftsForDay[_random.Next(0, shiftsForDay.Count)];
            _availableShifts.Remove(shiftToPlot);
            if (shiftToPlot.NumAvailable <= 1) return $"{shiftToPlot.ShiftName} - {shiftToPlot.JobTitle}";
            shiftToPlot.NumAvailable--;
            _availableShifts.Add(shiftToPlot);

            return $"{shiftToPlot.ShiftName} - {shiftToPlot.JobTitle}";
        }

        private void SaveSpreadsheet(ExcelFile spreadsheet, string spreadsheetName)
        {
            if (!System.IO.File.Exists($"{spreadsheetName}.ods"))
            {
                spreadsheet.Save($"{spreadsheetName}.xlsx");
                LaunchSpreadsheet(spreadsheetName);
            }
            else
            {
                int saveCopy = 1;
                while (System.IO.File.Exists($"{spreadsheetName}-{saveCopy}.xlsx"))
                {
                    saveCopy++;
                }
                spreadsheet.Save($"{spreadsheetName}-{saveCopy}.xlsx");
                LaunchSpreadsheet($"{spreadsheetName}-{saveCopy}");
            }

        }

        private void LaunchSpreadsheet(string spreadsheetName)
        {
            System.Diagnostics.Process.Start($"{spreadsheetName}.xlsx");
        }
    }
}
