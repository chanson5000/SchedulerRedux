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
        private readonly Random _random = new Random();

        public Scheduler(List<Employee> employees, List<AssignedShift> assignedShifts)
        {
            _employees = employees;
            _availableShifts = assignedShifts;
        }

        public string Generate()
        {
            // Need this to use GemBox free version.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            const string workSheetName = "Generated Schedule";
            string defaultFileName = "Schedule";
            // Some other possible options are:
            // xlsx, ods, csv, html, pdf, png
            const string fileType = "xlsx";

            ExcelFile excelFile = new ExcelFile();
            ExcelWorksheet worksheet = excelFile.Worksheets.Add(workSheetName);

            // Neither of these should ever be set to < 1;
            const int dataColumnStart = 1;
            const int dataRowStart = 2;

            int column = dataColumnStart;
            int row = dataRowStart;

            // Populating the employee names on y axis.
            foreach (Employee employee in _employees)
            {
                worksheet.Cells[row, dataColumnStart - 1].Value = employee.FullName;
                row++;
            }

            // Populate days of the weed on the x axis.
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                worksheet.Cells[dataRowStart - 1, column].Value = day.ToString();
                column++;
            }

            int maxAttempts = 5;

            // While there are any available shifts.

            while (_availableShifts.Any() && maxAttempts >= 0)
            {
                column = dataColumnStart;
                if (!maxAttempts.Equals(0))
                {
                    // Populating the spreadsheet by day.
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        row = dataRowStart;
                        foreach (Employee employee in _employees)
                        {
                            // If the cell is empty, the employee has job titles, and there are any available shifts for the day.
                            if (worksheet.Cells[row, column].Value == null
                                && employee.AvailableJobs.Any()
                                && _availableShifts.Any(x => x.DayOfWeek.Equals(day)))
                            {
                                // Ask for a shift to plot for the employee on that day.
                                var cellInfo = PlotShift(day, employee);

                                // If we recieve a shift to plot.
                                if (cellInfo != "")
                                {
                                    worksheet.Cells[row, column].Value = cellInfo;
                                }
                            }

                            row++;
                        }

                        column++;
                    }

                    maxAttempts--;
                }

                // Too many attempts, find which shifts we were not able to plot.
                else
                {
                    worksheet.Cells[dataRowStart + _employees.Count() + 1, dataColumnStart - 1]
                        .SetValue("Unable to Schedule:");

                    column = dataColumnStart;

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        // If there are shifts that haven't been plotted for that day of the week.
                        if (_availableShifts.Exists(x => x.DayOfWeek.Equals(day)))
                        {
                            // Plot vertically un-plotted shifts for that day.
                            foreach (var shift in _availableShifts.Where(x => x.DayOfWeek.Equals(day)))
                            {
                                worksheet.Cells[dataRowStart + _employees.Count() + 1, column]
                                    .SetValue($"{shift.ShiftName} - {shift.JobTitle}");
                                row++;
                            }
                        }

                        column++;
                    }

                    maxAttempts--;
                }

            }

            // Format our worksheet.
            AutoFitWorksheet(worksheet);

            return SaveSpreadsheet(excelFile, defaultFileName, fileType);
        }


        private string PlotShift(DayOfWeek day, Employee employee)
        {
            List<string> empAvailableJobs = employee.AvailableJobs;

            // Randomly return no shift to plot in order for plotting to not be the same every time.
            if (!empAvailableJobs.Any() || _random.Next(0, 2) == 0)
            {
                return "";
            }

            // Find all the shifts available to plot for the employee.
            List<AssignedShift> empAvailableShiftsForDay =
                                _availableShifts.Where(x => x.DayOfWeek.Equals(day)
                                && empAvailableJobs.Contains(x.JobTitle)).ToList();

            // Return nothing if no shifts to plot.
            if (!empAvailableShiftsForDay.Any()) return "";

            // Randomly pick a shift to plot from the employee's available shifts.
            AssignedShift shiftToPlot = empAvailableShiftsForDay[_random.Next(0, empAvailableShiftsForDay.Count)];

            AssignedShift shiftRecord = _availableShifts.First(x => x.Equals(shiftToPlot));

            if (shiftRecord.NumAvailable > 1)
            {
                shiftRecord.DecrementAvailability();
            }
            else
            {
                _availableShifts.Remove(shiftRecord);
            }

            // Return the shift information for plotting.
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

        public void LaunchSpreadsheet(string spreadsheetName)
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
