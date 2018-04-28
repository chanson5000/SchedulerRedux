using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;
using DayOfWeek = System.DayOfWeek;

namespace ScheduleWhizRedux.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IEmployeeRepository _employees;
        private readonly IJobRepository _jobs;
        private readonly IAssignedJobRepository _assignedJobs;
        private readonly IAssignedShiftRepository _assignedShifts;
        private BindableCollection<Employee> _allEmployees = new BindableCollection<Employee>();
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
            _employees = new EmployeeRepository();
            _jobs = new JobRepository();
            _assignedJobs = new AssignedJobRepository();
            _assignedShifts = new AssignedShiftRepository();
            AllEmployees = new BindableCollection<Employee>(_employees.GetAllSorted());
            AllJobs = new BindableCollection<Job>(_jobs.GetAllSorted());
            _windowManager = new WindowManager();
            _addEmployeeViewModel = new AddEmployeeViewModel();
            _modifyEmployeeViewModel = new ModifyEmployeeViewModel();
            _addJobViewModel = new AddJobViewModel();
            _modifyJobViewModel = new ModifyJobViewModel();
            _addShiftViewModel = new AddShiftViewModel();
        }

        public BindableCollection<Employee> AllEmployees
        {
            get => _allEmployees;
            set
            {
                _allEmployees = value;
                NotifyOfPropertyChange(() => AllEmployees);
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
                if (SunSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle) != null)
                {
                    SunShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
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
                if (MonSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle) != null)
                {
                    MonShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle);
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
                if (TueSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle) != null)
                {
                    TueShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
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
                if (WedSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle) != null)
                {
                    WedShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
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
                if (ThuSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle) != null)
                {
                    ThuShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
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
                if (FriSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle) != null)
                {
                    FriShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle);
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
                if (SatSelectedJob != null && _assignedShifts.GetAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle) != null)
                {
                    SatShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle, SunSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle, MonSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle, TueSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle, WedSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle, ThuSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle, FriSelectedShift);
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
                        _assignedShifts.GetNumAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle, SatSelectedShift);
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
                if (SunSelectedJob != null && SunSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle,
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
                if (MonSelectedJob != null && MonSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle,
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
                if (TueSelectedJob != null && TueSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle,
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
                if (WedSelectedJob != null && WedSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle,
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
                if (ThuSelectedJob != null && ThuSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle,
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
                if (FriSelectedJob != null && FriSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle,
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
                if (SatSelectedJob != null && SatSelectedShift != null)
                {
                    _assignedShifts.SetNumAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle,
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
                AllEmployees = new BindableCollection<Employee>(_employees.GetAllSorted());
            }
        }

        public void ModifyEmployee()
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to modify.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            _modifyEmployeeViewModel.ModifyingEmployee = SelectedEmployee;
            var result = _windowManager.ShowDialog(_modifyEmployeeViewModel);
            if (result == true)
            {
                AllEmployees = new BindableCollection<Employee>(_employees.GetAllSorted());
            }
        }

        public void RemoveEmployee()
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to remove.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (MessageBox.Show("Do you really want to remove the employee from the database? This cannot be undone.",
                    "Remove Employee?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // Do nothing if user says no.
            }
            else
            {
                if (_employees.Remove(SelectedEmployee))
                {
                    AllEmployees = new BindableCollection<Employee>(_employees.GetAllSorted());
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
                AllJobs = new BindableCollection<Job>(_jobs.GetAllSorted());
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public void ModifyJob()
        {
            if (SelectedJob == null)
            {
                MessageBox.Show("Please select a job to modify.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (SelectedJob == null) return;
            _modifyJobViewModel.ModifiedJob = SelectedJob.JobTitle;
            var result = _windowManager.ShowDialog(_modifyJobViewModel);
            if (result != true) return;
            AllJobs = new BindableCollection<Job>(_jobs.GetAllSorted());
            NotifyOfPropertyChange(() => SelectedEmployee);
        }

        public void RemoveJob()
        {
            if (SelectedJob == null)
            {
                MessageBox.Show("Please select a job to remove.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (MessageBox.Show("Do you really want to remove the job from the database? This cannot be undone.",
                    "Remove Job?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // Do nothing if user says no.
            }
            else
            {
                if (_jobs.Remove(SelectedJob))
                {
                    AllJobs = new BindableCollection<Job>(_jobs.GetAllSorted());
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
                var jobToUnAssign = _jobs.GetRecord(SelectedAssignedJob);

                if (_assignedJobs.Remove(jobToUnAssign, SelectedEmployee))
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
                var jobToAssign = _jobs.GetRecord(SelectedAvailableJob);

                if (_assignedJobs.Add(jobToAssign, SelectedEmployee))
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
            SunShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
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
            MonShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle);
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
            TueShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
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
            WedShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
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
            ThuShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
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
            FriShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle);
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
            SatShiftsAvailableForJob = _assignedShifts.GetAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Sunday, SunSelectedJob.JobTitle, SunSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    SunShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Sunday, SunSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Monday, MonSelectedJob.JobTitle, MonSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    MonShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Monday, MonSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Tuesday, TueSelectedJob.JobTitle, TueSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    TueShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Tuesday, TueSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Wednesday, WedSelectedJob.JobTitle, WedSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    WedShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Wednesday, WedSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Thursday, ThuSelectedJob.JobTitle, ThuSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ThuShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Thursday, ThuSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Friday, FriSelectedJob.JobTitle, FriSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    FriShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Friday, FriSelectedJob.JobTitle);
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
                if (_assignedShifts.Remove(DayOfWeek.Saturday, SatSelectedJob.JobTitle, SatSelectedShift))
                {
                    MessageBox.Show("This shift was successfully removed from the database.", "Operation Successful",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    SatShiftsAvailableForJob =
                        _assignedShifts.GetAvailable(DayOfWeek.Saturday, SatSelectedJob.JobTitle);
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
