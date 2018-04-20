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
    class ModifyEmployeeViewModel : Screen
    {
        private Employee _modifyingEmployee;

        public Employee ModifyingEmployee
        {
            get { return _modifyingEmployee; }
            set { _modifyingEmployee = value; }
        }

        public void ModifyEmployee()
        {
            if (String.IsNullOrWhiteSpace(ModifyingEmployee.FirstName) || String.IsNullOrWhiteSpace(ModifyingEmployee.LastName)
                || String.IsNullOrWhiteSpace(ModifyingEmployee.EmailAddress) || String.IsNullOrWhiteSpace(ModifyingEmployee.PhoneNumber))
            {
                MessageBox.Show("Please do not leave any fields blank.", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (DataAccess.ModifyEmployee(ModifyingEmployee))
            {

                MessageBox.Show($"The employee, {ModifyingEmployee.FirstName} {ModifyingEmployee.LastName}, was modified.",
                    "Operation Successful",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                this.TryClose(true);
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
