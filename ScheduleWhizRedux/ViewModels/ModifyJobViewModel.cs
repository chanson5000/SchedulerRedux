using System;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.ViewModels
{
    internal class ModifyJobViewModel : Screen
    {
        private readonly IJobRepository _jobs;

        public ModifyJobViewModel()
        {
            _jobs = new JobRepository();
        }

        public string ModifiedJob { get; set; }

        public void ModifyJob()
        {
            if (String.IsNullOrWhiteSpace(ModifiedJob))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (_jobs.Add(ModifiedJob.Trim()))
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
