using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.ViewModels
{
    class AddEmployeeViewModel : Screen
    {
        private string _addFirstName;
        private string _addLastName;
        private string _addEmailAddress;
        private string _addPhoneNumber;

        public string AddFirstName
        {
            get { return _addFirstName; }
            set { _addFirstName = value; }
        }

        public string AddLastName
        {
            get { return _addLastName; }
            set { _addLastName = value; }
        }

        public string AddEmailAddress
        {
            get { return _addEmailAddress; }
            set { _addEmailAddress = value; }
        }

        public string AddPhoneNumber
        {
            get { return _addPhoneNumber; }
            set { _addPhoneNumber = value; }
        }

        public void AddEmployee()
        {
            
        }
    }

}

