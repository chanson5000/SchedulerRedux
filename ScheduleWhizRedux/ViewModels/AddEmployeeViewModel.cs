using System;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.ViewModels
{
    internal class AddEmployeeViewModel : Screen
    {
        private readonly IEmployeeRepository _employees;

        public AddEmployeeViewModel()
        {
            _employees = new EmployeeRepository();
        }

        public string AddFirstName { get; set; }

        public string AddLastName { get; set; }

        public string AddEmailAddress { get; set; }

        public string AddPhoneNumber { get; set; }

        public void AddEmployee()
        {
            if (string.IsNullOrWhiteSpace(AddFirstName) || string.IsNullOrWhiteSpace(AddLastName) ||
                string.IsNullOrWhiteSpace(AddEmailAddress) || string.IsNullOrWhiteSpace(AddPhoneNumber))
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

            if (_employees.Add(employee))
            {
                
                MessageBox.Show($"The employee, {AddFirstName} {AddLastName}, was added to the database.",
                    "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                AddFirstName = "";
                AddLastName = "";
                AddEmailAddress = "";
                AddPhoneNumber = "";
                TryClose(true);
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

