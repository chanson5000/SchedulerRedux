using System;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.ViewModels
{
    internal class AddJobViewModel : Screen
    {
        private readonly IJobRepository _jobs;

        public AddJobViewModel()
        {
            _jobs = new JobRepository();
        }

        public string NewJob { get; set; }

        public void AddJob()
        {
            if (string.IsNullOrWhiteSpace(NewJob))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (_jobs.Add(NewJob.Trim()))
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
