using System.Linq;
using System.Windows;
using ScheduleWhizRedux.Repositories;
using ScheduleWhizRedux.Utilities;


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
            Scheduler scheduler = new Scheduler(new EmployeeRepository().GetAllSorted(),
                new AssignedShiftRepository().GetAll().Where(x => x.NumAvailable > 0)
                    .ToList());
 
            string spreadsheetFileName = scheduler.Generate();

            scheduler.LaunchSpreadsheet(spreadsheetFileName);
        }
    }
}
