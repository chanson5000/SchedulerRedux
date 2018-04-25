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
        private string _selectedAssignedJob;
        private string _selectedAvailableJob;
        private readonly AddEmployeeViewModel addEmployeeViewModel;
        private readonly ModifyEmployeeViewModel modifyEmployeeViewModel;
        private readonly AddJobViewModel addJobViewModel;
        private readonly ModifyJobViewModel modifyJobViewModel;
        private readonly AddShiftViewModel addShiftViewModel;
        private readonly IWindowManager windowManager;
        private Job _sunSelectedJob;
        private string _monSelectedJob;
        private string _tueSelectedJob;
        private string _wedSelectedJob;
        private string _thuSelectedJob;
        private string _friSelectedJob;
        private string _satSelectedJob;
        private string _sunSelectedShift;
        private string _monSelectedShift;
        private string _tueSelectedShift;
        private string _wedSelectedShift;
        private string _thuSelectedShift;
        private string _friSelectedShift;
        private string _satSelectedShift;
        private List<string> _sunShiftsAvailableForJob;
        private List<string> _monShiftsAvailableForJob;
        private List<string> _tueShiftsAvailableForJob;
        private List<string> _wedShiftsAvailableForJob;
        private List<string> _thuShiftsAvailableForJob;
        private List<string> _friShiftsAvailableForJob;
        private List<string> _satShiftsAvailableForJob;
        private int _sunNumShiftsAvailableForJob;
        private int _monNumShiftsAvailableForJob;
        private int _tueNumShiftsAvailableForJob;
        private int _wedNumShiftsAvailableForJob;
        private int _thuNumShiftsAvailableForJob;
        private int _friNumShiftsAvailableforJob;
        private int _satNumShiftsAvailableForJob;

        //public string SunAssignedShift { get; set; }

        public List<string> SunShiftsAvailableForJob
        {
            get { return _sunShiftsAvailableForJob; }
            set
            {
                _sunShiftsAvailableForJob = value;
                //if (SunSelectedJob != null && SunShiftsAvailableForJob != null && SunSelectedShift !=null)
                //{
                //    SunNumShiftsAvailableForJob =
                //        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle,
                //            SunSelectedShift);
                //}
                NotifyOfPropertyChange(() => SunShiftsAvailableForJob);
            }
        }


        public string SunSelectedShift
        {
            get { return _sunSelectedShift; }
            set
            {
                _sunSelectedShift = value;
                if (SunSelectedJob != null && SunSelectedShift != null)
                {
                    SunNumShiftsAvailableForJob =
                       DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle, SunSelectedShift);
                }
                NotifyOfPropertyChange(() => SunSelectedShift);
            }
        }

        public int SunNumShiftsAvailableForJob
        {
            get
            {
                return _sunNumShiftsAvailableForJob;
            }
            set
            {
                _sunNumShiftsAvailableForJob = value;
                if (SunSelectedJob.JobTitle != null && SunSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle,
                        SunSelectedShift, SunNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => SunNumShiftsAvailableForJob);
            }
        }

        public Job SunSelectedJob
        {
            get { return _sunSelectedJob; }
            set
            {
                _sunSelectedJob = value;
                if (SunSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle) != null)
                {
                    SunShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => SunSelectedJob);
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
            addShiftViewModel = new AddShiftViewModel();
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

        public string MonSelectedJob
        {
            get { return _monSelectedJob; }
            set
            {
                _monSelectedJob = value;
                NotifyOfPropertyChange(() => MonSelectedJob);
            }
        }

        public string TueSelectedJob
        {
            get { return _tueSelectedJob; }
            set
            {
                _tueSelectedJob = value;
                NotifyOfPropertyChange(() => TueSelectedJob);
            }
        }

        public string WedSelectedJob
        {
            get { return _wedSelectedJob; }
            set
            {
                _wedSelectedJob = value;
                NotifyOfPropertyChange(() => WedSelectedJob);
            }
        }

        public string ThuSelectedJob
        {
            get { return _thuSelectedJob; }
            set
            {
                _thuSelectedJob = value;
                NotifyOfPropertyChange(() => ThuSelectedJob);
            }
        }

        public string FriSelectedJob
        {
            get { return _friSelectedJob; }
            set
            {
                _friSelectedJob = value;
                NotifyOfPropertyChange(() => FriSelectedJob);
            }
        }

        public string SatSelectedJob
        {
            get { return _satSelectedJob; }
            set
            {
                _satSelectedJob = value;
                NotifyOfPropertyChange(() => SatSelectedJob);
            }
        }

        public string MonSelectedShift
        {
            get { return _monSelectedShift; }
            set
            {
                _monSelectedShift = value;
                NotifyOfPropertyChange(() => MonSelectedShift);
            }
        }

        public string TueSelectedShift
        {
            get { return _tueSelectedShift; }
            set
            {
                _tueSelectedShift = value;
                NotifyOfPropertyChange(() => TueSelectedShift);
            }
        }

        public string WedSelectedShift
        {
            get { return _wedSelectedShift; }
            set
            {
                _wedSelectedShift = value;
                NotifyOfPropertyChange(() => WedSelectedShift);
            }
        }

        public string ThuSelectedShift
        {
            get { return _thuSelectedShift; }
            set
            {
                _thuSelectedShift = value;
                NotifyOfPropertyChange(() => ThuSelectedShift);
            }
        }

        public string FriSelectedShift
        {
            get { return _friSelectedShift; }
            set
            {
                _friSelectedShift = value;
                NotifyOfPropertyChange(() => FriSelectedShift);
            }
        }

        public string SatSelectedShift
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

        public void SunAddShift()
        {
            if (SunSelectedJob == null)
            {
                // TODO: Add a message.
                return;
            }

            addShiftViewModel.Day = DayOfWeek.Sunday;
            addShiftViewModel.Job = SunSelectedJob.JobTitle;
            addShiftViewModel.NumAvailable = SunNumShiftsAvailableForJob;

            var result = windowManager.ShowDialog(addShiftViewModel);
            if (result != true) return;
            SunShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
        }

        public void SunRemoveShift()
        {
            if (SunSelectedJob == null || SunSelectedShift == null)
            {
                // TODO: Add a message.
                return;
            }

            if (MessageBox.Show("Do you really want to remove the shift from the database? This cannot be undone.",
                    "Remove Job?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // Do nothing if user says no.
            }
            else
            {
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle, SunSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    SunShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
