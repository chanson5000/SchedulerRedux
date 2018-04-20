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
    public class ModifyJobViewModel : Screen
    {
        private string _modifiedJob;
        private DataAccess db;

        public ModifyJobViewModel()
        {
            db = new DataAccess();
        }

        public string ModifiedJob
        {
            get { return _modifiedJob; }
            set { _modifiedJob = value; }
        }

        public void ModifyJob()
        {
            if (String.IsNullOrWhiteSpace(ModifiedJob))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (db.AddJob(ModifiedJob.Trim()))
            {
                MessageBox.Show($"The job, {ModifiedJob}, was modified.", "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                TryClose(true);
            }
            else
            {
                MessageBox.Show($"Unable to modify the job, {ModifiedJob}.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }



    }
}
