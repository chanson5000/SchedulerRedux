using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScheduleWhizRedux.Helpers;

namespace ScheduleWhizRedux.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void SelectedEmployee_AssignedJobs_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectedEmployee_AvailableJobs.UnselectAll();
        }

        private void SelectedEmployee_AvailableJobs_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectedEmployee_AssignedJobs.UnselectAll();
        }
    }
}
