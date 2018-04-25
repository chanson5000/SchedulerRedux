using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;

namespace ScheduleWhizRedux.ViewModels
{
    public class AddJobViewModel : Screen
    {
        private string _newJob;

        public string NewJob
        {
            get { return _newJob; }
            set { _newJob = value; }
        }

        public void AddJob()
        {
            if (String.IsNullOrWhiteSpace(NewJob))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (DataAccess.AddJob(NewJob.Trim()))
            {
                MessageBox.Show($"The job, {NewJob}, was added to the database.", "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NewJob = "";
                TryClose(true);
            }
            else
            {
                MessageBox.Show($"Unable to add the job, {NewJob}, to the database.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
