
using System.Windows;
using ScheduleWhizRedux.Repositories;


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scheduler scheduler = new Scheduler(new EmployeeRepository().GetAllSorted(), new JobRepository().GetAllSorted(), new AssignedJobRepository().GetAll(), new AssignedShiftRepository().GetAll());
            scheduler.Generate();
        }
    }
}
