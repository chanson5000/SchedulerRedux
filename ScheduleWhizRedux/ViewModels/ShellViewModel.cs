using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;
using DayOfWeek = System.DayOfWeek;

namespace ScheduleWhizRedux.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private BindableCollection<Employee> _employees = new BindableCollection<Employee>();
        private BindableCollection<Job> _allJobs = new BindableCollection<Job>();
        private Employee _selectedEmployee;
        private Job _selectedJob;
        private string _selectedAssignedJob;
        private string _selectedAvailableJob;
        private readonly AddEmployeeViewModel _addEmployeeViewModel;
        private readonly ModifyEmployeeViewModel _modifyEmployeeViewModel;
        private readonly AddJobViewModel _addJobViewModel;
        private readonly ModifyJobViewModel _modifyJobViewModel;
        private readonly AddShiftViewModel _addShiftViewModel;
        private readonly IWindowManager _windowManager;
        private Job _sunSelectedJob;
        private Job _monSelectedJob;
        private Job _tueSelectedJob;
        private Job _wedSelectedJob;
        private Job _thuSelectedJob;
        private Job _friSelectedJob;
        private Job _satSelectedJob;
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
        private int _friNumShiftsAvailableForJob;
        private int _satNumShiftsAvailableForJob;

        public ShellViewModel()
        {
            Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
            _windowManager = new WindowManager();
            _addEmployeeViewModel = new AddEmployeeViewModel();
            _modifyEmployeeViewModel = new ModifyEmployeeViewModel();
            _addJobViewModel = new AddJobViewModel();
            _modifyJobViewModel = new ModifyJobViewModel();
            _addShiftViewModel = new AddShiftViewModel();
        }

        public BindableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                NotifyOfPropertyChange(() => Employees);
            }
        }

        public BindableCollection<Job> AllJobs
        {
            get => _allJobs;
            set
            {
                _allJobs = value;
                NotifyOfPropertyChange(() => AllJobs);
            }
        }


        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                NotifyOfPropertyChange(() => SelectedJob);
            }
        }

        public string SelectedAssignedJob
        {
            get => _selectedAssignedJob;
            set
            {
                _selectedAssignedJob = value;
                NotifyOfPropertyChange(() => SelectedAssignedJob);
            }
        }

        public string SelectedAvailableJob
        {
            get => _selectedAvailableJob;
            set
            {
                _selectedAvailableJob = value;
                NotifyOfPropertyChange(() => SelectedAvailableJob);
            }
        }


        public Job SunSelectedJob
        {
            get => _sunSelectedJob;
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

        public Job MonSelectedJob
        {
            get => _monSelectedJob;
            set
            {
                _monSelectedJob = value;
                if (MonSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle) != null)
                {
                    MonShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => MonSelectedJob);
            }
        }

        public Job TueSelectedJob
        {
            get => _tueSelectedJob;
            set
            {
                _tueSelectedJob = value;
                if (TueSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle) != null)
                {
                    TueShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => TueSelectedJob);
            }
        }

        public Job WedSelectedJob
        {
            get => _wedSelectedJob;
            set
            {
                _wedSelectedJob = value;
                if (WedSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle) != null)
                {
                    WedShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => WedSelectedJob);
            }
        }

        public Job ThuSelectedJob
        {
            get => _thuSelectedJob;
            set
            {
                _thuSelectedJob = value;
                if (ThuSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle) != null)
                {
                    ThuShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => ThuSelectedJob);
            }
        }

        public Job FriSelectedJob
        {
            get => _friSelectedJob;
            set
            {
                _friSelectedJob = value;
                if (FriSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle) != null)
                {
                    FriShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => FriSelectedJob);
            }
        }

        public Job SatSelectedJob
        {
            get => _satSelectedJob;
            set
            {
                _satSelectedJob = value;
                if (SatSelectedJob != null && DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle) != null)
                {
                    SatShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
                }
                NotifyOfPropertyChange(() => SatSelectedJob);
            }
        }

        public List<string> SunShiftsAvailableForJob
        {
            get => _sunShiftsAvailableForJob;
            set
            {
                _sunShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => SunShiftsAvailableForJob);
            }
        }

        public List<string> MonShiftsAvailableForJob
        {
            get => _monShiftsAvailableForJob;
            set
            {
                _monShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => MonShiftsAvailableForJob);
            }
        }

        public List<string> TueShiftsAvailableForJob
        {
            get => _tueShiftsAvailableForJob;
            set
            {
                _tueShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => TueShiftsAvailableForJob);
            }
        }

        public List<string> WedShiftsAvailableForJob
        {
            get => _wedShiftsAvailableForJob;
            set
            {
                _wedShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => WedShiftsAvailableForJob);
            }
        }

        public List<string> ThuShiftsAvailableForJob
        {
            get => _thuShiftsAvailableForJob;
            set
            {
                _thuShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => ThuShiftsAvailableForJob);
            }
        }

        public List<string> FriShiftsAvailableForJob
        {
            get => _friShiftsAvailableForJob;
            set
            {
                _friShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => FriShiftsAvailableForJob);
            }
        }

        public List<string> SatShiftsAvailableForJob
        {
            get => _satShiftsAvailableForJob;
            set
            {
                _satShiftsAvailableForJob = value;
                NotifyOfPropertyChange(() => SatShiftsAvailableForJob);
            }
        }

        public string SunSelectedShift
        {
            get => _sunSelectedShift;
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

        public string MonSelectedShift
        {
            get => _monSelectedShift;
            set
            {
                _monSelectedShift = value;
                if (MonSelectedJob != null && MonSelectedShift != null)
                {
                    MonNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle, MonSelectedShift);
                }
                NotifyOfPropertyChange(() => MonSelectedShift);
            }
        }

        public string TueSelectedShift
        {
            get => _tueSelectedShift;
            set
            {
                _tueSelectedShift = value;
                if (TueSelectedJob != null && TueSelectedShift != null)
                {
                    TueNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle, TueSelectedShift);
                }
                NotifyOfPropertyChange(() => TueSelectedShift);
            }
        }

        public string WedSelectedShift
        {
            get => _wedSelectedShift;
            set
            {
                _wedSelectedShift = value;
                if (WedSelectedJob != null && WedSelectedShift != null)
                {
                    WedNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle, WedSelectedShift);
                }
                NotifyOfPropertyChange(() => WedSelectedShift);
            }
        }

        public string ThuSelectedShift
        {
            get => _thuSelectedShift;
            set
            {
                _thuSelectedShift = value;
                if (ThuSelectedJob != null && ThuSelectedShift != null)
                {
                    ThuNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle, ThuSelectedShift);
                }
                NotifyOfPropertyChange(() => ThuSelectedShift);
            }
        }

        public string FriSelectedShift
        {
            get => _friSelectedShift;
            set
            {
                _friSelectedShift = value;
                if (FriSelectedJob != null && FriSelectedShift != null)
                {
                    FriNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle, FriSelectedShift);
                }
                NotifyOfPropertyChange(() => FriSelectedShift);
            }
        }

        public string SatSelectedShift
        {
            get => _satSelectedShift;
            set
            {
                _satSelectedShift = value;
                if (SatSelectedJob != null && SatSelectedShift != null)
                {
                    SatNumShiftsAvailableForJob =
                        DataAccess.GetNumAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle, SatSelectedShift);
                }
                NotifyOfPropertyChange(() => SatSelectedShift);
            }
        }

        public int SunNumShiftsAvailableForJob
        {
            get => _sunNumShiftsAvailableForJob;
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
        public int MonNumShiftsAvailableForJob
        {
            get => _monNumShiftsAvailableForJob;
            set
            {
                _monNumShiftsAvailableForJob = value;
                if (MonSelectedJob.JobTitle != null && MonSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle,
                        MonSelectedShift, MonNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => MonNumShiftsAvailableForJob);
            }
        }

        public int TueNumShiftsAvailableForJob
        {
            get => _tueNumShiftsAvailableForJob;
            set
            {
                _tueNumShiftsAvailableForJob = value;
                if (TueSelectedJob.JobTitle != null && TueSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle,
                        TueSelectedShift, TueNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => TueNumShiftsAvailableForJob);
            }
        }

        public int WedNumShiftsAvailableForJob
        {
            get => _wedNumShiftsAvailableForJob;
            set
            {
                _wedNumShiftsAvailableForJob = value;
                if (WedSelectedJob.JobTitle != null && WedSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle,
                        WedSelectedShift, WedNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => WedNumShiftsAvailableForJob);
            }
        }

        public int ThuNumShiftsAvailableForJob
        {
            get => _thuNumShiftsAvailableForJob;
            set
            {
                _thuNumShiftsAvailableForJob = value;
                if (ThuSelectedJob.JobTitle != null && ThuSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle,
                        ThuSelectedShift, ThuNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => ThuNumShiftsAvailableForJob);
            }
        }

        public int FriNumShiftsAvailableForJob
        {
            get => _friNumShiftsAvailableForJob;
            set
            {
                _friNumShiftsAvailableForJob = value;
                if (FriSelectedJob.JobTitle != null && FriSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle,
                        FriSelectedShift, FriNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => FriNumShiftsAvailableForJob);
            }
        }

        public int SatNumShiftsAvailableForJob
        {
            get => _satNumShiftsAvailableForJob;
            set
            {
                _satNumShiftsAvailableForJob = value;
                if (SatSelectedJob.JobTitle != null && SatSelectedShift != null)
                {
                    DataAccess.SetNumAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle,
                        SatSelectedShift, SatNumShiftsAvailableForJob);
                }
                NotifyOfPropertyChange(() => SatNumShiftsAvailableForJob);
            }
        }

        public void AddEmployee()
        {
            var result = _windowManager.ShowDialog(_addEmployeeViewModel);
            if (result == true)
            {
                Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            }
        }

        public void ModifyEmployee()
        {
            if (SelectedEmployee == null) return;
            _modifyEmployeeViewModel.ModifyingEmployee = SelectedEmployee;
            var result = _windowManager.ShowDialog(_modifyEmployeeViewModel);
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

        public void AddJob()
        {
            var result = _windowManager.ShowDialog(_addJobViewModel);
            if (result == true)
            {
                AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobRecords());
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public void ModifyJob()
        {
            if (SelectedJob == null) return;
            _modifyJobViewModel.ModifiedJob = SelectedJob.JobTitle;
            var result = _windowManager.ShowDialog(_modifyJobViewModel);
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
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Sunday;
            _addShiftViewModel.Job = SunSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            SunShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
        }

        public void MonAddShift()
        {
            if (MonSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Monday;
            _addShiftViewModel.Job = MonSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            MonShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle);
        }

        public void TueAddShift()
        {
            if (TueSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Tuesday;
            _addShiftViewModel.Job = TueSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            TueShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
        }

        public void WedAddShift()
        {
            if (WedSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Wednesday;
            _addShiftViewModel.Job = WedSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            WedShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
        }

        public void ThuAddShift()
        {
            if (ThuSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Thursday;
            _addShiftViewModel.Job = ThuSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            ThuShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
        }

        public void FriAddShift()
        {
            if (FriSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Friday;
            _addShiftViewModel.Job = FriSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            FriShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle);
        }

        public void SatAddShift()
        {
            if (SatSelectedJob == null)
            {
                MessageBox.Show("Please select a job.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _addShiftViewModel.Day = DayOfWeek.Saturday;
            _addShiftViewModel.Job = SatSelectedJob.JobTitle;

            var result = _windowManager.ShowDialog(_addShiftViewModel);
            if (result != true) return;
            SatShiftsAvailableForJob = DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
        }

        public void SunRemoveShift()
        {
            if (SunSelectedJob == null || SunSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        public void MonRemoveShift()
        {
            if (MonSelectedJob == null || MonSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle, MonSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    MonShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Monday, MonSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void TueRemoveShift()
        {
            if (TueSelectedJob == null || TueSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle, TueSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    TueShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void WedRemoveShift()
        {
            if (WedSelectedJob == null || WedSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle, WedSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    WedShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ThuRemoveShift()
        {
            if (ThuSelectedJob == null || ThuSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle, ThuSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ThuShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void FriRemoveShift()
        {
            if (FriSelectedJob == null || FriSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle, FriSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    FriShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Friday, FriSelectedJob.JobTitle);
                }
                else
                {
                    MessageBox.Show("Unable to remove the shift from the database.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void SatRemoveShift()
        {
            if (SatSelectedJob == null || SatSelectedShift == null)
            {
                MessageBox.Show("Please select a job and a shift.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DataAccess.RemoveShiftForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle, SatSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    SatShiftsAvailableForJob =
                        DataAccess.GetAvailableShiftsForJobOnDay(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
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
