using System;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.ViewModels
{
    internal class AddShiftViewModel : Screen
    {
        private readonly IAssignedShiftRepository _assignedShifts;

        public AddShiftViewModel()
        {
            _assignedShifts = new AssignedShiftRepository();
        }

        public string NewShift { get; set; }

        public string Job { get; set; }

        public DayOfWeek Day { get; set; }

        public int NumAvailable { get; set; }

        public void AddShift()
        {
            if (string.IsNullOrWhiteSpace(NewShift))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (_assignedShifts.Add(Day, Job, NewShift.Trim(), NumAvailable))
            {
                MessageBox.Show($"The shift, {NewShift}, was added to the database.", "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NewShift = "";
                NumAvailable = 0;
                TryClose(true);
            }
            else
            {
                MessageBox.Show($"Unable to add the job, {NewShift}, to the database.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
