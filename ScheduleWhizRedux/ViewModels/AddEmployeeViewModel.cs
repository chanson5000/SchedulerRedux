using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Helpers;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.ViewModels
{
    public class AddEmployeeViewModel : Screen
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
            if (String.IsNullOrWhiteSpace(AddFirstName) || String.IsNullOrWhiteSpace(AddLastName) ||
                String.IsNullOrWhiteSpace(AddEmailAddress) || String.IsNullOrWhiteSpace(AddPhoneNumber))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Employee employee = new Employee()
            {
                FirstName = AddFirstName.Trim(),
                LastName = AddLastName.Trim(),
                EmailAddress = AddEmailAddress.Trim(),
                PhoneNumber = AddPhoneNumber.Trim()
            };

            if (DataAccess.AddEmployee(employee))
            {
                
                MessageBox.Show($"The employee, {AddFirstName} {AddLastName}, was added to the database.",
                    "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.TryClose(true);
            }
            else
            {
                MessageBox.Show($"Unable to add the employee, {AddFirstName} {AddLastName}, to the database.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            TryClose();
        }
    }

}

