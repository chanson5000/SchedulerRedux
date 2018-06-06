using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GemBox.Spreadsheet;

namespace ScheduleWhizRedux.Models
{
    public class Schedule
    {
        private readonly string _spreadsheetName;
        private readonly string _rootFileName;
        private string _savedFileLocation;
        private readonly ExcelWorksheet _worksheet;
        private readonly ExcelFile _excelFile;
        private readonly Random _random = new Random();
        private List<AssignedShift> _availableShifts;
        // Some other possible options are:
        // xlsx, ods, csv, html, pdf, png
        private string _fileType = "xlsx";
        // Neither of these should ever be set to < 1;
        // They help with formatting data placement.
        private const int DataColumnStart = 1;
        private const int DataRowStart = 2;

        public Schedule(string spreadsheetName)
        {
            // License for the GemBox free version.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            _excelFile = new ExcelFile();

            _rootFileName = "Schedule";

            _spreadsheetName = spreadsheetName;

            _worksheet = _excelFile.Worksheets.Add(_spreadsheetName);
        }

        public string RootFileName => _rootFileName;
        public string SavedFileLocation => _savedFileLocation;

        // Populate the days of the week on the x axis.
        private void PopulateDaysOfWeek()
        {
            int column = DataColumnStart;

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                _worksheet.Cells[DataRowStart - 1, column].Value = day.ToString();
                column++;
            }
        }

        // Populate the employee names on the y axis.
        private void PopulateEmployeeNames(List<Employee> employees)
        {
            int row = DataRowStart;

            foreach (Employee employee in employees)
            {
                _worksheet.Cells[row, DataColumnStart - 1].Value = employee.FullName;
                row++;
            }
        }

        private void PopulateShifts(List<Employee> employees, List<AssignedShift> shifts)
        {
            _availableShifts = shifts;
            int maxAttempts = 5;
            int row = DataRowStart;

            // While there are any available shifts and we havent reached our attemps limit.
            while (_availableShifts.Any() && maxAttempts >= 0)
            {
                int column = DataColumnStart;
                if (!maxAttempts.Equals(0))
                {
                    // Populating the spreadsheet by day.
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        row = DataRowStart;
                        foreach (Employee employee in employees)
                        {
                            // If the cell is empty, the employee has job titles, and there are any available shifts for the day.
                            if (_worksheet.Cells[row, column].Value == null
                                && employee.AvailableJobs.Any()
                                && _availableShifts.Any(x => x.DayOfWeek.Equals(day)))
                            {
                                // Ask for a shift to plot for the employee on that day.
                                var cellInfo = PlotShift(day, employee);

                                // If we recieve a shift to plot.
                                if (cellInfo != "")
                                {
                                    _worksheet.Cells[row, column].Value = cellInfo;
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
                    _worksheet.Cells[DataRowStart + employees.Count() + 1, DataColumnStart - 1]
                         .SetValue("Unable to Schedule:");

                    column = DataColumnStart;

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        // If there are shifts that haven't been plotted for that day of the week.
                        if (shifts.Exists(x => x.DayOfWeek.Equals(day)))
                        {
                            // Plot vertically un-plotted shifts for that day.
                            foreach (var shift in shifts.Where(x => x.DayOfWeek.Equals(day)))
                            {
                                _worksheet.Cells[DataRowStart + employees.Count() + 1, column]
                                    .SetValue($"{shift.ShiftName} - {shift.JobTitle}");
                                row++;
                            }
                        }

                        column++;
                    }

                    maxAttempts--;
                }
            }
        }

        private void AutoFormat()
        {
            int columnCount = _worksheet.CalculateMaxUsedColumns();

            for (int i = 0; i < columnCount; i++)
            {
                _worksheet.Columns[i].AutoFit(1, _worksheet.Rows[1], _worksheet.Rows[_worksheet.Rows.Count - 1]);
            }
        }

        public void PopulateSchedule(List<Employee> employees, List<AssignedShift> shifts)
        {
            PopulateDaysOfWeek();
            PopulateEmployeeNames(employees);
            PopulateShifts(employees, shifts);
            AutoFormat();
        }

        private string PlotShift(DayOfWeek day, Employee employee)
        {
            List<string> empAvailableJobs = employee.AvailableJobs;

            // Randomly return no shift to plot in order for plotting to not be the same every time or
            // if there are no Jobs available.
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

        public string SaveToDisk()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!File.Exists($"{userFolder}\\{_rootFileName}.{_fileType}"))
            {
                _savedFileLocation = $"{userFolder}\\{_rootFileName}.{_fileType}";
                _excelFile.Save(_savedFileLocation);
                
                return _savedFileLocation;
            }

            int saveCopy = 1;
            while (File.Exists($"{userFolder}\\{_rootFileName}-{saveCopy}.{_fileType}"))
            {
                saveCopy++;
            }
            _savedFileLocation = $"{userFolder}\\{_rootFileName}-{saveCopy}.{_fileType}";
            _excelFile.Save(_savedFileLocation);

            return _savedFileLocation;
        }
        
        public void OpenFile()
        {
            System.Diagnostics.Process.Start(_savedFileLocation);
        }
    }
}
