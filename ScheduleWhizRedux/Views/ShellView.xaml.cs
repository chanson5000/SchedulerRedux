
using System.Windows;


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
