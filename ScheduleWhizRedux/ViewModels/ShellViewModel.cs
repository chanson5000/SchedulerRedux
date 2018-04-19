using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.ViewModels
{
    public class ShellViewModel: Conductor<object>
    {
        private BindableCollection<Employee> _employees = new BindableCollection<Employee>();
        private BindableCollection<Employee> _peopleFound = new BindableCollection<Employee>();
        private BindableCollection<Job> _allJobs = new BindableCollection<Job>();
        private Employee _selectedEmployee;
        private Job _selectedJob;
        private readonly AddEmployeeViewModel addEmployeeViewModel;
        private readonly IWindowManager windowManager;
        //private List<string> _availableJobs;
        //private List<string> _assignedJobs;
        

        public ShellViewModel()
        {
            Employees = new BindableCollection<Employee>(DataAccess.GetAllEmployees());
            AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobs());
            windowManager = new WindowManager();
            addEmployeeViewModel = new AddEmployeeViewModel();
        }

        public BindableCollection<Job> AllJobs
        {
            get { return _allJobs; }
            set { _allJobs = value; }
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

        public void AddEmployee()
        {
            windowManager.ShowWindow(addEmployeeViewModel);
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
