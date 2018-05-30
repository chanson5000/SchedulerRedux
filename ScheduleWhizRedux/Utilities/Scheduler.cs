using System;
using GemBox.Spreadsheet;
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
        private readonly List<AssignedShift> _availableShifts;
        private readonly Random _random = new Random();

        public Scheduler(List<Employee> employees, List<AssignedShift> assignedShifts)
        {
            _employees = employees;
            _availableShifts = assignedShifts;
        }

        public void Generate()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            const string workSheetName = "Generated Schedule";
            const string fileType = "xlsx";

            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet worksheet = excelFile.Worksheets.Add(workSheetName);

            const int dataColumnStart = 1;
            const int dataRowStart = 2;

            int column = dataColumnStart;
            int row = dataRowStart;

            foreach (Employee employee in _employees)
            {
                worksheet.Cells[row, 0].Value = employee.FullName;
                row++;
            }

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                worksheet.Cells[dataRowStart - 1, column].Value = day.ToString();

                while (_availableShifts.Any(x => x.DayOfWeek.Equals(day)))
                {
                    row = dataRowStart;

                    foreach (Employee employee in _employees)
                    {
                        var cellInfo = PlotShift(day, employee);
                        if (cellInfo != "")
                        {
                            worksheet.Cells[row, column].Value = cellInfo;
                        }

                        row++;
                    }
                }

                column++;
            }

            string defaultFileName = "Schedule";

            AutoFitWorksheet(worksheet);

            string spreadsheetFileName = SaveSpreadsheet(excelFile, defaultFileName, fileType);

            LaunchSpreadsheet(spreadsheetFileName);
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

        private string SaveSpreadsheet(ExcelFile spreadsheet, string spreadsheetName, string fileType)
        {
            if (!File.Exists($"{spreadsheetName}.{fileType}"))
            {
                spreadsheet.Save($"{spreadsheetName}.{fileType}");

                return $"{spreadsheetName}.{fileType}";
            }

            int saveCopy = 1;
            while (File.Exists($"{spreadsheetName}-{saveCopy}.{fileType}"))
            {
                saveCopy++;
            }
            spreadsheet.Save($"{spreadsheetName}-{saveCopy}.{fileType}");

            return $"{spreadsheetName}-{saveCopy}.{fileType}";
        }

        private void LaunchSpreadsheet(string spreadsheetName)
        {
            System.Diagnostics.Process.Start(spreadsheetName);
        }

        private void AutoFitWorksheet(ExcelWorksheet worksheet)
        {
            int columnCount = worksheet.CalculateMaxUsedColumns();

            for (int i = 0; i < columnCount; i++)
            {
                worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
            }
        }
    }
}
