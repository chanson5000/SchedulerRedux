using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private BindableCollection<Employee> _employees = new BindableCollection<Employee>();
        //private BindableCollection<Employee> _peopleFound = new BindableCollection<Employee>();
        private BindableCollection<Job> _allJobs = new BindableCollection<Job>();
        private Employee _selectedEmployee;
        private Job _selectedJob;
        private string _selectedAssignedJob;
        private string _selectedAvailableJob;
        private readonly AddEmployeeViewModel addEmployeeViewModel;
        private readonly ModifyEmployeeViewModel modifyEmployeeViewModel;
        private readonly AddJobViewModel addJobViewModel;
        private readonly ModifyJobViewModel modifyJobViewModel;
        private readonly IWindowManager windowManager;

        public ShellViewModel()
        {
            Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
            windowManager = new WindowManager();
            addEmployeeViewModel = new AddEmployeeViewModel();
            modifyEmployeeViewModel = new ModifyEmployeeViewModel();
            addJobViewModel = new AddJobViewModel();
            modifyJobViewModel = new ModifyJobViewModel();
        }

        public BindableCollection<Job> AllJobs
        {
            get { return _allJobs; }
            set
            {
                _allJobs = value;
                NotifyOfPropertyChange(() => AllJobs);
            }
        }

        public BindableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                NotifyOfPropertyChange(() => Employees);
            }
        }

        public string SelectedAssignedJob
        {
            get { return _selectedAssignedJob; }
            set
            {
                _selectedAssignedJob = value;
                NotifyOfPropertyChange(() => SelectedAssignedJob);
            }
        }

        public string SelectedAvailableJob
        {
            get { return _selectedAvailableJob; }
            set
            {
                _selectedAvailableJob = value;
                NotifyOfPropertyChange(() => SelectedAvailableJob);
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public Job SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyOfPropertyChange(() => SelectedJob);
            }
        }

        public void UpdateEmployeeList()
        {
            Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
        }

        public void AddEmployee()
        {
            var result = windowManager.ShowDialog(addEmployeeViewModel);
            if (result == true)
            {
                Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            }
        }

        public void RemoveEmployee()
        {
            if (MessageBox.Show("Do you really want to remove the employee from the database? This cannot be undone.",
                    "Remove Employee?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // Do nothing if user says no.
            }
            else
            {
                if (DataAccess.RemoveEmployee(SelectedEmployee))
                {
                    Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
                    MessageBox.Show("The the employee was removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Unable to remove the employee from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ModifyEmployee()
        {
            if (SelectedEmployee == null) return;
            modifyEmployeeViewModel.ModifyingEmployee = SelectedEmployee;
            var result = windowManager.ShowDialog(modifyEmployeeViewModel);
            if (result == true)
            {
                Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            }
        }

        public void AddJob()
        {
            var result = windowManager.ShowDialog(addJobViewModel);
            if (result == true)
            {
                AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public void ModifyJob()
        {
            if (SelectedJob == null) return;
            modifyJobViewModel.ModifiedJob = SelectedJob.Title;
            var result = windowManager.ShowDialog(modifyJobViewModel);
            if (result != true) return;
            AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
            NotifyOfPropertyChange(() => SelectedEmployee);
        }

        public void RemoveJob()
        {
            if (MessageBox.Show("Do you really want to remove the job from the database? This cannot be undone.",
                    "Remove Job?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // Do nothing if user says no.
            }
            else
            {
                if (DataAccess.RemoveJob(SelectedJob))
                {
                    AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
                    NotifyOfPropertyChange(() => SelectedEmployee);
                    MessageBox.Show("The the job was removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Unable to remove the employee from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void SwapJobAssignment()
        {
            if (SelectedAssignedJob != null)
            {
                var jobToUnAssign = DataAccess.GetJobRecordFromTitle(SelectedAssignedJob);

                if (DataAccess.UnAssignJobFromEmployee(jobToUnAssign, SelectedEmployee))
                {
                    MessageBox.Show(
                        $"The job, {jobToUnAssign.Title}, was unassigned from {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
                        "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    NotifyOfPropertyChange(() => SelectedEmployee);
                }
                else
                {
                    MessageBox.Show(
                        $"Unable to unassign the job, {SelectedAssignedJob} from {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
                        "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (SelectedAvailableJob != null)
            {
                var jobToAssign = DataAccess.GetJobRecordFromTitle(SelectedAvailableJob);

                if (DataAccess.AssignJobToEmployee(jobToAssign, SelectedEmployee))
                {
                    MessageBox.Show(
                        $"The job, {jobToAssign.Title}, was assigned to {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
                        "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    NotifyOfPropertyChange(() => SelectedEmployee);
                }
                else
                {
                    MessageBox.Show(
                        $"Unable to assign the job, {SelectedAvailableJob}, to {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
                        "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else
            {
                MessageBox.Show("You must select an employee and a job to assign or unassign.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        // Works automagically with clear text using Caliburn Micro.
        // Its all about the naming conventions.
        // This code may be useful to have as a reference for later.
        //public bool CanClearText(string firstName, string lastName)
        //{
        //    return !String.IsNullOrWhiteSpace(firstName) || !String.IsNullOrWhiteSpace(lastName);
        //}   

        //public void LoadPageOne()
        //{
        //    ActivateItem(new FirstChildViewModel());
        //}

        //public void LoadPageTwo()
        //{
        //    ActivateItem(new SecondChildViewModel());
        //}
        //public BindableCollection<Employee> PeopleFound
        //{
        //    get { return _peopleFound; }
        //    set
        //    {
        //        _peopleFound = value;
        //        NotifyOfPropertyChange(() => PeopleFound);
        //    }
        //}

    }
}
