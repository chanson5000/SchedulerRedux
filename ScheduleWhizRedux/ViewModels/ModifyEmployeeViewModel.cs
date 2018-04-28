using System;
using System.Windows;
using Caliburn.Micro;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;
using ScheduleWhizRedux.Repositories;

namespace ScheduleWhizRedux.ViewModels
{
    internal class ModifyEmployeeViewModel : Screen
    {
        private readonly IEmployeeRepository _employees;

        public ModifyEmployeeViewModel()
        {
            _employees = new EmployeeRepository();
        }

        public Employee ModifyingEmployee { get; set; }

        public void ModifyEmployee()
        {
            if (String.IsNullOrWhiteSpace(ModifyingEmployee.FirstName) || String.IsNullOrWhiteSpace(ModifyingEmployee.LastName)
                || String.IsNullOrWhiteSpace(ModifyingEmployee.EmailAddress) || String.IsNullOrWhiteSpace(ModifyingEmployee.PhoneNumber))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (_employees.Modify(ModifyingEmployee))
            {
                MessageBox.Show($"The employee, {ModifyingEmployee.FirstName} {ModifyingEmployee.LastName}, was modified.",
                    "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                TryClose(true);
            }
            else
            {
                MessageBox.Show($"Unable to modify the employee, {ModifyingEmployee.FirstName} {ModifyingEmployee.LastName}, to the database.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
