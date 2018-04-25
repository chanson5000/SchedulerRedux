using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.ViewModels
{
    public class AddShiftViewModel : Screen
    {
        private DayOfWeek _day;
        private int _numAvailable;
        private string _job;
        private string _newShift;

        public string NewShift
        {
            get { return _newShift; }
            set
            {
                _newShift = value;
            }
        }

        public string Job
        {
            get { return _job; }
            set { _job = value; }
        }

        public DayOfWeek Day
        {
            get { return _day; }
            set { _day = value; }
        }

        public int NumAvailable
        {
            get { return _numAvailable; }
            set { _numAvailable = value; }
        }
    
        public void AddShift()
        {
            if (NewShift.Trim() == "")
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (DataAccess.AddShiftForJobOnDay(Day, Job, NewShift, NumAvailable))
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
