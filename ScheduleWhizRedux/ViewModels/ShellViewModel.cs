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
        private BindableCollection<Employee> _people = new BindableCollection<Employee>();
        private BindableCollection<Employee> _peopleFound = new BindableCollection<Employee>();
        //private List<string> _availableJobs;
        //private List<string> _assignedJobs;
        private BindableCollection<Job> _allJobs = new BindableCollection<Job>();
        private Employee _selectedPerson;
        //readonly DataAccess db = new DataAccess();
        
        public ShellViewModel()
        {
            People = new BindableCollection<Employee>(DataAccess.GetAllPeople());
            AllJobs = new BindableCollection<Job>(DataAccess.GetAllJobs());
        }

        //public void EmployeeJobs()
        //{
        //    AssignedJobs = DataAccess.GetEmployeeAssignedJobs(SelectedPerson.Id);
        //    List<string> unAssignedJobs = new List<string>();
        //    foreach (var job in AssignedJobs)
        //    {
        //        if (!AssignedJobs.Contains(job))
        //        {
        //            unAssignedJobs.Add(job);
        //        }
        //    }

        //    AvailableJobs = unAssignedJobs;
        //}

        public BindableCollection<Job> AllJobs
        {
            get { return _allJobs; }
            set { _allJobs = value; }
        }

        //public List<string> AvailableJobs
        //{
        //    get { return _availableJobs; }
        //    set
        //    {
        //        _assignedJobs = value;
        //        NotifyOfPropertyChange(() => AvailableJobs);
        //    }
        //}

        //public List<string> AssignedJobs
        //{
        //    get { return _assignedJobs; }
        //    set
        //    {
        //        _assignedJobs = value;
        //        NotifyOfPropertyChange(() => AssignedJobs);
        //    }
        //}

        public BindableCollection<Employee> People
        {
            get { return _people; }
            set { _people = value; }
        }

        //public BindableCollection<Employee> PeopleFound
        //{
        //    get { return _peopleFound; }
        //    set
        //    {
        //        _peopleFound = value;
        //        NotifyOfPropertyChange(() => PeopleFound);
        //    }
        //}

        public Employee SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        // Works automagically with clear text using Caliburn Micro.
        // Its all about the naming conventions.
        public bool CanClearText(string firstName, string lastName)
        {
            return !String.IsNullOrWhiteSpace(firstName) || !String.IsNullOrWhiteSpace(lastName);
        }   

        public void LoadPageOne()
        {
            ActivateItem(new FirstChildViewModel());
        }

        public void LoadPageTwo()
        {
            ActivateItem(new SecondChildViewModel());
        }

    }

}
