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
using DayOfWeek = System.DayOfWeek;

namespace ScheduleWhizRedux.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private BindableCollection<Employee> _employees = new BindableCollection<Employee>();
        //private BindableCollection<Employee> _peopleFound = new BindableCollection<Employee>();
        private BindableCollection<Job> _allJobs = new BindableCollection<Job>();
        private Employee _selectedEmployee;
        private Job _selectedJob;
        private IJob _selectedAssignedJob;
        private IJob _selectedAvailableJob;
        private readonly AddEmployeeViewModel addEmployeeViewModel;
        private readonly ModifyEmployeeViewModel modifyEmployeeViewModel;
        private readonly AddJobViewModel addJobViewModel;
        private readonly ModifyJobViewModel modifyJobViewModel;
        private readonly IWindowManager windowManager;
        private IJob _sunSelectedJob;
        private IJob _monSelectedJob;
        private IJob _tueSelectedJob;
        private IJob _wedSelectedJob;
        private IJob _thuSelectedJob;
        private IJob _friSelectedJob;
        private IJob _satSelectedJob;
        private IShift _sunSelectedShift;
        private IShift _monSelectedShift;
        private IShift _tueSelectedShift;
        private IShift _wedSelectedShift;
        private IShift _thuSelectedShift;
        private IShift _friSelectedShift;
        private IShift _satSelectedShift;
        private List<IShift> _sunShiftsAvailableForJob;
        private List<IShift> _monShiftsAvailableForJob;
        private List<IShift> _tueShiftsAvailableForJob;
        private List<IShift> _wedShiftsAvailableForJob;
        private List<IShift> _thuShiftsAvailableForJob;
        private List<IShift> _friShiftsAvailableForJob;
        private List<IShift> _satShiftsAvailableForJob;
        private INumAvailable _sunNumShiftsAvailableForJob;
        private INumAvailable _monNumShiftsAvailableForJob;
        private INumAvailable _tueNumShiftsAvailableForJob;
        private INumAvailable _wedNumShiftsAvailableForJob;
        private INumAvailable _thuNumShiftsAvailableForJob;
        private INumAvailable _friNumShiftsAvailableforJob;
        private INumAvailable _satNumShiftsAvailableForJob;

        public List<IShift> SunShiftsAvailableForJob
        {
            get { return DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob); }
            set
            {
                _sunShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => SunShiftsAvailableForJob);
            }
        }

        public INumAvailable SunNumShiftsAvailableForJob
        {
            get
            {
                return DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob, SunSelectedShift);
            }
            set
            {
                _sunNumShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => SunNumShiftsAvailableForJob);
            }
        }


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

        public IJob SelectedAssignedJob
        {
            get { return _selectedAssignedJob; }
            set
            {
                _selectedAssignedJob = value;
                NotifyOfPropertyChange(() => SelectedAssignedJob);
            }
        }

        public IJob SelectedAvailableJob
        {
            get { return _selectedAvailableJob; }
            set
            {
                _selectedAvailableJob = value;
                NotifyOfPropertyChange(() => SelectedAvailableJob);
            }
        }

        public IJob SunSelectedJob
        {
            get { return _sunSelectedJob; }
            set
            {
                _sunSelectedJob = value;
                NotifyOfPropertyChange(() => SunSelectedJob);
            }
        }

        public IJob MonSelectedJob
        {
            get { return _monSelectedJob; }
            set
            {
                _monSelectedJob = value;
                NotifyOfPropertyChange(() => MonSelectedJob);
            }
        }

        public IJob TueSelectedJob
        {
            get { return _tueSelectedJob; }
            set
            {
                _tueSelectedJob = value;
                NotifyOfPropertyChange(() => TueSelectedJob);
            }
        }

        public IJob WedSelectedJob
        {
            get { return _wedSelectedJob; }
            set
            {
                _wedSelectedJob = value;
                NotifyOfPropertyChange(() => WedSelectedJob);
            }
        }

        public IJob ThuSelectedJob
        {
            get { return _thuSelectedJob; }
            set
            {
                _thuSelectedJob = value;
                NotifyOfPropertyChange(() => ThuSelectedJob);
            }
        }

        public IJob FriSelectedJob
        {
            get { return _friSelectedJob; }
            set
            {
                _friSelectedJob = value;
                NotifyOfPropertyChange(() => FriSelectedJob);
            }
        }

        public IJob SatSelectedJob
        {
            get { return _satSelectedJob; }
            set
            {
                _satSelectedJob = value;
                NotifyOfPropertyChange(() => SatSelectedJob);
            }
        }

        public IShift SunSelectedShift
        {
            get { return _sunSelectedShift; }
            set
            {
                _sunSelectedShift = value;
                NotifyOfPropertyChange(() => SunSelectedShift);
            }
        }

        public IShift MonSelectedShift
        {
            get { return _monSelectedShift; }
            set
            {
                _monSelectedShift = value;
                NotifyOfPropertyChange(() => MonSelectedShift);
            }
        }

        public IShift TueSelectedShift
        {
            get { return _tueSelectedShift; }
            set
            {
                _tueSelectedShift = value;
                NotifyOfPropertyChange(() => TueSelectedShift);
            }
        }

        public IShift WedSelectedShift
        {
            get { return _wedSelectedShift; }
            set
            {
                _wedSelectedShift = value;
                NotifyOfPropertyChange(() => WedSelectedShift);
            }
        }

        public IShift ThuSelectedShift
        {
            get { return _thuSelectedShift; }
            set
            {
                _thuSelectedShift = value;
                NotifyOfPropertyChange(() => ThuSelectedShift);
            }
        }

        public IShift FriSelectedShift
        {
            get { return _friSelectedShift; }
            set
            {
                _friSelectedShift = value;
                NotifyOfPropertyChange(() => FriSelectedShift);
            }
        }

        public IShift SatSelectedShift
        {
            get { return _satSelectedShift; }
            set
            {
                _satSelectedShift = value;
                NotifyOfPropertyChange(() => SatSelectedShift);
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
            modifyJobViewModel.ModifiedJob = SelectedJob.JobTitle;
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
                        $"The job, {jobToUnAssign.JobTitle}, was unassigned from {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
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
                        $"The job, {jobToAssign.JobTitle}, was assigned to {SelectedEmployee.FirstName} {SelectedEmployee.LastName}.",
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
    }
}
